using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ParallelAggregation
    {
        static async Task Main(string[] args)
        {
            ParallelAggregation parallelAggregation = new ParallelAggregation();
            Console.WriteLine($@"The value of parallel aggregation is {parallelAggregation.ParallelSum(new int[] { 1, 2, 3, 4, 5, 6 })} ");
            Console.WriteLine($@"The value of parallel aggregation through PLinq is {parallelAggregation.ParallelSumPlinq(new int[] { 1, 2, 3, 4, 5, 6 })} ");
            Console.WriteLine($@"The value of parallel aggregation through PLinq Aggregattion is {parallelAggregation.ParallelAggregate(new int[] { 1, 2, 3, 4, 5, 6 })} ");
        }

        public int ParallelSum(IEnumerable<int> values)
        {
            object mutex = new object();
            int result = 0;
            Parallel.ForEach(
                source: values,
                localInit: () => 0,
                body: (item, state, localValue) => localValue + item,
                localFinally: localValue =>
                {
                    lock (mutex)
                        result += localValue;
                });
            return result;
        }

        public int ParallelSumPlinq(IEnumerable<int> values)
        {
            return values.AsParallel().Sum();
        }

        public int ParallelAggregate(IEnumerable<int> values)
        {
            return values.AsParallel().Aggregate(
                seed: 0,
                func: (sum, item) => sum + item
                );;
        }
    }
}
