using System;
using System.Collections.Generic;
using System.Text;
using myMLApp.DataStructures.Input;
using myMLApp.DataStructures.Output;

namespace myMLApp.Strategies
{
    public class AdequacyLevelPredictionStrategy : IPredictionStrategy
    {
        private readonly InputData inputData;
        private readonly string dataPath = "adequacy-data.txt";
        private readonly string[] parameters = new string[] { "YearsInAgile", "YearsInNET", "YearsInSQL", "AdequacyLevelInAgile", "AdequacyLevelInNET", "AdequacyLevelInSQL" };
        public AdequacyLevelPredictionStrategy(InputData inputData)
        {
            this.inputData = inputData;
        }

        public void Predict()
        {
            AdequacyLevelPrediction prediction = MLHelper.Predict<AdequacyLevelData, AdequacyLevelPrediction>(dataPath, parameters, (AdequacyLevelData)inputData);
            Console.WriteLine($" - Adequacy level for {inputData.ToString()} is: {prediction.PredictedLabels}");
        }
    }
}
