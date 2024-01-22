using Microsoft.ML.Data;

namespace DBI.Application.Services.MlNet
{
    public class OutputDataRecognition
    {
        [ColumnName("dense_1")]
        public float[] Scores { get; set; }
    }
}
