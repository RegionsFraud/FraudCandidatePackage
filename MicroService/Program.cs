using MicroService.Models;
using MicroService.Services;
using System.Diagnostics;

namespace MicroService
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var p = new Program();
            await p.RunAsync(2);
            await p.RunAsync(5);
            await p.RunAsync(10);
            await p.RunAsync(20);
            await p.RunAsync(30);
            await p.RunAsync(50);
        }

        private async Task RunAsync(int numOfChecks)
        {
            var myService = new MyService();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            await myService.ExecuteRules(GetChecks(numOfChecks), new CancellationToken());
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        private static Request GetChecks(int v)
        {
            var request = new Request();
            for (int i = 0; i < v; i++)
            {
                request.Checks.Add(
                    new Check()
                    {
                        RoutingNumber = "123456789",
                        AccountNumber = "0912345678",
                        Amount = 100
                    });
            }
            return request;
        }
    }
}
