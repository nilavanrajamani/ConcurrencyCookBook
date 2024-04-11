using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class NotificationOnDifferentThread
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine(@$"UI Thread is {Environment.CurrentManagedThreadId}");
            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(x => Console.WriteLine($@"Interval {x} on thread {Environment.CurrentManagedThreadId}"));
            Console.ReadLine();
        }
    }
}
