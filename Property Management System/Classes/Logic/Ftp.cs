using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Property_Management_System
{
    class Ftp
    {
        private static string Address = Properties.Settings.Default.FTP_Address;
        private static string User = Properties.Settings.Default.FTP_User;
        private static string Password = Properties.Settings.Default.FTP_Password;
        private static string LocalLoc = Properties.Settings.Default.FTP_LocalLoc;

        public void Download(string RemoteLoc)
        {
            try
            {
                FtpWebRequest FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Address + "/" + RemoteLoc);
                FtpRequest.Credentials = new NetworkCredential(User, Password);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                Stream FtpStream = FtpResponse.GetResponseStream();
                FileStream LocalFileStream = new FileStream(LocalLoc, FileMode.Create);
                byte[] ByteBuffer = new byte[2048];
                int BytesRead = FtpStream.Read(ByteBuffer, 0, 2048);
                try
                {
                    while (BytesRead > 0)
                    {
                        LocalFileStream.Write(ByteBuffer, 0, BytesRead);
                        BytesRead = FtpStream.Read(ByteBuffer, 0, 2048);
                    }
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[Ftp:Download] File Downloaded: "+ RemoteLoc);
                    }
                }
                catch (Exception e)
                {
                    Log.Commit("[Ftp:Download] Error downloading.");
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[Ftp:Download] Error: " + e.Message);
                    }
                }
                LocalFileStream.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
            }
            catch (Exception e)
            {
                Log.Commit("[Ftp:Download] Error downloading.");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Ftp:Download] Error: " + e.Message);
                }
            }
        }

        public static string[] ListDirectory(string RemoteLoc)
        {
            try
            {
                FtpWebRequest FtpRequest = (FtpWebRequest)FtpWebRequest.Create(Address + "/" + RemoteLoc);
                FtpRequest.Credentials = new NetworkCredential(User, Password);
                FtpRequest.UseBinary = true;
                FtpRequest.UsePassive = true;
                FtpRequest.KeepAlive = true;
                FtpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse FtpResponse = (FtpWebResponse)FtpRequest.GetResponse();
                Stream FtpStream = FtpResponse.GetResponseStream();
                StreamReader FtpReader = new StreamReader(FtpStream);
                string DirectoryRaw = null;
                try
                {
                    while (FtpReader.Peek() != -1)
                    {
                        DirectoryRaw += FtpReader.ReadLine() + ",";
                    }
                }
                catch (Exception e)
                {
                    Log.Commit("[Ftp:ListDirectory] Error loading.");
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[Ftp:ListDirectory] Error: " + e.Message);
                    }
                }
                FtpReader.Close();
                FtpStream.Close();
                FtpResponse.Close();
                FtpRequest = null;
                try
                {
                    string[] DirectoryList = DirectoryRaw.Split(",".ToCharArray());
                    return DirectoryList;
                }
                catch (Exception e)
                {
                    Log.Commit("[Ftp:ListDirectory] Error loading.");
                    if (Properties.Settings.Default.User_AdvancedLogging)
                    {
                        Log.Commit("[Ftp:ListDirectory] Error: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Commit("[Ftp:ListDirectory] Error loading.");
                if (Properties.Settings.Default.User_AdvancedLogging)
                {
                    Log.Commit("[Ftp:ListDirectory] Error: " + e.Message);
                }
            }
            return new string[] { "" };
        }
    }
}
