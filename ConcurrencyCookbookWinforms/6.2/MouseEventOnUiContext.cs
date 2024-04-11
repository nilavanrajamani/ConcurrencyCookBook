using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConcurrencyCookbookWinforms
{
    public partial class MouseEventOnUiContext : Form
    {
        public MouseEventOnUiContext()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SynchronizationContext uiContext = SynchronizationContext.Current;
            Trace.WriteLine($@"UI thread is {Environment.CurrentManagedThreadId}");

            IObservable<EventPattern<MouseEventArgs>> mouseEventsObservable =
                Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                    handler => (s, a) => handler(s, a),
                    handler => MouseClick += handler,
                    handler => MouseClick -= handler
                    );

            IObservable<int> locationsObservable = mouseEventsObservable.Select(evt => evt.EventArgs.Clicks).ObserveOn(Scheduler.Default);

            IObservable<int> subscribableLocations = locationsObservable.Select(loc =>
            {
                Thread.Sleep(2000);                
                var thread = Environment.CurrentManagedThreadId;
                Trace.WriteLine($@"Calculated result {loc} on thread {thread}");
                return loc;
            }).ObserveOn(uiContext);

            subscribableLocations.Subscribe(z => Trace.WriteLine($@"Result {z} on thread {Environment.CurrentManagedThreadId}"));


        }
    }
}
