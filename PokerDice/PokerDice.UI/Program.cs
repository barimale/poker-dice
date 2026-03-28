using PokerDice.UI.Factory;

namespace PokerDice.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(FormFactory.CreateForm());
        }
    }
}