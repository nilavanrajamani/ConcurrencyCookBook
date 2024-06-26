﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class AwaitingTasksAsTheyCompleteProblem
    {
        static async Task Main(string[] args)
        {
            AwaitingTasksAsTheyCompleteProblem awaitingTasksAsTheyComplete = new AwaitingTasksAsTheyCompleteProblem();
            Task<int> taskA = awaitingTasksAsTheyComplete.DelayAndReturnAsync(2);
            Task<int> taskB = awaitingTasksAsTheyComplete.DelayAndReturnAsync(3);
            Task<int> taskC = awaitingTasksAsTheyComplete.DelayAndReturnAsync(1);
            Task<int>[] tasks = new[] { taskA, taskB, taskC };

            int i = 1;
            foreach (var task in tasks)
            {
                Console.WriteLine($@"{i} execution in progress");    
                var result = await task;
                Console.WriteLine($@"{i} execution Completed");
                i++;
                Console.WriteLine(result);
            }
        }

        async Task<int> DelayAndReturnAsync(int value)
        {
            await Task.Delay(TimeSpan.FromSeconds(value));
            return value;
        }
    }
}
