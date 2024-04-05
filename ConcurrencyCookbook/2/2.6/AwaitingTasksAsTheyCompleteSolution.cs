using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class AwaitingTasksAsTheyCompleteSolution
    {
        static async Task Main(string[] args)
        {
            AwaitingTasksAsTheyCompleteSolution awaitingTasksAsTheyComplete = new AwaitingTasksAsTheyCompleteSolution();
            Task<int> taskA = awaitingTasksAsTheyComplete.DelayAndReturnAsync(2);
            Task<int> taskB = awaitingTasksAsTheyComplete.DelayAndReturnAsync(3);
            Task<int> taskC = awaitingTasksAsTheyComplete.DelayAndReturnAsync(1);
            
            Task<int>[] tasks = new[] { taskA, taskB, taskC };
            IEnumerable<Task> taskQuery = from t in tasks select awaitingTasksAsTheyComplete.AwaitAndProcessAsync(t);

            Task[] processingTasks = taskQuery.ToArray();
            await Task.WhenAll(processingTasks);
        }

        async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(value));
            return value;
        }

        async Task AwaitAndProcessAsync(Task<int> value)
        {
            Console.WriteLine(await value);
        }
    }
}
