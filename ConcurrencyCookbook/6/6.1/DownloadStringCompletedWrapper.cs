using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class DownloadStringCompletedWrapper
    {
        static async Task Main(string[] args)
        {
            var client = new WebClient();
            IObservable<EventPattern<object>> downloadStrings =
                Observable.FromEventPattern(client, nameof(WebClient.DownloadStringCompleted));
            downloadStrings.Subscribe(data =>
            {
                var eventArgs = (DownloadStringCompletedEventArgs)data.EventArgs;
                if (eventArgs.Error != null)
                {
                    Console.WriteLine("OnNext: (Error)" + eventArgs.Error);
                }
                else
                {
                    Console.WriteLine("OnNext: " + eventArgs.Result);
                }
            },
            ex => Console.WriteLine("OnError: " + ex.ToString()),
            () => Console.WriteLine("OnCompleted"));

            client.DownloadStringAsync(new Uri("http://invalid.example.com/"));
        }
    }
}
