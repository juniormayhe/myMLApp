namespace myMLApp.DataStructures.Input
{
    // STEP 1: Define your data structures
    // IrisData is used to provide training data, and as
    // input for prediction operations
    // - First 4 properties are inputs/features used to predict the label
    // - Label is what you are predicting, and is only set when training
    public class FlowerTypeData : InputData
    {
        public float SepalLength;
        public float SepalWidth;
        public float PetalLength;
        public float PetalWidth;
        public string Label;

        public override string ToString()
        {
            return $"SepalLength {SepalLength}, SepalWidth {SepalWidth}, PetalLength {PetalLength}, PetalWidth {PetalWidth}";
        }
    }
}
