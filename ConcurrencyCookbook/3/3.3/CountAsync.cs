using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class CountAsync
    {
        static async Task Main(string[] args)
        {
            CountAsync usingLinqWithAsyncStreams = new CountAsync();

            int count = await usingLinqWithAsyncStreams.SlowRange().CountAsync();
            Console.WriteLine($@"The total count is without any await operation is {count}");
            Console.WriteLine("\n");

            int countAwait = await usingLinqWithAsyncStreams.SlowRange().CountAwaitAsync(async x =>
            {
                Console.WriteLine($@"The value of i {x} evaluated in CountAwait");
                await Task.Delay(1000);
                return x % 2 == 0;
            });
            Console.WriteLine($@"The total count is with await operation is {countAwait}");
        }

        async IAsyncEnumerable<int> SlowRange()
        {
            for (int i = 0; i != 5; i++)
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
