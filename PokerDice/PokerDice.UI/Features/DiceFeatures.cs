namespace PokerDice.UI.Features
{
    public class DiceFeatures
    {
        private readonly MainForm _mainForm;

        public DiceFeatures(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public void Execute()
        {
            var selectedIndexes = _mainForm.dicesPanel
                            .Controls
                            .OfType<CheckBox>()
                            .Where(c => c.Checked)
                            .Select(c => int.Parse(c.Name))
                            .ToList();

            // re-roll selected dices
            foreach (var index in selectedIndexes)
            {
                _mainForm.context.Dice[index - 1] = _mainForm.engine.SourceGenerator.GenerateDie();
            }

            var unselectedIndexes = _mainForm.dicesPanel
                .Controls
                .OfType<CheckBox>()
                .Where(c => !c.Checked)
                .Select(c => int.Parse(c.Name))
                .ToList();

            // freeze not selected dices
            foreach (var unselected in unselectedIndexes)
            {
                var checkbox = _mainForm.dicesPanel.Controls.OfType<CheckBox>().First(c => c.Name == unselected.ToString());
                checkbox.Enabled = false;
            }

            // refresh enabled checkboxes text
            var unenabledCheckboxes =
                _mainForm.dicesPanel
                .Controls
                .OfType<CheckBox>()
                .Where(c => c.Enabled)
                .Select(c => int.Parse(c.Name))
                .ToList();

            foreach (var ue in unenabledCheckboxes)
            {
                var checkbox = _mainForm.dicesPanel.Controls.OfType<CheckBox>().First(c => c.Name == ue.ToString());
                checkbox.Text = _mainForm.context.Dice[ue - 1].ToString();
                checkbox.Checked = false;
            }
        }
    }
}
