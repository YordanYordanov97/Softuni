using System;
using System.Collections.Generic;
using System.Text;


public class Rectangle : DrawingTool
{
    public int Width { get; set; }
    public int Length { get; set; }

    public void Draw()
    {
        Console.WriteLine($"{new string('|', 1)}{new string('-', Width)}{new string('|', 1)}");
        for (int i = 0; i < Length-2; i++)
        {
            Console.WriteLine($"{new string('|', 1)}{new string(' ', Width)}{new string('|', 1)}");
        }
        Console.WriteLine($"{new string('|', 1)}{new string('-', Width)}{new string('|', 1)}");
    }
}

