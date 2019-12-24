using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.Net;

namespace URDBTool
{
    public class conf
    {
        
        public static bool Pereferia = false;
        public static string FTPServer = "ftp://ftpserver";

        const string ExportFileName = "KB";
        static DateTime TimeStart = DateTime.Now;


        public static string InputFile
        {
            get { return (Pereferia ? ExportFileName + "0.zip" : ExportFileName + "1.zip"); }
        }
        public static string OutputFile
        {
            get { return (Pereferia ? ExportFileName + "1.zip" : ExportFileName + "0.zip"); }
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
                    if (Registry.CurrentUser.OpenSubKey("Software\\1C\\1Cv7\\7.7\\Titles").GetValue(s,"").ToString() == BaseName)
                    {
                        return s;
                    }
                }
                return "";
            }
        }



        public static string Archive(string Base)
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


        private bool FTPDownload(string DownloadFile, string LocalFile)
        {
            ManualResetEvent wait = new ManualResetEvent(false);
            FTPUpLoader uploader = new FTPUpLoader(wait);
            uploader.AllowAbortUpload("out.txt", "ftp://sharriso1/ftptests.txt");
            wait.WaitOne();
            if (uploader.AsyncException != null)
            {
                Console.WriteLine(uploader.AsyncException.ToString());
            }
            return true;
        }

        //The EndGetResponseCallback method  
        //completes a call to BeginGetResponse.
        private static void EndGetResponseCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState)ar.AsyncState;
            FtpWebResponse response = null;
            try
            {
                response = (FtpWebResponse)state.Request.EndGetResponse(ar);
                response.Close();
                state.StatusDescription = response.StatusDescription;
                // Signal the main application thread that 
                // the operation is complete.
                state.OperationComplete.Set();
            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                Console.WriteLine("Error getting response.");
                state.OperationException = e;
                state.OperationComplete.Set();
            }
        }
        private static void EndGetStreamCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState)ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);
                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;
                FileStream stream = File.OpenRead(state.FileName);
                do
                {
                    readBytes = stream.Read(buffer, 0, bufferLength);
                    requestStream.Write(buffer, 0, readBytes);
                    count += readBytes;
                }
                while (readBytes != 0);
                Console.WriteLine("Writing {0} bytes to the stream.", count);
                // IMPORTANT: Close the request stream before sending the request.
                requestStream.Close();
                // Asynchronously get the response to the upload request.
                state.Request.BeginGetResponse(
                    new AsyncCallback(EndGetResponseCallback),
                    state
                );
            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                Console.WriteLine("Could not get the request stream.");
                state.OperationException = e;
                state.OperationComplete.Set();
                return;
            }

        }

        private NetworkCredential GetNetworkCredential()
        {
            return new NetworkCredential("hipp", "hipp");
        }


    }
}
