using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ObservableBasics
    {
        static async Task Main(string[] args)
        {
            IObservable<long> ticks = Observable.Timer(
                dueTime: TimeSpan.FromSeconds(5),
                period: TimeSpan.FromSeconds(1)
                );

            ticks.Subscribe( x => { Console.WriteLine($@"Ticks is {x}"); }); 

            Console.ReadLine();
        }
    }
}
