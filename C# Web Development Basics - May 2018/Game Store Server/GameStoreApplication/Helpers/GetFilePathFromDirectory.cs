using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WebServer.GameStoreApplication.Helpers
{
    public static class GetFileFromDirectory
    {
        public static string[] GetFileLinesByCurrentName(string fileName, string fileType)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var fileLines = File.ReadAllLines(newPath + $@".\GameStoreApplication\Data\{fileName}.{fileType}");

            return fileLines;
        }

        public static string GetFileAllTextByCurrentName(string fileName, string fileType)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var file = File.ReadAllText(newPath + $@".\GameStoreApplication\Resources\{fileName}.{fileType}");

            return file;
        }
    }
}
