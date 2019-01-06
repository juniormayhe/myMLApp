using myMLApp.DataStructures.Input;

namespace myMLApp.AbstractFactories
{
    public class ConcreteFactory : IAbstractFactory
    {
        public InputData CreateInputData(float sepalLength, float sepalWidth, float petalLength, float petalWidth)
        {
            var flowerTypeData = new FlowerTypeData()
            {
                SepalLength = sepalLength,
                SepalWidth = sepalWidth,
                PetalLength = petalLength,
                PetalWidth = petalWidth,
            };

            return flowerTypeData;   
        }

        public InputData CreateInputData(float yearsInAgile, float yearsInNET, float yearsInSQL, float adequacyLevelInAgile, float adequacyLevelInNET, float adequacyLevelInSQL)
        {
            var adequacyLevelData = new AdequacyLevelData()
            {
                YearsInAgile = yearsInAgile,
                YearsInNET = yearsInNET,
                YearsInSQL = yearsInSQL,
                AdequacyLevelInAgile = adequacyLevelInAgile,
                AdequacyLevelInNET = adequacyLevelInNET,
                AdequacyLevelInSQL = adequacyLevelInSQL
            };
            return adequacyLevelData;
        }
    }
}
