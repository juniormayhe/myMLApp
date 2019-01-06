using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Runtime.Data;
using Microsoft.ML.Transforms.Conversions;

using myMLApp.DataStructures.Input;
using myMLApp.DataStructures.Output;
using myMLApp.Mappers;

namespace myMLApp
{
    public static class MLHelper
    {
        public static IDataView LoadTrainingData<TSrc>(this MLContext mlContext, string dataPath, TSrc inputData)
        {
            // If working in Visual Studio, make sure the 'Copy to Output Directory'
            // property of iris-data.txt is set to 'Copy always'
            IMapper mapper = new FlowerTypeMapper();
            if (inputData is AdequacyLevelData)
            {
                mapper = new AdequacyLevelMapper();
            }
            TextLoader.Arguments args = mapper.Map(inputData);

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
        /// <typeparam name="TSrc"></typeparam>
        /// <typeparam name="TDst"></typeparam>
        /// <param name="dataPath"></param>
        /// <param name="parameters"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static TDst Predict<TSrc, TDst>(string dataPath, string[] parameters, TSrc inputData)
            where TSrc : class
            where TDst : class, new()
        {
            // Create a ML.NET environment  
            MLContext mlContext = new MLContext();

            IDataView trainingDataView = mlContext.LoadTrainingData<TSrc>(dataPath, inputData);
            EstimatorChain<KeyToValueMappingTransformer> learningPipeline = mlContext.GetLearningPipeline(parameters);
            TransformerChain<KeyToValueMappingTransformer> model = trainingDataView.TrainModel(learningPipeline);

            PredictionFunction<TSrc, TDst> prediction = model.MakePredictionFunction<TSrc, TDst>(mlContext);

            TDst result = prediction.Predict(inputData);
            return result;
        }
    }
}