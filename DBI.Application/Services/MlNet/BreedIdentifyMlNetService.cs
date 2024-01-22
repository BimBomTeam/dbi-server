using DBI.Infrastructure.Services.Model;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms.Onnx;

namespace DBI.Application.Services.MlNet
{
    public class BreedIdentifyMlNetService : IBreedIdentifyMlNetService
    {
        //input_1
        //top_activation/mul:0

        //b0_input
        //dense_2

        private readonly PredictionEngine<InputData, OutputData> breedIdentifyEngine;
        public BreedIdentifyMlNetService()
        {
            breedIdentifyEngine = CreateBreedRecognizePredictionEngine();
        }
        ~BreedIdentifyMlNetService()
        {
            if (breedIdentifyEngine != null)
                breedIdentifyEngine.Dispose();
        }

        private PredictionEngine<InputData, OutputData> CreateBreedRecognizePredictionEngine()
        {
            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Models", "model_133.onnx");

            MLContext mlContext = new MLContext();

            int size = 224;
            var pipeline =
                mlContext.Transforms.ResizeImages(outputColumnName: "efficientnet-b0_input", imageWidth: size, imageHeight: size)
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "efficientnet-b0_input", scaleImage: 1f / 255f, interleavePixelColors: true))
                .Append(mlContext.Transforms.ApplyOnnxModel(outputColumnName: "dense_2", inputColumnName: "efficientnet-b0_input", modelFile: modelPath, fallbackToCpu: true));

            var data = mlContext.Data.LoadFromEnumerable(new List<InputData>());

            var model = pipeline.Fit(data);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, OutputData>(model);

            return predictionEngine;
        }

        public async Task<int> IdentifyAsync(string base64)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var bytes = Convert.FromBase64String(base64);
                    var contents = new MemoryStream(bytes);

                    OutputData output = breedIdentifyEngine.Predict(new InputData() { Image = MLImage.CreateFromStream(contents) });

                    var res = output.Scores.ToList().IndexOf(output.Scores.Max());

                    return res;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}