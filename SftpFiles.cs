using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Renci.SshNet;

namespace apienvio_sync
{
    public class SftpFiles
    {

        public static List<string> list(string server, string user, string password)
        {
            var list = new List<string>();
            using (var sftp = new SftpClient(server, user, password))
            {
                try
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory("/");
                    foreach (var file in files)
                    {
                        list.Add(file.Name);
                    }
                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return list;
        }


        public static void downloadFile(string server, string user, string password, string fileName)
        {
            var pathRemoteFile = @"/" + fileName;
            var pathLocalFile = Path.Combine(@"C:\inetpub\wwwroot\APIEnvioFacturas\Reportes", fileName);
            using (var sftp = new SftpClient(server, user, password))
            {
                try
                {
                    sftp.Connect();
                    using (var fileStream = File.OpenWrite(pathLocalFile))
                    {
                        sftp.DownloadFile(pathRemoteFile, fileStream);
                        fileStream.Close();
                    }
                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void uploadFile(string server, string user, string password, string fileName)
        {
            var pathRemoteFile = @"/" + fileName;
            var pathLocalFile = Path.Combine(@"C:\inetpub\wwwroot\APIEnvioFacturas\Reportes", fileName);
            using (var sftp = new SftpClient(server, user, password))
            {
                try
                {
                    sftp.Connect();
                    using (var fileStream = File.OpenRead(pathLocalFile))
                    {
                        sftp.UploadFile(fileStream, pathRemoteFile);
                        fileStream.Close();
                    }
                    sftp.Disconnect();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
