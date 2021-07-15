using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace apienvio_sync
{
    public class LocalFiles
    {

        public static List<string> list()
        {
            string docPath = @"C:\inetpub\wwwroot\APIEnvioFacturas\Reportes";
            var files = Directory.EnumerateFiles(docPath);
            var list = new List<string>();
            foreach (var file in files)
            {
                list.Add(file.Replace(docPath + @"\", ""));
            }
            return list;
        }
    }
}
