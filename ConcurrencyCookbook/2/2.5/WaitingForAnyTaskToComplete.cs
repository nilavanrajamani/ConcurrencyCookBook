using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class WaitingForAnyTasksToComplete
    {
        static async Task Main(string[] args)
        {
            //Task task1 = Task.Delay(TimeSpan.FromSeconds(1));
            //Task task2 = Task.Delay(TimeSpan.FromSeconds(2));
            //Task task3 = Task.Delay(TimeSpan.FromSeconds(5));

            //await Task.WhenAll(task1, task2, task3);

            Task<int> task1 = Task.FromResult(1);
            Task<int> task2 = Task.FromResult(2);
            Task<int> task3 = Task.FromResult(3);
            int[] results = await Task.WhenAll(task1, task2, task3);
            Console.WriteLine(results);

            WaitingForAnyTasksToComplete waitingForTasksToComplete = new WaitingForAnyTasksToComplete();
            await waitingForTasksToComplete.DownloadAllAsync(new HttpClient(), new string[] { "http://www.google.com", "http://www.bing.com" });

        }

        async Task<string> DownloadAllAsync(HttpClient client, IEnumerable<string> urls)
        {
            var downloads = urls.Select(url => client.GetStringAsync(url));

            Task<string> htmlPage = await Task.WhenAny(downloads);            
            return string.Concat(await htmlPage);
        }
    }
}
