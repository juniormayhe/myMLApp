using Microsoft.ML.Runtime.Api;

namespace myMLApp.DataStructures.Output
{
    /// <summary>
    /// AdequacyLevelPrediction is the result returned from prediction operations
    /// </summary>
    public class AdequacyLevelPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
