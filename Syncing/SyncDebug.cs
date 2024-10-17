using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        public List<string> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();

            // IMHO the following is the simplest solution
            // (and works on older versions of .NET)
            // -- but it would require making this method async,
            // which in turn would require changing the return type to Task<List<string>>
            //var tasks = items.Select(async item =>
            //{
            //    var result = await Task.Run(() => item).ConfigureAwait(false);
            //    bag.Add(result);
            //});
            //await Task.WhenAll(tasks);

            // normally I would `await` the call to `Parallel.ForEachAsync()`
            // -- but again that would require making the method async, etc, etc
            Parallel.ForEachAsync(items, async (item, token) =>
            {
                if (token.IsCancellationRequested) return;

                var result = await Task.Run(() => item).ConfigureAwait(false);
                bag.Add(result);
            })
            .GetAwaiter().GetResult();

            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var dictionaryLock = new Object();
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();

            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var threads = Enumerable.Range(0, 3)
                .Select(i => new Thread(() => {
                    foreach (var item in itemsToInitialize)
                    {
                        // since the test itself is counting the # of `getItem()` invocations,
                        // we need to ensure we only try to insert each key once
                        if (!concurrentDictionary.ContainsKey(item))
                        {
                            lock (dictionaryLock)
                            {
                                if (!concurrentDictionary.ContainsKey(item))
                                    concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                            }
                        }
                    }
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}