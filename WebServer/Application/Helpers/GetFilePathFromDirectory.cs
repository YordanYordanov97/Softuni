namespace WebServer.Application.Helpers
{
    using System.IO;

    public static class GetFileFromDirectory
    {
        public static string[] GetFileLinesByCurrentName(string fileName,string fileType)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var fileLines = File.ReadAllLines(newPath + $@".\Application\Data\{fileName}.{fileType}");

            return fileLines;
        }

        public static string GetFileAllTextByCurrentName(string fileName, string fileType)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var file = File.ReadAllText(newPath + $@".\Application\Resources\{fileName}.{fileType}");

            return file;
        }
    }
}
