using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public interface IAsyncEnumerableProducer
    {
        IAsyncEnumerable<string> GetValuesAsync(HttpClient client);
    }
    
    internal class ConsumingAsynchronousStreams
    {
        private IAsyncEnumerableProducer _asyncEnumerableProducer;

        public ConsumingAsynchronousStreams(IAsyncEnumerableProducer asyncEnumerableProducer)
        {
            _asyncEnumerableProducer = asyncEnumerableProducer;
        }
        public async Task ProcessValueAsync(HttpClient client)
        {
            await foreach (var item in _asyncEnumerableProducer.GetValuesAsync(client))
            {
                Console.WriteLine(item);
            }
        }
    }
}
