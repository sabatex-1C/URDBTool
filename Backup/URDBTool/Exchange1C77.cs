using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net.NetworkInformation;

namespace URDBTool
{
    abstract public class Exchange1C77
    {
        public static bool Pereferia = false;
        public static string ExportFileName = "KB";
        public static string Executable1C77 = "C:\\Program Files\\1CV77\\BIN\\1Cv7.exe";
        public static TextBox StatusBar = null;
        public static string UserName1C77 = "Admin";
        public static string Passwd1C77 = "11111111";
        private string[] Backup1C77String()
        {
            string[] s = new string[5];
            s[0] = "[General]";
            s[1] = "Quit=1";
            s[2] = "SaveData=1";
            s[3] = "[SaveData]";
            s[4] = "SaveToFile=" + '"' +Archive() + "1C_archive.zip" + '"';
            return s;
        }
        private bool Execute1C77(string[] commandline)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Executable1C77;
            string param = "config  /D" + Base + " /N" + UserName1C77 + " /P" + Passwd1C77;
            string inifile = Directory.GetCurrentDirectory() + "\\temp.ini";
            StreamWriter ini = new StreamWriter(inifile);
            foreach (string s in commandline)
            {
                ini.WriteLine(s);
            }
            ini.Close();

            param = param + " /@" + '"' + inifile + '"';
            proc.StartInfo.Arguments = param;
            try
            {
                proc.Start();
                proc.WaitForExit();
                if (proc.ExitCode != 0) return false;
            }
            finally
            {
                File.Delete(inifile);
                proc.Dispose();
            }
            return true;
        }



        private static DateTime TimeStart = DateTime.Now;


        public static void WriteMessage(string Msg)
        {
            if (StatusBar != null)
            {
                StatusBar.AppendText(Msg);
                StatusBar.ScrollToCaret();
                StatusBar.Refresh();
            }
        }
        public static string Archive()
        {
            string s = Base + "Archive\\" +
                       TimeStart.Year.ToString() +   //Year
                       "\\" + TimeStart.Month +         //Month
                       "\\" + TimeStart.Day +           //day
                       "\\" + TimeStart.Hour +          //hour
                       "." + TimeStart.Minute + "\\";         //minute
            // test archive exist
            if (!Directory.Exists(s))
            {
                Directory.CreateDirectory(s);
            }
            return s;

        }

        #region ABSTRACT CLASS
        abstract public bool ChanelInputStatus();
        abstract public bool ChanelOtputStatus();
        abstract public void GetInputFile();
        abstract public void WriteOtputFile();
        #endregion ABSTRACT

        #region PROPERTIES
        public static string InputFileName
        {
            get { return (Pereferia ? ExportFileName + "0.zip" : ExportFileName + "1.zip"); }
        }
        public static string OutputFileName
        {
            get { return (Pereferia ? ExportFileName + "1.zip" : ExportFileName + "0.zip"); }
        }
        
        public static string InputFile
        {
            get { return conf.Base + (Pereferia ? "CP\\":"PC\\")+InputFileName; }
        }
        public static string OutputFile
        {
            get { return conf.Base + (conf.Pereferia ? "PC\\" : "CP\\")+OutputFileName; }
        }

        public static string BaseName
        {
            get { return (Pereferia ? "KIBL" : "HIPP"); }
        }
        public static string Base
        {
            get
            {
                string[] bases = Registry.CurrentUser.OpenSubKey("Software\\1C\\1Cv7\\7.7\\Titles").GetValueNames();
                foreach (string s in bases)
                {
                    if (Registry.CurrentUser.OpenSubKey("Software\\1C\\1Cv7\\7.7\\Titles").GetValue(s, "").ToString() == BaseName)
                    {
                        return s;
                    }
                }
                return "";
            }
        }

        #endregion

        private static bool Excange1C77(string ExecutableFile, string BasePath, string LogFile, string User, string Password, bool Load)
        {
            GC.Collect();
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = ExecutableFile;
            string param = "config  /D" + BasePath + " /N" + User + " /P" + Password;
            string inifile = Directory.GetCurrentDirectory() + "\\temp.ini";
            StreamWriter ini = new StreamWriter(inifile);
            ini.WriteLine("[General]");
            ini.WriteLine("Output=" + '"' + LogFile + '"');
            ini.WriteLine("Quit=1");
            ini.WriteLine("UnloadData=0");
            if (Load) ini.WriteLine("SaveData=1");
            ini.WriteLine("CheckAndRepair=0");
            ini.WriteLine("AutoExchange=1");
            if (Load) ini.WriteLine("[SaveData]");
            if (Load) ini.WriteLine("SaveToFile=" + '"' + conf.Archive(BasePath) + "1C_archive.zip" + '"');
            ini.WriteLine("[AutoExchange]");
            ini.WriteLine("SharedMode=1");
            ini.WriteLine("WriteTo=*");
            ini.WriteLine("ReadFrom=" + (Load ? "*" : ""));
            ini.WriteLine("[CheckAndRepair]");
            ini.WriteLine("Repair=0");
            ini.WriteLine("PhysicalIntegrity=0");
            ini.WriteLine("Reindex=0");
            ini.WriteLine("Logicalintegrity=0");
            ini.WriteLine("CreateForUnresolved=0");
            ini.WriteLine("Reconstruct=0");

            ini.Close();

            param = param + " /@" + '"' + inifile + '"';
            proc.StartInfo.Arguments = param;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0) return false;
            File.Delete(inifile);
            return true;

        }

        private static bool BackUp1C77()
        {
            return true;
        }
        private static bool ImportData1C77()
        {
            return true;
        }


        private static bool ExportData1C77()
        {
            //Excange1C77(conf.Executable1C77, conf.Base, Directory.GetCurrentDirectory() + "\\Log.txt", "Admin", "11111111", File.Exists(InputPath + conf.InputFile));
            return true;
        }

        public void Start()
        {
            if (!ChanelInputStatus()) return;
            // Убираем старый файл обмена
            if (File.Exists(InputFile)) File.Delete(InputFile);
            //Забираем файл
            GetInputFile();
            // Если есть файл импорта то сделаем бекап системы и импорт даных
            if (File.Exists(InputFile))
            {
                File.Copy(InputFile, Archive() + InputFileName);
                BackUp1C77();
                ImportData1C77();
            }
            // Подготовим файл експорта
            ExportData1C77();
            
            // Пороверяем канал для записи и очищаем его
            ChanelOtputStatus();
            // Выводим даные
            WriteOtputFile();
            // Очищаем файл обмена
            if (File.Exists(InputFile)) File.Delete(InputFile);

            //// Если база переферийная то ещё ищем файл users.usr
            //if (File.Exists(Flash + "users.usr") && conf.Pereferia)
            //{
            //    File.Move(conf.Base + "usrdef\\users.usr", conf.Archive(conf.Base) + "users.usr");
            //    File.Move(Flash + "users.usr", conf.Base + "usrdef\\users.usr");
            //} 

        }
    }

    public class FlashExchange1c77 : Exchange1C77
    {
        public static string FlashDriveName = "SINHROHIPP";
        private string Flash = "";

        private static string GetFlashDrive()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady == true)
                {
                    if (d.VolumeLabel == FlashDriveName) return d.Name;
                }
            }
            return "";
        }
        public override bool ChanelInputStatus()
        {
            Flash = GetFlashDrive();
            DialogResult dr = DialogResult.OK;
            while (Flash == "" && dr == DialogResult.OK)
            {
                dr = MessageBox.Show("Вставте Флешку і натисніть ОК!", "Відсутня Флешка", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK) Flash = GetFlashDrive();
            }
            if (Flash == "") return false;
            return true;
        }

        public override void GetInputFile()
        {
            if (File.Exists(Flash + InputFileName))
            {
                File.Move(Flash + InputFileName, InputFile);
            }
        }

        public override bool ChanelOtputStatus()
        {
            if (File.Exists(Flash + OutputFileName)) File.Delete(Flash + OutputFileName);
            return true;
        }

        public override void WriteOtputFile()
        {
            if (File.Exists(OutputFile)) File.Move(OutputFile, Flash + OutputFileName);

        }
    }

    public class FTPExchange1c77 : Exchange1C77
    {
        public static string FTPServer = "ftp://ftpserver";

        public override bool ChanelInputStatus()
        {
            Ping ping = new Ping();
            if (ping.Send(FTPServer).Status == IPStatus.Success)
            {
                return true;
            }
            return false;
        }

        public override void GetInputFile()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override bool ChanelOtputStatus()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void WriteOtputFile()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

}


