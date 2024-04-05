using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook
{
    public class ConsoleReadLineAsyncBug
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Task<string> readLineTask = Console.In.ReadLineAsync();

                    Debug.WriteLine("hi");
            }

        }
    }
}
