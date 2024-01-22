using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;

namespace DBI.Application.Services.MlNet
{
    public class InputData
    {
        [ColumnName("efficientnet-b0_input")]
        [ImageType(224, 224)]
        public MLImage Image { get; set; }
    }
}
