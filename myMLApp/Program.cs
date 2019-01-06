using System;
using System.Collections.Generic;
using myMLApp.AbstractFactories;
using myMLApp.DataStructures.Input;
using myMLApp.DataStructures.Output;
using myMLApp.Mappers;
using myMLApp.Strategies;

namespace myMLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Organizing input data...");
            
            Dictionary<Type, List<IPredictionStrategy>> dictionary = BuildInputData();

            foreach (var item in dictionary)
            {
                Console.WriteLine($"\nPredicting for {item.Key.Name}...");
                List<IPredictionStrategy> strategies = item.Value;
                foreach (var strategy in strategies)
                {
                    strategy.Predict();
                }
            }
            Console.WriteLine("\nDone");
            Console.ReadKey();
        }

        private static Dictionary<Type, List<IPredictionStrategy>> BuildInputData()
        {
            IAbstractFactory abstractFactory = new ConcreteFactory();
            var dictionary = new Dictionary<Type, List<IPredictionStrategy>>();

            var irisToPredict = new List<IPredictionStrategy> {
                new FlowerTypePredictionStrategy(abstractFactory.CreateInputData(3.3f, 1.6f, 0.2f, 5.1f)),
                new FlowerTypePredictionStrategy(abstractFactory.CreateInputData(0.2f, 0.5f, 0.2f, 0.5f)),
                new FlowerTypePredictionStrategy(abstractFactory.CreateInputData(1.3f, 0.6f, 0.4f, 1.0f))
            };
            dictionary.Add(typeof(FlowerTypeData), irisToPredict);

            var adequacyLevelsToPredict = new List<IPredictionStrategy> {
                new AdequacyLevelPredictionStrategy(abstractFactory.CreateInputData(2.1f, 3.2f, 2.2f, 210.00f, 64f, 110f)),
                new AdequacyLevelPredictionStrategy(abstractFactory.CreateInputData(1f, 4.9f, 2f, 100f, 98.0f, 40f)),
                new AdequacyLevelPredictionStrategy(abstractFactory.CreateInputData(0.4f, 1.9f, 0f, 40.00f, 38f, 0f)),
                new AdequacyLevelPredictionStrategy(abstractFactory.CreateInputData(0.3f, 4.2f, 1.2f, 30f, 84.0f, 24f)),
                new AdequacyLevelPredictionStrategy(abstractFactory.CreateInputData(0.9f, 4.8f, 1.8f, 90.00f, 96f, 36f))
            };
            dictionary.Add(typeof(AdequacyLevelData), adequacyLevelsToPredict);
            return dictionary;
        }
    }
}
