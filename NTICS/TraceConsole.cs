using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace NTICS
{
    public partial class TraceConsole : Frame
    {
        public TraceConsole()
        {
            InitializeComponent();
        }
        public void Write(string message)
        {
            lbInfo.Items[lbInfo.Items.Count - 1] = lbInfo.Items[lbInfo.Items.Count - 1] + message;
        }

        public void WriteLine(string message)
        {
            lbInfo.Items.Add(message);
        }

    }

    public class TraceConsoleListner : TraceListener
    {
        private TraceConsole trace;

        public TraceConsoleListner()
        {
            trace = new TraceConsole();

        }
        
        public override void Write(string message)
        {
            trace.Write(message);
        }

        public override void WriteLine(string message)
        {
            trace.WriteLine(message);
        }
    }


}
