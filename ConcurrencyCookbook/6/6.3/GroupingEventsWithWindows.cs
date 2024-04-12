using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class GroupingEventsWithWindows
    {
        static async Task Main(string[] args)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Window(2)
                .Subscribe(group =>
                {
                    Console.WriteLine($@"{DateTime.Now.Second}: Starting new group");
                    group.Subscribe(
                        x => Console.WriteLine($@"{DateTime.Now.Second}: Got {x}"),
                        () => Console.WriteLine($@"{DateTime.Now.Second}: Ending Group"));
                });

            Console.ReadLine();
        }
    }
}
