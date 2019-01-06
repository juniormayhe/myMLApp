using myMLApp.DataStructures.Input;

namespace myMLApp.AbstractFactories
{
    interface IAbstractFactory
    {
        InputData CreateInputData(float sepalLength, float sepalWidth, float petalLength, float petalWidth);

        InputData CreateInputData(float yearsInAgile, float yearsInNET, float yearsInSQL, float adequacyLevelInAgile, float adequacyLevelInNET, float adequacyLevelInSQL);
    }
}
