using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Win32;

namespace URDBTool
{
    public static class CMD_1C77
    {
        public enum Operation {Backup,Reindex,Import,Export};
        public static string Executable1C77 = "C:\\Program Files\\1CV77\\BIN\\1Cv7.exe";
        public static string UserName1C77 = "Admin";
        public static string Passwd1C77 = "11111111";
        public static string Base(string BaseName)
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
        public static string[] CMDLine(Operation op, string Archive, string LogFile)
        {
            string[] s = new string[29];
            
            // Основная секция
            s[0] = "[General]";
            s[1] = "Output=" + LogFile;                                       // Вывод протокола работы
            s[2] = "Quit=1";                                                  // Автозавершнение пакетного файла
            s[3] = "CheckAndRepair=" + (op == Operation.Reindex ? "1" : "0"); // Тестирование и исправление
            s[4] = "UnloadData=0";                                            // Выгрузка конфигурации
            s[5] = "SaveData=" + (op == Operation.Backup ? "1" : "0");          // Бекап
            bool exc = (op == Operation.Import || op == Operation.Export);
            s[6] = "AutoExchange="+(exc ? "1":"0");                              // Автообмен

            // Секция Тестирования и Исправления
            s[7] = "[CheckAndRepair]";
            s[8] = "Repair=1";
            s[9] = "PhysicalIntegrity=0";
            s[10] = "Reindex=" + (op == Operation.Reindex ? "1" : "0");          // Переиндексация;
            s[11] = "Logicalintegrity=0";
            s[12] = "RecalcSecondaries=0";
            s[13] = "RecalcTotals=0";
            s[14] = "Pack=0";
            s[15] = "SkipUnresolved=0";
            s[16] = "CreateForUnresolved=0";
            s[17] = "Reconstruct=0";
 
            // Секция выгрузки даных
            s[18] = "[UnloadData]";
            s[19] = "UnloadToFile=";
            s[20] = "IncludeUserDef=0";
            s[21] = "Password=";
            
            // Секция сохранения даных
            s[22] = "[SaveData]";
            s[23] = "SaveToFile=" + '"' + Archive + '"';
            s[24] = "";
 
            // Секция Автообмена
            s[25] = "[AutoExchange]";
            s[26] = "SharedMode=1";
            s[27] = "WriteTo="+(op==Operation.Export?"*":"");
            s[28] = "ReadFrom=" + (op == Operation.Import ? "*" : "");

            return s;
        }
        public static string[] CMDLine(Operation op, string Archive)
        {
            return CMDLine(op, Archive, "");
        }
        public static string[] CMDLine(Operation op)
        {
            return CMDLine(op,"","");
        }
        public static bool Execute1C77(string[] commandline, string BaseName)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = Executable1C77;
            string param = "config  /D" + Base(BaseName) + " /N" + UserName1C77 + " /P" + Passwd1C77;
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

    }
}
