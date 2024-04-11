using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcurrencyCookbookWinforms._6._2
{
    public partial class NotificationOnUIThread : Form
    {
        public NotificationOnUIThread()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Trace.WriteLine(@$"UI Thread is {Environment.CurrentManagedThreadId}");
            SynchronizationContext synchronizationContext = SynchronizationContext.Current;
            Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(synchronizationContext)
                .Subscribe(x =>
                {
                    Trace.WriteLine($@"Interval {x} on thread {Environment.CurrentManagedThreadId}");
                });
        }
    }
}
