using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConcurrencyCookbook
{
    public class ElapsedTimeReflection
    {
        static async Task Main(string[] args)
        {
            //With conversion delegate
            var timer = new System.Timers.Timer(interval: 1000) { Enabled = true };
            IObservable<EventPattern<object>> timerReports =
                Observable.FromEventPattern(timer, nameof(System.Timers.Timer.Elapsed));

            timerReports.Subscribe(data => Console.WriteLine("OnNext: " + ((ElapsedEventArgs)data.EventArgs).SignalTime));
            Console.ReadLine();
        }
    }
}
