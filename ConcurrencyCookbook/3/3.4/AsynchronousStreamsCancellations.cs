using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class AsynchronousStreamsCancellations
    {
        static async Task Main(string[] args)
        {
            AsynchronousStreamsCancellations usingLinqWithAsyncStreams = new AsynchronousStreamsCancellations();
            using var cts = new CancellationTokenSource(500);
            CancellationToken cancellationToken = cts.Token;
            IAsyncEnumerable<int> values = usingLinqWithAsyncStreams.SlowRange(cancellationToken);
            await foreach (var item in values.WithCancellation(cancellationToken))
            {
                await Task.Delay(1000);
                if (item % 2 == 0)
                    Console.WriteLine($@"The value of i in main is {item} at {DateTime.Now}");
            }
        }

        async IAsyncEnumerable<int> SlowRange(CancellationToken cancellationToken = default)
        {
            for (int i = 0; i != 10; i++)
            {
                Console.WriteLine($@"The value of i {i} at the start of SlowRange at {DateTime.Now}");
                await Task.Delay(1000, cancellationToken);
                Console.WriteLine($@"The value of i {i} after await of SlowRange at {DateTime.Now}");
                yield return i;
                Console.WriteLine($@"The value of i {i} after yield at {DateTime.Now}");
            }
        }
    }
}
