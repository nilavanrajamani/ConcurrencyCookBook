using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ReportingProgress
    {
        static async Task Main(string[] args)
        {
            var progress = new Progress<double>();
            progress.ProgressChanged += Progress_ProgressChanged;
            ConsoleWriteLineProgress consoleWriteLineProgress = new ConsoleWriteLineProgress();
            await consoleWriteLineProgress.MyMethodAsync(progress);
        }

        private static void Progress_ProgressChanged(object? sender, double e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public class ConsoleWriteLineProgress
    {
        public async Task MyMethodAsync(IProgress<double> progress = null)
        {
            bool done = false;
            double percentComplete = 0;
            while (!done)
            {
                await Task.Delay(1000);
                percentComplete += 10;
                progress?.Report(percentComplete);
                if (percentComplete == 100)
                {
                    done = true;
                }
            }
        }
    }
}
