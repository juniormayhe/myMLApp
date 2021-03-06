﻿using Microsoft.ML.Runtime.Api;

namespace myMLApp.DataStructures.Output
{
    /// <summary>
    /// IrisPrediction is the result returned from prediction operations
    /// </summary>
    public class FlowerTypePrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabels;
    }
}
