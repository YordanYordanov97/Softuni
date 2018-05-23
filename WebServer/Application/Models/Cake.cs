using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WebServer.Application.Models
{
    public class Cake
    {
        public void AddCakeInFile(string name, double price)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var filePath = newPath + @".\Application\Data\database.csv";

            var streamReader = new StreamReader(filePath);
            var id = streamReader.ReadToEnd().Split(Environment.NewLine).Length;
            streamReader.Dispose();

            using (var streamWriter=new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine($"{id},{name},{price:f2}");
            }
            //File.AppendAllText(filePath, $"{name},{price:f2}{Environment.NewLine}");
        }
    }
}
