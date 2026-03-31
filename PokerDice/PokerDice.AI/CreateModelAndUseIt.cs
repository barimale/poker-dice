using Microsoft.ML;
using PokerDice.AI.DataModel;
using PokerDice.AI.Training;
using Microsoft.ML.Data;

namespace PokerDice.AI
{
    public class CreateModelAndUseIt
    {
        public void Main()
        {
            var ml = new MLContext(seed: 42);
            ml.GpuDeviceId = null; // ensure CPU
            ml.FallbackToCpu = true;
            //ml./*MaxThreads*/ = Environment.ProcessorCount;
            // Load data
            var data = ml.Data.LoadFromEnumerable(Training.Training.GenerateTrainingData(10000)); //100_000

            // Build pipeline
            var pipeline =
                ml.Transforms.Conversion.MapValueToKey("Label", nameof(DiceState.Action))
                  .AppendCacheCheckpoint(ml)
                  .Append(ml.Transforms.Concatenate("Features",
                      nameof(DiceState.Die1),
                      nameof(DiceState.Die2),
                      nameof(DiceState.Die3),
                      nameof(DiceState.Die4),
                      nameof(DiceState.Die5),
                      nameof(DiceState.RollIndex)))
                  .Append(ml.MulticlassClassification.Trainers.LbfgsMaximumEntropy(
                      labelColumnName: "Label",
                      featureColumnName: "Features"))
                  .Append(ml.Transforms.Conversion.MapKeyToValue("PredictedAction", "PredictedLabel"));

            var model = pipeline.Fit(data);

            ml.Model.Save(model, data.Schema, "r:\\model.zip");

            // Prediction engine
            // Define DataViewSchema and ITransformers
            DataViewSchema modelSchema;
            ITransformer trainedModel = ml.Model.Load("r:\\model.zip", out modelSchema);

            var engine = ml.Model.CreatePredictionEngine<DiceState, DiceActionPrediction>(trainedModel); // model
            var sample = new DiceState
            {
                Die1 = 2,
                Die2 = 4,
                Die3 = 3,
                Die4 = 5,
                Die5 = 6,
                RollIndex = 3
            };

            var pred = engine.Predict(sample);
            Console.WriteLine($"Action: {pred.PredictedAction}");
        }
    }
}
