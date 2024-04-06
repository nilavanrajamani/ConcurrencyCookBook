using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class UsingLinqWithAsyncStreamsWithoutWhereAwait
    {
        static async Task Main(string[] args)
        {
            UsingLinqWithAsyncStreamsWithoutWhereAwait usingLinqWithAsyncStreams = new UsingLinqWithAsyncStreamsWithoutWhereAwait();

            //The below line produces asynchronous stream even if we not use WhereAwait and just Where
            IAsyncEnumerable<int> values = usingLinqWithAsyncStreams.SlowRange().Where(x =>
            {
                Console.WriteLine($@"The value of i {x} evaluated in WhereAwait");
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
