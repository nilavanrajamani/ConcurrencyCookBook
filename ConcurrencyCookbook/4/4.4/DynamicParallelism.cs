using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrencyCookbook._4._4._4
{
    public class DynamicParallelism
    {
        static async Task Main(string[] args)
        {

        }

        record Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
        }

        void Traverse(Node current)
        {
            //Do some CPU Intensive work
            if (current.Left != null)
            {
                Task.Factory.StartNew(() => Traverse(current.Left),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
            }

            if (current.Right != null)
            {
                Task.Factory.StartNew(() => Traverse(current.Right),
                    CancellationToken.None,
                    TaskCreationOptions.AttachedToParent,
                    TaskScheduler.Default);
            }
        }

        void ProcessTree(Node root)
        {
            Task task = Task.Factory.StartNew(() => Traverse(root),
                CancellationToken.None,
                TaskCreationOptions.AttachedToParent,
                TaskScheduler.Default);
            task.Wait();
        }
    }
}
