using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ParallelLinq
    {
        static async Task Main(string[] args)
        {
            ParallelLinq parallelAggregation = new ParallelLinq();
            Console.WriteLine($@"The value of parallel aggregation through PLinq is ");
            foreach (var item in parallelAggregation.ParallelMultiply(new int[] { 1, 2, 3, 4, 5, 6 }))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($@"The value of parallel aggregation through PLinq Aggregattion is ");
            foreach (var item in parallelAggregation.ParallelMultiplyOrdered(new int[] { 1, 2, 3, 4, 5, 6 }))
            {
                Console.WriteLine(item);
            }
        }

        public IEnumerable<int> ParallelMultiply(IEnumerable<int> values)
        {
            return values.AsParallel().Select(x => x * 2);
        }

        public IEnumerable<int> ParallelMultiplyOrdered(IEnumerable<int> values)
        {
            return values.AsParallel().AsOrdered().Select(x => x * 2);
        }
    }
}
