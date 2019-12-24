using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NTICS;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.Threading;

namespace URDBTool
{
    public partial class main_form : Form
    {

        public main_form()
        {
            InitializeComponent();
            config.Initialize();
        }
        private void main_form_FormClosed(object sender, FormClosedEventArgs e)
        {
            config.Close();
        }
        private void btReindex_Click(object sender, EventArgs e)
        {
            CMD_1C77.Execute1C77(CMD_1C77.CMDLine(CMD_1C77.Operation.Reindex),"HIPP");
        }

        
        private void btFlashExcange_Click(object sender, EventArgs e)
        {
            FlashExchange1c77 flash = new FlashExchange1c77();
            flash.Start();
        }
        private void btFTPChange_Click(object sender, EventArgs e)
        {
            //FTP.Download(conf.FTPServer + "/" + conf.InputFile, InputPath + conf.InputFile);

            //if (File.Exists(InputPath + conf.InputFile))
            //{
            //    File.Copy(InputPath + conf.InputFile, conf.Archive(conf.Base) + conf.InputFile);
            //}

            //Excange1C77(conf.Executable1C77, conf.Base, Directory.GetCurrentDirectory() + "\\Log.txt", "Admin", "11111111", File.Exists(InputPath + conf.InputFile));
            //// Записываем файлы

            //if (File.Exists(OutputPath + conf.OutputFile)) FTP.Upload(conf.FTPServer + "/" + conf.OutputFile, OutputPath + conf.OutputFile);

            FTPExchange1c77 FTP = new FTPExchange1c77();
            FTP.Start();
        }


        
        




 

      }
}