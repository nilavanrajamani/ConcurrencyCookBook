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

namespace ConcurrencyCookbookWinforms._6._5
{
    public partial class TimeoutWithFallbackSubscribable : Form
    {
        public TimeoutWithFallbackSubscribable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IObservable<MouseEventArgs> mouseEventArgs =
                Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                handler => (s, a) => handler(s, a),
                handler => MouseDown += handler,
                handler => MouseDown -= handler
                )
                .Select(x => x.EventArgs);

            Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                handler => (s, a) => handler(s, a),
                handler => MouseMove += handler,
                handler => MouseMove -= handler
                )
                .Select(x => x.EventArgs)
                .Timeout(TimeSpan.FromSeconds(1), mouseEventArgs)
                .Subscribe(
                    x => Trace.WriteLine($@"{DateTime.Now.Second}: Saw {x.X + x.Y} items"),
                    ex => Trace.WriteLine(ex)
                );
            ;
        }
    }
}
