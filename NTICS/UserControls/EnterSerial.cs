using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NTICS.UserControls
{
    public partial class EnterSerial : UserControl
    {
        public EnterSerial()
        {
            InitializeComponent();
        }


        private bool CheckSerial(string Value)
        {
            try
            {
                uint test = Protection.StringToUInt(Value);
                return true;
            }
            catch 
            {
                return false;
            }
        }


        private void TextChanged(object sender, EventArgs e)
        {
            btOK.Enabled = CheckSerial(serial1.Text) || CheckSerial(serial2.Text) ||
                           CheckSerial(serial3.Text) || CheckSerial(serial4.Text) ||
                           CheckSerial(serial5.Text) || CheckSerial(serial6.Text) ||
                           CheckSerial(serial7.Text) || CheckSerial(serial8.Text) ||
                           CheckSerial(serial9.Text) || CheckSerial(serial10.Text) ||
                           CheckSerial(serial11.Text) || CheckSerial(serial12.Text);
        }


    }
}
