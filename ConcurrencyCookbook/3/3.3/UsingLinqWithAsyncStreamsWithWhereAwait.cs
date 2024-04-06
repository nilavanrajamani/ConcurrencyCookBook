using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class UsingLinqWithAsyncStreamsWithWhereAwait
    {
        static async Task Main(string[] args)
        {
            UsingLinqWithAsyncStreamsWithWhereAwait usingLinqWithAsyncStreams = new UsingLinqWithAsyncStreamsWithWhereAwait();
            IAsyncEnumerable<int> values = usingLinqWithAsyncStreams.SlowRange().WhereAwait(async x =>
            {
                Console.WriteLine($@"The value of i {x} evaluated in WhereAwait");
                await Task.Delay(1000);
                return x % 2 == 0;
            });
            await foreach (var item in values)
            {
                Console.WriteLine($@"The value of i in main is {item} at {DateTime.Now}");
            }
        }

        async IAsyncEnumerable<int> SlowRange()
        {
            for (int i = 0; i != 10; i++)
            {
                Console.WriteLine($@"The value of i {i} at the start of SlowRange at {DateTime.Now}");
                await Task.Delay(1000);
                Console.WriteLine($@"The value of i {i} after await of SlowRange at {DateTime.Now}");
                yield return i;
                Console.WriteLine($@"The value of i {i} after yield at {DateTime.Now}");
            }
        }
    }
}
