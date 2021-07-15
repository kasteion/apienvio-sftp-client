using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace apienvio_sync
{
    public class App
    {

        private readonly IConfigurationRoot _config;
        
        public App(IConfigurationRoot config)
        {
            _config = config;
        }

        public void Run()
        {
            var server = _config.GetConnectionString("server");
            var user = _config.GetConnectionString("user");
            var password = _config.GetConnectionString("password");

            List<string> remoteFiles = SftpFiles.list(server, user, password);
            Dictionary<string, int> Synced = new Dictionary<string, int>();
            foreach (var file in remoteFiles)
            {
                Synced.Add(file, 0);
            }
            List<string> localFiles = LocalFiles.list();
            foreach (var file in localFiles)
            {
                if (Synced.ContainsKey(file))
                {
                    Synced[file] = 1;
                }
                else
                {
                    Synced.Add(file, 2);
                }
            }
            foreach (var file in Synced)
            {
                switch (file.Value)
                {
                    case 0:
                        Console.WriteLine(file.Key + " exists in server but not in client, i will download it.");
                        SftpFiles.downloadFile(server, user, password, file.Key);
                        break;
                    case 1:
                        Console.WriteLine(file.Key + " is synced - nothing to do");
                        break;
                    case 2:
                        Console.WriteLine(file.Key + " exist in client but not in server, i will upload it.");
                        SftpFiles.uploadFile(server, user, password, file.Key);
                        break;
                }
            }
        }
    }
}
