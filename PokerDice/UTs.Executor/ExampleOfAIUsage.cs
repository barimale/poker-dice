using PokerDice.AI;
using UTs.Executor.BaseUT;
using Vortice.DXGI;
using Xunit.Abstractions;
using Xunit.Extensions;

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
            var fileName = "r:\\model.zip";
            File.Delete(fileName);

            //when
            var modelTrainer = new TrainModel();
            modelTrainer.OnIterateChange += (i, bestAction) =>
            {
                try
                {
                    Console.WriteLine($"Iteration {i}, best action: {bestAction}");
                    var line = $"Iteration {i}, best action: {bestAction}";
                    File.AppendAllText("r:\\training_log.txt", line + Environment.NewLine);
                }
                catch (Exception)
                {
                    Output.WriteLine("Failed to write to log file.");
                }
            };
            modelTrainer.CreateAndSaveTo(10_000, fileName);

            //then
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
