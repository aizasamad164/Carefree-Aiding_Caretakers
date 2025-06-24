using Caretaker_System_3;
using Microsoft.ML;

namespace MLModel
{
    public class MLModel
    {
        public static ITransformer? trainedModel;

        public static void TrainAndPredict()
        {
            var context = new MLContext();
            IDataView data = context.Data.LoadFromTextFile <InsuranceInfo> ("D:/Aiza-NED/Object Oriented Programming/insurance.csv", separatorChar: ',', hasHeader: true);
            var TrainTestData = context.Data.TrainTestSplit(data, testFraction: 0.2);
            IDataView trainingData = TrainTestData.TrainSet;
            IDataView testingData = TrainTestData.TestSet;

            var dataProcessPipeline = context.Transforms.Categorical.OneHotEncoding("RegionEncoded", "Region")
            .Append(context.Transforms.Categorical.OneHotEncoding("SexEncoded", "Sex"))
            .Append(context.Transforms.Categorical.OneHotEncoding("SmokerEncoded", "Smoker"))
            .Append(context.Transforms.Concatenate("Features", "Age", "SexEncoded", "SmokerEncoded", "BMI", "RegionEncoded", "Children"))
            .Append(context.Transforms.CopyColumns("Label", "Charges"));

            var trainer = context.Regression.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features", learningRate: 0.04);
            var trainingpipeline = dataProcessPipeline.Append(trainer);
            var model = trainingpipeline.Fit(trainingData);
            trainedModel = model;

            IDataView predictions = model.Transform(testingData);
            var evaluation_metrics = context.Regression.Evaluate(predictions, labelColumnName: "Charges");
        }

        public static float PredictFromDatabaseEntry(InsuranceInfo dbEntry)
        {
            if (trainedModel == null)
            {
                throw new InvalidOperationException("Model not trained yet");
            }

            var context = new MLContext();
            var predictionEngine = context.Model.CreatePredictionEngine<InsuranceInfo, InsurancePrediction>(trainedModel);
            var prediction = predictionEngine.Predict(dbEntry);

            return prediction.PredictedCharges;
        }       
    }
}