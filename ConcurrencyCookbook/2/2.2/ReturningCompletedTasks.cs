using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ReturningCompletedTasks
    {
        static async Task Main(string[] args)
        {
            MySynchronousImplementation mySynchronousImplementation = new MySynchronousImplementation();
            try
            {
                await mySynchronousImplementation.DoSomethingAsyncWithExceptionBlock();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
    interface IMyAsyncInterface
    {
        Task<int> GetValueAsync();

        Task DoSomethingAsync();

        Task<T> NotImplementedAsync<T>();

        Task<int> GetValueAsync(CancellationToken cancellationToken);

        Task DoSomethingAsyncWithExceptionBlock();
    }

    public class MySynchronousImplementation : IMyAsyncInterface
    {
        public MySynchronousImplementation()
        {

        }
        public Task DoSomethingAsync()
        {
            return Task.CompletedTask;
        }

        public Task DoSomethingAsyncWithExceptionBlock()
        {
            try
            {
                throw new Exception();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }
        }

        public Task<int> GetValueAsync()
        {
            return Task.FromResult(1);
        }

        public Task<int> GetValueAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled<int>(cancellationToken);
            return Task.FromResult(13);
        }

        public Task<T> NotImplementedAsync<T>()
        {
            return Task.FromException<T>(new NotImplementedException());
        }
    }

}
