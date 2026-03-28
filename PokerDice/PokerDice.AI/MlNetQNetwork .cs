using Microsoft.ML;
using Microsoft.ML.Data;

namespace PokerDice.AI
{

    public sealed class MlNetQNetwork : IQNetwork
    {
        private readonly MLContext _ml;
        private readonly int _actionCount;

        private ITransformer _model;
        private PredictionEngine<ModelInput, ModelOutputSingle>[] _engines;

        public MlNetQNetwork(int actionCount)
        {
            _ml = new MLContext(seed: 123);
            _actionCount = actionCount;

            BuildInitialModel();
        }

        private void BuildInitialModel()
        {
            var data = _ml.Data.LoadFromEnumerable(new List<TrainingRow>() { new TrainingRow()
            {
                 Features= [0,0,0,0,0,0,0,0,0,0,0,0],
                 Label = 0
            } });

            var pipeline = _ml.Transforms
                .Concatenate("Features", nameof(TrainingRow.Features))
                .Append(_ml.Transforms.NormalizeMinMax("Features"))
                .Append(_ml.Regression.Trainers.Sdca(
                    labelColumnName: "Label",
                    featureColumnName: "Features"));
            var preview = data.Preview();
            _model = pipeline.Fit(data);
            _engines = new PredictionEngine<ModelInput, ModelOutputSingle>[_actionCount];

            for (int action = 0; action < _actionCount; action++)
            {
                _engines[action] = _ml.Model.CreatePredictionEngine<ModelInput, ModelOutputSingle>(_model);
            }
        }

        public float[] Predict(GameState state)
        {
            var input = new ModelInput { Features = state.Features };
            var scores = new float[_actionCount];

            for (int action = 0; action < _actionCount; action++)
            {
                var output = _engines[action].Predict(input);
                scores[action] = output.Score;
            }

            return scores;
        }

        public void TrainBatch(float[][] states, float[][] targetQs)
        {
            int n = _actionCount;
            var models = new ITransformer[n];

            for (int action = 0; action < n; action++)
            {
                var batch = new List<TrainingRow>(states.Length);
                for (int i = 0; i < states.Length; i++)
                    batch.Add(new TrainingRow { Features = states[i], Label = targetQs[i][action] });

                var data = _ml.Data.LoadFromEnumerable(batch);
                var pipeline = _ml.Transforms
                    .Concatenate("Features", nameof(TrainingRow.Features))
                    .Append(_ml.Transforms.NormalizeMinMax("Features"))
                    .Append(_ml.Regression.Trainers.Sdca(labelColumnName: "Label", featureColumnName: "Features"));

                models[action] = pipeline.Fit(data);
                _engines[action] = _ml.Model.CreatePredictionEngine<ModelInput, ModelOutputSingle>(models[action]);
            }
        }

        private sealed class TrainingRow
        {
            [VectorType(12)]
            public float[] Features { get; set; }

            public float Label { get; set; } // scalar now
        }
    }

    public sealed class ModelInput
    {
        [VectorType(12)]
        public float[] Features { get; set; }
    }

    public sealed class ModelOutputSingle
    {
        public float Score { get; set; }
    }

}
