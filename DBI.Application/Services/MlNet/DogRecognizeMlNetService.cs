using Microsoft.ML.Data;
using Microsoft.ML;
using DBI.Infrastructure.Services.Model;

namespace DBI.Application.Services.MlNet
{
    public class DogRecognizeMlNetService : IDogRecognizeMlNetService
    {
        private readonly PredictionEngine<InputDataRecognition, OutputDataRecognition> breedRecognizePredictionEngine;
        public DogRecognizeMlNetService()
        {
            breedRecognizePredictionEngine = CreateDogRecognizePredictionEngine();
        }
        ~DogRecognizeMlNetService()
        {
            if (breedRecognizePredictionEngine != null)
                breedRecognizePredictionEngine.Dispose();
        }

        private PredictionEngine<InputDataRecognition, OutputDataRecognition> CreateDogRecognizePredictionEngine()
        {
            string modelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Models", "model_recognition.onnx");

            MLContext mlContext = new MLContext();

            int size = 224;
            var pipeline =
                mlContext.Transforms.ResizeImages(outputColumnName: "efficientnet-b1_input", imageWidth: size, imageHeight: size)
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "efficientnet-b1_input", scaleImage: 1f / 255f, interleavePixelColors: true))
                    //.Append(mlContext.Transforms.NormalizeMeanVariance(outputColumnName: "efficientnet-b1_input", variance: new float[] { 0.5f }))
                .Append(mlContext.Transforms.ApplyOnnxModel(outputColumnName: "dense_1", inputColumnName: "efficientnet-b1_input", modelFile: modelPath, fallbackToCpu: true));

            var data = mlContext.Data.LoadFromEnumerable(new List<InputDataRecognition>());

            var model = pipeline.Fit(data);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<InputDataRecognition, OutputDataRecognition>(model);

            return predictionEngine;
        }

        public async Task<bool> RecogniteDogOnImageAsync(string base64)
        {
            return await Task.Run(() =>
            {
                try
                {
                    var bytes = Convert.FromBase64String(base64);
                    var contents = new MemoryStream(bytes);

                    OutputDataRecognition output = breedRecognizePredictionEngine.Predict(new InputDataRecognition() { Image = MLImage.CreateFromStream(contents) });

                    var result = output.Scores[0] > 0.4;

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            });
        }
    }
}
