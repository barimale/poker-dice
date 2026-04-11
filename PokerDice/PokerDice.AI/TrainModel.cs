using Microsoft.ML;
using PokerDice.AI.DataModel;
using System.Runtime;

namespace PokerDice.AI
{
    public class TrainModel
    {
        public Action<int, double, string, int[]> OnIterateChange;

        public TrainModel WithLatencyOff()
        {
            GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

            return this;
        }

        public void CreateAndSaveTo(int samples, string path)
        {
            var ml = new MLContext(seed: 42);
            ml.GpuDeviceId = null; // 0; WITHOUT GPU
            ml.FallbackToCpu = true;

            // Load data
            var training = new Training.Training();
            training.OnIterateChange += OnIterateChange;
            var data = ml.Data.LoadFromEnumerable(samples < 0 ? training.GenerateTrainingData() :  training.GenerateTrainingData(samples));

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
                  .Append(ml.MulticlassClassification.Trainers.LightGbm(
                      labelColumnName: "Label",
                      featureColumnName: "Features"))
                  .Append(ml.Transforms.Conversion.MapKeyToValue("PredictedAction", "PredictedLabel"));

            var model = pipeline.Fit(data);

            ml.Model.Save(model, data.Schema, path);
        }
    }
}
