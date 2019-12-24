//---------------------------------------------------------------------
//  This file is part of the Microsoft .NET Framework SDK Code Samples.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
// 
//This source code is intended only as a supplement to Microsoft
//Development Tools and/or on-line documentation.  See these other
//materials for detailed information regarding Microsoft code samples.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------

#region Using directives

using System;
using System.Text;
using System.IO;
using System.Net;
#endregion

namespace URDBTool
{
    static public class FTP
    {
    
        static public void Download(string downloadUrl,string LocalFileName)
        {
            Stream responseStream = null;
            FileStream fileStream = null;
			StreamReader reader = null;
			try
			{
				FtpWebRequest downloadRequest =     (FtpWebRequest)WebRequest.Create(downloadUrl);
                downloadRequest.Credentials = new NetworkCredential("hipp", "hipp");
                downloadRequest.Proxy = null;
				FtpWebResponse downloadResponse =	(FtpWebResponse)downloadRequest.GetResponse();
				responseStream = downloadResponse.GetResponseStream();

                if (LocalFileName.Length == 0)
				{
					reader = new StreamReader(responseStream);
					Console.WriteLine(reader.ReadToEnd());
				}
				else
				{
                    fileStream = File.Create(LocalFileName);
					byte[] buffer = new byte[1024];
					int bytesRead;
					while (true)
					{
						bytesRead = responseStream.Read(buffer, 0, buffer.Length);
						if (bytesRead == 0)
							break;
						fileStream.Write(buffer, 0, bytesRead);
					}
				}
				Console.WriteLine("Download complete.");
			}
			catch (UriFormatException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (WebException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
            finally
            {
				if (reader != null)
					reader.Close();
				else if (responseStream != null)
                    responseStream.Close();
                if (fileStream != null)
                    fileStream.Close();
            }
        }

        static public void Upload(string uploadUrl, string LocalFileName)
        {
            Stream requestStream = null;
            FileStream fileStream = null;
            FtpWebResponse uploadResponse = null;
			try
			{
				FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(uploadUrl);
                uploadRequest.Credentials = new NetworkCredential("hipp", "hipp");
                uploadRequest.Proxy = null;

                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;

				// UploadFile is not supported through an Http proxy
				// so we disable the proxy for this request.
				uploadRequest.Proxy = null;

				requestStream = uploadRequest.GetRequestStream();
                fileStream = File.Open(LocalFileName, FileMode.Open);

				byte[] buffer = new byte[1024];
				int bytesRead;
				while (true)
				{
					bytesRead = fileStream.Read(buffer, 0, buffer.Length);
					if (bytesRead == 0)
						break;
					requestStream.Write(buffer, 0, bytesRead);
				}
				
				// The request stream must be closed before getting 
				// the response.
				requestStream.Close();

				uploadResponse =
					(FtpWebResponse)uploadRequest.GetResponse();
				Console.WriteLine("Upload complete.");
			}
			catch (UriFormatException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
			catch (WebException ex)
			{
				Console.WriteLine(ex.Message);
			}
            finally
            {
                if (uploadResponse != null)
                    uploadResponse.Close();
                if (fileStream != null)
                    fileStream.Close();
                if (requestStream != null)
                    requestStream.Close();
            }
        }

        private static void List(string listUrl)
        {
            StreamReader reader = null;
            try
            {
                FtpWebRequest listRequest =
                    (FtpWebRequest)WebRequest.Create(listUrl);
                listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse listResponse =
                    (FtpWebResponse)listRequest.GetResponse();
                reader = new StreamReader(listResponse.GetResponseStream());
                Console.WriteLine(reader.ReadToEnd());
                Console.WriteLine("List complete.");
            }
			catch (UriFormatException ex)
			{
				Console.WriteLine(ex.Message);
			}
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
