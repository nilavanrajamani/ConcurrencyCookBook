using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class GroupingEventsWithBuffers
    {
        static async Task Main(string[] args)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Buffer(2)
                .Subscribe(x => Console.WriteLine($@"{DateTime.Now.Second}: Got {x[0]} and {x[1]}"));
            Console.ReadLine();
        }
    }
}
