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

namespace ConcurrencyCookbookWinforms._6._3
{
    public partial class BufferMouseEvents : Form
    {
        public BufferMouseEvents()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Observable.FromEventPattern<MouseEventHandler, MouseEventArgs>(
                handler => (s, a) => handler(s, a),
                handler => MouseMove += handler,
                handler => MouseMove -= handler
                )
                .Buffer(TimeSpan.FromSeconds(1))
                .Subscribe(x => Trace.WriteLine($@"{DateTime.Now.Second}: Saw {x.Count} items"));
            ;
        }
    }
}
