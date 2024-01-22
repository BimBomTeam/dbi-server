using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;

namespace DBI.Application.Services.MlNet
{
    public class InputDataRecognition
    {
        [ColumnName("efficientnet-b1_input")]
        [ImageType(224, 224)]
        public MLImage Image { get; set; }
    }
}
