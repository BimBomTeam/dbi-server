using DBI.Infrastructure.Services;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms.Onnx;

namespace DBI.Application.Services.MlNet
{
    public class MlNetService : IAiModelService
    {
        //input_1
        //top_activation/mul:0

        //b0_input
        //dense_2

        private readonly PredictionEngine<InputData, OutputData> predictionEngine;
        public MlNetService()
        {
            predictionEngine = CreatePredictionEngine();
        }
        ~MlNetService()
        { 
            predictionEngine.Dispose();
        }

        private PredictionEngine<InputData, OutputData> CreatePredictionEngine()
        {
            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Models", "model_133.onnx");

            MLContext mlContext = new MLContext();

            int size = 224;
            var pipeline =
                //mlContext.Transforms.LoadImages("efficientnet-b0_input", @"C:\Users\koval\Downloads\New folder", "efficientnet-b0_input")
                mlContext.Transforms.ResizeImages(outputColumnName: "efficientnet-b0_input", imageWidth: size, imageHeight: size)
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "efficientnet-b0_input", scaleImage: 1f / 255f, interleavePixelColors: true))
                //.Append(mlContext.Transforms.NormalizeMeanVariance(inputColumnName: "efficientnet-b0_input", outputColumnName: "efficientnet-b0_input"))
                //.Append(tensorFlowModel.ScoreTensorFlowModel("dense_2", "dense_input_1")));
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

                    OutputData output = predictionEngine.Predict(new InputData() { Image = MLImage.CreateFromStream(contents) });

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