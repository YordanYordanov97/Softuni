using System;
using System.Collections.Generic;
using System.Text;

public class Square : DrawingTool
{
    public int Len { get; set; }

    public void Draw()
    {
        Console.WriteLine($"{new string('|', 1)}{new string('-', Len)}{new string('|', 1)}");
        for (int i = 0; i < Len - 2; i++)
        {
            Console.WriteLine($"{new string('|', 1)}{new string(' ', Len)}{new string('|', 1)}");
        }
        Console.WriteLine($"{new string('|', 1)}{new string('-', Len)}{new string('|', 1)}");
    }
}

