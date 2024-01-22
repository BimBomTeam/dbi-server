using Microsoft.ML.Data;

namespace DBI.Application.Services.MlNet
{
    internal class OutputData
    {
        [ColumnName("dense_2")]
        public float[] Scores { get; set; }
    }
}
