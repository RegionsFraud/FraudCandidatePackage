using MicroService.Commands;
using MicroService.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Services
{
    public class MyService
    {
        private ParallelAsync? _parallelAsync;

        private async Task<int> CallService(int delay)
        {
            await Task.Delay(delay);
            return 1000;
        }

        public async Task<CheckDTO[]> ExecuteRules(Request request, CancellationToken cancellationToken)
        {
            _parallelAsync = new ParallelAsync();
            
            // initialization
            var checkDeposits = new ConcurrentBag<CheckDTO>();
            var checkRecordIndices = new ConcurrentDictionary<Check, int>();

            int checkIndex = 1;
            foreach (Check checkMap in request.Checks)
            {
                checkRecordIndices.TryAdd(checkMap, checkIndex++);
            }

            await Parallel.ForEachAsync(request.Checks, _parallelAsync.Options(token: cancellationToken),
                async (checkDeposit, cancellationToken) =>
                {
                    // Call One Service
                    var ewsTask = CallService(1000);

                    // Call next Service
                    var iirtTask = CallService(1500);

                    Task.WaitAll(ewsTask, iirtTask);

                    var deposit = new CheckDTO()
                    {
                        Index = checkRecordIndices[checkDeposit],
                        Check = checkDeposit,
                        EWSData = ewsTask.Result,
                        IIRTData = iirtTask.Result,
                    };
                    checkDeposits.Add(deposit);
                })
                .WaitAsync(cancellationToken);
            return checkDeposits.ToArray();
        }
    }
}
