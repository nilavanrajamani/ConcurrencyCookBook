using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class HandlingExceptionsFromAsyncTaskMethods
    {
        static async Task Main(string[] args)
        {
            HandlingExceptionsFromAsyncTaskMethods handlingExceptionsFromAsyncTaskMethods = new HandlingExceptionsFromAsyncTaskMethods();
            await handlingExceptionsFromAsyncTaskMethods.TestAsync();
        }

        async Task ThrowExceptionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new InvalidOperationException("Test");
        }

        async Task TestAsync()
        {
            Task exceptionAsync = ThrowExceptionAsync();
            try
            {
                await exceptionAsync;
            }
            catch (InvalidOperationException ex)
            {
            }
        }
    }
}
