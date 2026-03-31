using Microsoft.ML;
using PokerDice.AI.DataModel;

namespace PokerDice.AI
{
    public class TrainModel
    {
        public void CreateAndSaveTo(string path)
        {
            var ml = new MLContext(seed: 42);
            ml.GpuDeviceId = 0;
            ml.FallbackToCpu = true;

            // Load data
            var data = ml.Data.LoadFromEnumerable(Training.Training.GenerateTrainingData(10_000)); //100_000

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
                  .Append(ml.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                      labelColumnName: "Label",
                      featureColumnName: "Features"))
                  .Append(ml.Transforms.Conversion.MapKeyToValue("PredictedAction", "PredictedLabel"));

            var model = pipeline.Fit(data);

            ml.Model.Save(model, data.Schema, path);
        }
    }
}
