namespace PokerDice.UI.Modules
{
    public class MainButtonFeatures
    {
        private readonly MainForm _mainForm;

        public MainButtonFeatures(MainForm mainForm)
        {
            _mainForm = mainForm;
        }

        public void ApplyButtonEnabledStyle(Button button, Color enabledColor, bool changeFont)
        {
            if (button == null) return;

            if (button.Enabled)
            {
                button.BackColor = enabledColor;
                button.ForeColor = Color.White;
                if (changeFont)
                {
                    button.Font = new Font(button.Font, FontStyle.Bold);
                }
            }
            else
            {
                button.BackColor = SystemColors.ControlDark;
                button.ForeColor = SystemColors.ControlText;
                if (changeFont)
                {
                    button.Font = new Font(button.Font, FontStyle.Regular);
                }
            }
        }
    }
}
