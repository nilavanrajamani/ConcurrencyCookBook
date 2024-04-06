using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class WithoutUsingLinqWithAsyncStreams
    {
        static async Task Main(string[] args)
        {
            WithoutUsingLinqWithAsyncStreams usingLinqWithAsyncStreams = new WithoutUsingLinqWithAsyncStreams();
            IAsyncEnumerable<int> values = usingLinqWithAsyncStreams.SlowRange();
            await foreach (var item in values)
            {
                await Task.Delay(1000);
                if (item % 2 == 0)
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
