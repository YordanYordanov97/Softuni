using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Application.Models
{
    public static class Calculator
    {
        public static string CalculateNumbers(double numberOne,string mathSign,double numberTwo)
        {
            var resultString = string.Empty;
            var result = 0.00D;
            switch (mathSign)
            {
                case "+":
                    result = numberOne + numberTwo;
                    break;
                case "-":
                    result = numberOne - numberTwo;
                    break;
                case "*":
                    result = numberOne * numberTwo;
                    break;
                case "/":
                    result = numberOne / numberTwo;
                    break;
                default:
                    resultString = "Invalid Sign";
                    break;
            }
            if (string.IsNullOrEmpty(resultString))
            {
                return $"<p>Result: {result:f2}</p>";
            }
            
            return $"<p>{resultString}</p>";

        }
    }
}
