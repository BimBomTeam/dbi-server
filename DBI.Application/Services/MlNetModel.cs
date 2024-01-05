using DBI.Infrastructure.Services;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;
using Microsoft.ML.Transforms.Onnx;

namespace DBI.Application.Services
{
    public class MlNetModel : IAiModelService
    {
        //input_1
        //top_activation/mul:0

        public class InputData
        {
            [ColumnName("efficientnet-b0_input")]
            [ImageType(224, 224)]
            public MLImage Image { get; set; }
        }

        public class OutputData
        {
            [ColumnName("dense_2")]
            public float[] Scores { get; set; }
        }
        public async Task<int> IdentifyAsync(string base64)
        {
            try
            {
                string pbPath = @"E:\\models\\0101\\dbi-ai-model\\best_model";
                string weightPath = @"E:\models\0101\model.onnx";
                string filePath = @"C:\Users\koval\Downloads\GettyImages-1138203895-d4020ac22e454ff2947097836691f76d (1).jpg";
                
                MLContext mlContext = new MLContext();

                int size = 224;
                var pipeline = mlContext.Transforms.ResizeImages(outputColumnName: "efficientnet-b0_input", imageWidth: size, imageHeight: size, inputColumnName: "efficientnet-b0_input")
                    .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "efficientnet-b0_input", outputAsFloatArray: true)
                    .Append(mlContext.Transforms.ApplyOnnxModel("dense_2", "efficientnet-b0_input", weightPath, fallbackToCpu: true)));

                var data = mlContext.Data.LoadFromEnumerable(new List<InputData>());

                var model = pipeline.Fit(data);

                var predictionEngine = mlContext.Model.CreatePredictionEngine<InputData, OutputData>(model);

                var bytes = Convert.FromBase64String(base64);
                var contents = new MemoryStream(bytes); 

                OutputData output = predictionEngine.Predict(new InputData() { Image = MLImage.CreateFromFile(filePath) });

                var res = output.Scores.ToList().IndexOf(output.Scores.Max());

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}