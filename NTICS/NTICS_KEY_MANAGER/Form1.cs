using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using NTICS;


namespace NTICS_KEY_MANAGER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int edrpou_32 = Int32.Parse(tb_edrpou.Text);
            byte[] edrpou = {(byte)edrpou_32,(byte)(edrpou_32 >> 8),(byte)(edrpou_32 >> 16),(byte)(edrpou_32 >> 24)};
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(384);
            RSA.FromXmlString("<RSAKeyValue><Modulus>7RXWNpO6yVx03AZ/eeJabvA0Of35a32jsBFiM+tV2T09Hevfl5pEnssDpKI8/wbP</Modulus><Exponent>AQAB</Exponent><P>+FS4QNrkEHAJB2xaLw2lMbl6TNMj8IeR</P><Q>9Gg1ym8VljXLwLhKsVUj2tzotjFPRThf</Q><DP>SdrQbaFsAKOBW+7Sp3nUboRuJhkJcEix</DP><DQ>EOD5hgdx6DPC5IZVsjV9CmpjL+Hr5Y3l</DQ><InverseQ>D56vb9fyeVkLIK2zhkfj523cf4GsFtOc</InverseQ><D>4I3TRAQuYXQxtwhsiwwKbZMTVG4qZFYs9e4yyxzyT3S6vKwj/88MOpGgJxNlQT/h</D></RSAKeyValue>");
            string s = RSA.ToXmlString(false);
            
            byte[] result = RSA.Encrypt(edrpou,false);
            tb_Serial.Text = Protection.ArrayToString(Protection.ByteArrayToUInt(result));
            result = Protection.UIntArrayToByte(Protection.StringToArray(tb_Serial.Text));

            byte[] test = RSA.Decrypt(result, false);
            int testedrpou = (int)test[0] | ((int)test[1] << 8) | ((int)test[2] << 16) | ((int)test[3] << 24);

        }
    }
}