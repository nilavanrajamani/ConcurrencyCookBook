// See https://aka.ms/new-console-template for more information
using ConcurrencyCookbook;


namespace ConcurrencyCookbook
{
    public class PausingForAPeriodOfTime
    {
        static async Task Main(string[] args)
        {            
            PausingClass pausingClass = new PausingClass();
            string googleContent = await pausingClass.DownloadStringWithTimeout(new HttpClient(), "http://www.google.com");
            Console.WriteLine(googleContent);
        }     

    }
    
    public class PausingClass
    {
        public async Task<string> DownloadStringWithTimeout(HttpClient httpClient, string url)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            Task<string> downloadTask = httpClient.GetStringAsync(url);
            Task timeoutTask = Task.Delay(Timeout.InfiniteTimeSpan, cts.Token);

            Task completedTask = await Task.WhenAny(downloadTask, timeoutTask);
            if (completedTask == timeoutTask)
                return null;
            return await downloadTask;
        }
    }
}