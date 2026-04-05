using PokerDice.AI;
using System.Runtime;
using UTs.Executor.BaseUT;
using Vortice.DXGI;
using Xunit.Abstractions;

namespace UTs.Executor
{
    public class ExampleOfAIUsage: PrintToConsoleUTBase
    {
        private readonly TestOutputTextWriter _redirectWriter;

        public ExampleOfAIUsage(ITestOutputHelper output)
            : base(output)
        {
            _redirectWriter = new TestOutputTextWriter(output);
            Console.SetOut(_redirectWriter);
        }

        [Fact]
        public void Execute()
        {
            //given
            var fileName = "r:\\model-XL.zip";
            var logFileName = "r:\\training_log-XL.txt";
            var amountOfIterations = 200_000;
            File.Delete(fileName);
            File.Delete(logFileName);

            //when
            var modelTrainer = new TrainModel()
                .WithLatencyOff();

            modelTrainer.OnIterateChange += (i,progress, bestAction, dice) =>
            {
                try
                {
                    var dices = string.Join(',', dice);
                    var line = $"Iteration {i}, progress: {progress}%, best action: {bestAction}, dices: {dices}";
                    Console.WriteLine(line);
                    File.AppendAllText(logFileName, line + Environment.NewLine);
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to write to log file.");
                }
            };
            modelTrainer.CreateAndSaveTo(amountOfIterations, fileName);

            //then
            Assert.True(File.Exists(logFileName));
            Assert.True(File.Exists(fileName));
        }

        [Fact]
        public void RecognizeGPUDevices()
        {
            //given

            //when
            using var factory = DXGI.CreateDXGIFactory1<IDXGIFactory1>();

            uint index = 0;
            IDXGIAdapter1 adapter;

            while (factory.EnumAdapters1(index, out adapter).Success)
            {
                var desc = adapter.Description1;
                Console.WriteLine($"GPU {index}: {desc.Description} - VRAM: {desc.DedicatedVideoMemory / (1024 * 1024)} MB");
                index++;
            }

            //then
        }
    }
}
