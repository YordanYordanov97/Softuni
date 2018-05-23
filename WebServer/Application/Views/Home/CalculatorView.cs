namespace WebServer.Application.Views.Home
{
    using System.IO;
    using WebServer.Application.Models;
    using WebServer.Server.Contracts;

    public class CalculatorView :IView
    {
        private const string ReplacementSnip = "<!--replace-->";
        private double numberOne;
        private double numberTwo;
        private string mathSign;

        public CalculatorView(double numberOne, string mathSign, double numberTwo)
        {
            this.numberOne = numberOne;
            this.numberTwo = numberTwo;
            this.mathSign = mathSign;
        }

        public string View()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string newPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            string html = File.ReadAllText(newPath + @".\Application\Resources\calculator.html");

            if (string.IsNullOrEmpty(mathSign))
            {
                return html;
            }

            var result=Calculator.CalculateNumbers(numberOne, mathSign, numberTwo);

            html = html.Replace(ReplacementSnip, result);

            return html;
        }
    }
}
