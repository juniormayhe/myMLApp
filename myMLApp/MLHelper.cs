using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Transforms.Conversions;

using myMLApp.DataStructures.Input;
using myMLApp.DataStructures.Output;

namespace myMLApp
{
    public static class MLHelper
    {
        public static IDataView LoadTrainingData(this MLContext mlContext, string dataPath, object inputData)
        {
            // If working in Visual Studio, make sure the 'Copy to Output Directory'
            // property of iris-data.txt is set to 'Copy always'
            TextLoader.Arguments args;
            if (inputData is IrisData)
            {
                args = new TextLoader.Arguments()
                {
                    Separator = ",",
                    HasHeader = true,
                    Column = new[]
                    {
                        new TextLoader.Column("SepalLength", DataKind.R4, 0),
                        new TextLoader.Column("SepalWidth", DataKind.R4, 1),
                        new TextLoader.Column("PetalLength", DataKind.R4, 2),
                        new TextLoader.Column("PetalWidth", DataKind.R4, 3),
                        new TextLoader.Column("Label", DataKind.Text, 4)
                    }
                };
            }
            else
            {
                args = new TextLoader.Arguments()
                {
                    Separator = ",",
                    HasHeader = true,
                    Column = new[]
                    {
                        new TextLoader.Column("YearsInAgile", DataKind.R4, 0),
                        new TextLoader.Column("YearsInNET", DataKind.R4, 1),
                        new TextLoader.Column("YearsInSQL", DataKind.R4, 2),
                        new TextLoader.Column("AdequacyLevelInAgile", DataKind.R4, 3),
                        new TextLoader.Column("AdequacyLevelInNET", DataKind.R4, 4),
                        new TextLoader.Column("AdequacyLevelInSQL", DataKind.R4, 5),
                        new TextLoader.Column("Label", DataKind.Text, 6)
                    }
                };
            }
            TextLoader textLoader = mlContext.Data.TextReader(args);
            IDataView trainingDataView = textLoader.Read(new MultiFileSource(dataPath));
            return trainingDataView;
        }

        /// <summary>
        /// Transform your data and add a learner
        /// Assign numeric values to text in the "Label" column, because only numbers can be processed during model training.
        /// Add a learning algorithm to the pipeline. e.g.(What type of iris is this?)
        /// Convert the Label back into original text (after converting to number in step 3)
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static EstimatorChain<KeyToValueMappingTransformer> GetLearningPipeline(this MLContext mlContext, params string[] parameters)
        {
            return mlContext.Transforms.Conversion.MapValueToKey("Label")
                            .Append(mlContext.Transforms.Concatenate("Features", parameters))
                            .Append(mlContext.MulticlassClassification.Trainers.StochasticDualCoordinateAscent(labelColumn: "Label", featureColumn: "Features", maxIterations: 20000))
                            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
        }


        /// <summary>
        /// Train your model based on the data set  
        /// </summary>
        /// <param name="trainingDataView"></param>
        /// <param name="pipeline"></param>
        /// <returns></returns>
        public static TransformerChain<KeyToValueMappingTransformer> TrainModel(this IDataView trainingDataView, EstimatorChain<KeyToValueMappingTransformer> pipeline)
        {
            return pipeline.Fit(trainingDataView);
        }

        /// <summary>
        /// Use your model to make a prediction
        /// You can change these numbers to test different predictions
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="model"></param>
        public static object GetPrediction(this MLContext mlContext,
            TransformerChain<Microsoft.ML.Transforms.Conversions.KeyToValueMappingTransformer> model,
            object inputData)
        {

            if (inputData is AdequacyLevelData)
            {
                var prediction = model.MakePredictionFunction<AdequacyLevelData, AdequacyLevelPrediction>(mlContext).Predict(inputData as AdequacyLevelData);
                return prediction;
            }
            else
            {
                var prediction = model.MakePredictionFunction<IrisData, IrisPrediction>(mlContext).Predict(inputData as IrisData);
                return prediction;
            }
        }

        public static object Predict(object inputData)
        {
            // Create a ML.NET environment  
            MLContext mlContext = new MLContext();

            IDataView trainingDataView;
            string dataPath = "";
            string[] parameters;

            if (inputData is IrisData)
            {
                dataPath = "iris-data.txt";
                parameters = new string[] { "SepalLength", "SepalWidth", "PetalLength", "PetalWidth" };
            }
            else
            {
                dataPath = "adequacy-data.txt";
                parameters = new string[] { "YearsInAgile", "YearsInNET", "YearsInSQL", "AdequacyLevelInAgile", "AdequacyLevelInNET", "AdequacyLevelInSQL" };
            }

            trainingDataView = mlContext.LoadTrainingData(dataPath, inputData);

            EstimatorChain<KeyToValueMappingTransformer> learningPipeline = mlContext.GetLearningPipeline(parameters);
            TransformerChain<KeyToValueMappingTransformer> model = trainingDataView.TrainModel(learningPipeline);

            if (inputData is IrisData)
            {
                IrisPrediction prediction = (IrisPrediction)mlContext.GetPrediction(model, inputData);
                return prediction;
            }
            else
            {
                AdequacyLevelPrediction prediction = (AdequacyLevelPrediction)mlContext.GetPrediction(model, inputData);
                return prediction;
            }
        }
    }
}
