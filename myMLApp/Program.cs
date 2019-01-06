using System;

using myMLApp.DataStructures.Input;
using myMLApp.DataStructures.Output;

namespace myMLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            processIris();

            processAdequacy();

            Console.ReadKey();
        }

        private static void processIris()
        {
            IrisData irisData = new IrisData()
            {
                SepalLength = 3.3f,
                SepalWidth = 1.6f,
                PetalLength = 0.2f,
                PetalWidth = 5.1f,
            };
            IrisPrediction irisPrediction = (IrisPrediction)MLHelper.Predict(irisData);
            Console.WriteLine($"Predicted flower type is: {irisPrediction.PredictedLabels}");
        }

        private static void processAdequacy()
        {
            var adequacyLevelData = new AdequacyLevelData()
            {
                YearsInAgile = 0.6f,
                YearsInNET = 4.6f,
                YearsInSQL = 1.8f,
                AdequacyLevelInAgile = 60f,
                AdequacyLevelInNET = 92.0f,
                AdequacyLevelInSQL = 90f

            };
            AdequacyLevelPrediction adequacyLevelPrediction = (AdequacyLevelPrediction)MLHelper.Predict(adequacyLevelData);
            Console.WriteLine($"Adequacy level is: {adequacyLevelPrediction.PredictedLabels}");
        }
    }
}
