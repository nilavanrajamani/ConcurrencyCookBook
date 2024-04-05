using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class CreatingAsynchronousStreams
    {
        static async Task Main(string[] args)
        {
            
        }

        async IAsyncEnumerable<int> GetValuesAsync()
        {
            await Task.Delay(1000);
            yield return 10;
            await Task.Delay(1000);
            yield return 13;
        }

        async IAsyncEnumerable<string> GetValuesAsync(HttpClient httpClient)
        {
            int offset = 0;
            const int limit = 10;
            while (true)
            {
                string result = await httpClient.GetStringAsync($"https://www.google.com");
                string[] valuesOnThisPage = result.Split('\n');
                foreach (var item in valuesOnThisPage)
                {
                    yield return item;
                }

                if (valuesOnThisPage.Length != limit)
                    break;

                offset += limit;
            }
        }
    }
}
