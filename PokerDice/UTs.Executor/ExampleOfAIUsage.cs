using PokerDice.AI;
using Vortice.DXGI;

namespace UTs.Executor
{
    public class ExampleOfAIUsage
    {
        [Fact]
        public void Execute()
        {
            //given
            var fileName = "r:\\model.zip";
            File.Delete(fileName);

            //when
            var modelTrainer = new TrainModel();
            modelTrainer.CreateAndSaveTo(fileName);

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
