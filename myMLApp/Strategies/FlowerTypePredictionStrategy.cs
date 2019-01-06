using System;

using myMLApp.DataStructures.Input;
using myMLApp.DataStructures.Output;

namespace myMLApp.Strategies
{
    public class FlowerTypePredictionStrategy : IPredictionStrategy
    {
        private readonly InputData inputData;
        private readonly string dataPath = "iris-data.txt";
        private readonly string[] parameters = new string[] { "SepalLength", "SepalWidth", "PetalLength", "PetalWidth" };

        public FlowerTypePredictionStrategy(InputData inputData)
        {
            this.inputData = inputData;
        }

        public void Predict()
        {
            FlowerTypePrediction prediction = MLHelper.Predict<FlowerTypeData, FlowerTypePrediction>(dataPath, parameters, (FlowerTypeData)inputData);
            Console.WriteLine($" - Predicted flower type for {inputData.ToString()} is: {prediction.PredictedLabels}");
        }
    }
}
