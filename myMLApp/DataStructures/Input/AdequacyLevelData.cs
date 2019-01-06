namespace myMLApp.DataStructures.Input
{
    // STEP 1: Define your data structures
    // IrisData is used to provide training data, and as
    // input for prediction operations
    // - First 4 properties are inputs/features used to predict the label
    // - Label is what you are predicting, and is only set when training
    public class AdequacyLevelData : InputData
    {
        public float YearsInAgile;
        public float YearsInNET;
        public float YearsInSQL;
        public float AdequacyLevelInAgile;
        public float AdequacyLevelInNET;
        public float AdequacyLevelInSQL;
        public string Label;

        public override string ToString()
        {
            return $"YearsInAgile {YearsInAgile}, YearsInNET {YearsInNET}, YearsInSQL {YearsInSQL}, AdequacyLevelInAgile {AdequacyLevelInAgile}, AdequacyLevelInNET {AdequacyLevelInNET}, AdequacyLevelInSQL {AdequacyLevelInSQL}";
        }
    }
}
