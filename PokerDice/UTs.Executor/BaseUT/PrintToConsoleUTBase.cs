using Xunit.Abstractions;

namespace UTs.Executor.BaseUT
{
    public abstract class PrintToConsoleUTBase
    {
        protected readonly ITestOutputHelper Output;

        public PrintToConsoleUTBase(ITestOutputHelper output)
        {
            Output = output;
        }

        public void Display(string line)
        {
            Output.WriteLine(line);
        }

        public void Display(string[] lines)
        {
            foreach (var line in lines)
            {
                Display(line);
            }
        }
    }
}
