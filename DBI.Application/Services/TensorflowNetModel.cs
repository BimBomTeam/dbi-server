using DBI.Infrastructure.Services;
using Microsoft.VisualBasic;
using Tensorflow;
using Tensorflow.Keras;
using Tensorflow.Keras.ArgsDefinition;
using Tensorflow.Keras.Engine;
using Tensorflow.Keras.Layers;
using Tensorflow.NumPy;

namespace DBI.Application.Services
{
    public class ImageData
    {
        public float[] Image { get; set; }
    }
    public class ImagePrediction
    {
        // Depending on your model's output schema, this might change
        public float[] Score { get; set; }
    }
    public class TensorflowNetModel : IAiModelService
    {
        tensorflow tf;

        public TensorflowNetModel()
        {
            tf = new tensorflow();
        }

        public async Task<int> IdentifyAsync(string base64)
        {
            try
            {
                string weightPath = @"E:\my_saved_model\";

                //tf.reset_default_graph();
                //tf.compat.v1.disable_eager_execution();
                //tf.variable_scope("other");
                //tf.name_scope("other");

                var model = new Sequential(new SequentialArgs());


                IModel modelTmp = KerasApi.keras.models.load_model(weightPath);
                //var res = modelTmp.predict(new Tensor(base64));


                NDArray nd = new NDArray(Convert.FromBase64String(base64), new Shape(300, 300, 3));
                Tensor t = new Tensor(nd);
                var t1 = tf.image.decode_image(t, 3, name: tf.no_op().name);
                var res = modelTmp.predict(t1);
                return (int)res.Max();
                //model.add(new InputLayer(new InputLayerArgs() { InputShape = new Shape(300, 300, 3) }));
                //model.add(new Conv2D(new Conv2DArgs()
                //{
                //    InputShape = new Shape(300, 300, 3),
                //    Filters = 128,
                //    KernelSize = new Shape(3, 3),
                //    Activation = new Activations().Relu,
                //    DilationRate = new Shape(1, 1),
                //}));

                //    Strides = new Shape(1, 1),
                //    KernelInitializer = new InitializersApi().HeNormal(),
                //    InputShape = new Shape(300, 300, 3),
                //    Filters = 128,
                //    KernelSize = new Shape(3, 3),
                //    Activation = new Activations().Relu,
                //    DilationRate = new Shape(1, 1),
                //    Padding = "same",
                //    KernelRegularizer = new Regularizers().l2()
                //model.add(new MaxPooling2D(new MaxPooling2DArgs()
                //{
                //    PoolSize = 2
                //}));
                //model.add(new Flatten(new FlattenArgs()));
                //model.add(new Dense(new DenseArgs()
                //{
                //    Units = 3,
                //    Activation = new Activations().Softmax,
                //}));

                //List<ILayer> layers = new List<ILayer>
                //{
                //    ,
                //    ,
                //    ,

                //};

            }
            catch (Exception ex)
            {
                throw ex;
                //return -1;
            }

            //////asd

            // Load TensorFlow model
            //var model = new Temsprf;.Graph();
            //var modelPath = "path_to_your_model.pb"; // Adjust to your model's path
            //model.Import(File.ReadAllBytes(modelPath));

            //// Decode Base64 to Image
            //byte[] imageBytes = Convert.FromBase64String(base64);
            //Image image;
            //using (var ms = new MemoryStream(imageBytes))
            //{
            //    image = Image.FromStream(ms);
            //}

            //// Preprocess the image as per your model's requirement
            //Bitmap bitmap = new Bitmap(image, new Size(224, 224)); // Example resize

            //var tensor = new Tensor(imageBytes.ToArray()); // Adjust preprocessing as needed

            //using (var session = new Session(model))
            //{
            //    var runner = session.Add();
            //    runner.AddInput(model["input"][0], tensor); // Adjust input layer name
            //    runner.Fetch(model["output"][0]); // Adjust output layer name

            //    var output = runner.Run();
            //    // Process the output to get predictions
            //}

            return 1;
        }
        //    public async Task<int> IdentifyAsync2(string base64)
        //    {
        //        // Your base64 image string

        //        // Convert base64 to byte array
        //        byte[] imageBytes = Convert.FromBase64String(base64);

        //        // Load and process the image
        //        float[] imagePixels = ConvertToTensor(imageBytes);

        //        // Load TensorFlow model
        //        var mlContext = new MLContext();
        //        string modelPath = "path_to_your_model.pb";
        //        var model = mlContext.Model.Load(modelPath);

        //        // Define data schema and setup pipeline
        //        var pipeline = model.ScoreTensorFlowModel(outputColumnNames: new[] { "output" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true);

        //        // Create prediction engine
        //        var predictionEngine = mlContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(pipeline.Fit(mlContext.Data.LoadFromEnumerable(new ImageData[] { })));

        //        // Predict
        //        var prediction = predictionEngine.Predict(new ImageData { Image = imagePixels });

        //        // Output (this will depend on your model's output)
        //        //Console.WriteLine($"Prediction: {prediction.Score.First()}");

        //        return 1;
        //    }
        //    static float[] ConvertToTensor(byte[] imageBytes)
        //    {
        //        // Load the image
        //        using var image = Image.Load<Rgb24>(imageBytes);

        //        // Resize and convert to grayscale if needed
        //        image.Mutate(x => x.Resize(224, 224)); // Resize to the size expected by your model

        //        // Convert to pixel array
        //        var pixelSpan = image.GetPixelSpan();
        //        float[] pixelArray = new float[pixelSpan.Length * 3]; // Assuming a color image (RGB)

        //        for (int i = 0; i < pixelSpan.Length; i++)
        //        {
        //            pixelArray[i * 3 + 0] = pixelSpan[i].R;
        //            pixelArray[i * 3 + 1] = pixelSpan[i].G;
        //            pixelArray[i * 3 + 2] = pixelSpan[i].B;
        //        }

        //        return pixelArray;
        //    }

        //}
    }
}