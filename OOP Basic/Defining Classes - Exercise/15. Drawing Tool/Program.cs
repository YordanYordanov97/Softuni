using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Program
{
   public static void Main(string[] args)
    {
        var figure = Console.ReadLine();
        var drawingTool=new List<DrawingTool>();
        if (figure == "Square")
        {
            var square = new Square();
            var len = int.Parse(Console.ReadLine());
            square.Len = len;
            drawingTool.Add(square);
        }
        if (figure == "Rectangle")
        {
            var rectangle= new Rectangle();
            var width = int.Parse(Console.ReadLine());
            var len = int.Parse(Console.ReadLine());
            rectangle.Length = len;
            rectangle.Width = width;
            drawingTool.Add(rectangle);
        }

        foreach(var f in drawingTool)
        {
            try
            {
                var square = (Square)f;
                square.Draw();
            }
            catch
            {

            }
            try
            {
                var rectangle= (Rectangle)f;
                rectangle.Draw();
            }
            catch
            {

            }
        }
    }
}

