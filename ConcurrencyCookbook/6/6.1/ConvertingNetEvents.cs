using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ConcurrencyCookbook
{
    public class ConvertingNetEvents
    {
        static async Task Main(string[] args)
        {
            var progress = new Progress<int>();
            IObservable<EventPattern<int>> progressReports =
                Observable.FromEventPattern<int>(
                    handler => progress.ProgressChanged += handler,
                    handler => progress.ProgressChanged -= handler
                    );
            progressReports.Subscribe(data => Console.WriteLine("OnNext: " + data.EventArgs));

            //With conversion delegate
            var timer = new System.Timers.Timer(interval: 1000) { Enabled = true };
            IObservable<EventPattern<ElapsedEventArgs>> timerReports =
                Observable.FromEventPattern<ElapsedEventHandler, ElapsedEventArgs>( 
                    handler => (s, a) => handler(s, a),
                    handler => timer.Elapsed += handler,
                    handler => timer.Elapsed -= handler
                    );
            timerReports.Subscribe(data => Console.WriteLine("OnNext: " + data.EventArgs.SignalTime));

        }
    }
}
