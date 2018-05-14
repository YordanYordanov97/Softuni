using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

public class Program
{
   public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var enginies = new List<Engine>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var input1=input.Take(2).ToList();
            var model = input1[0];
            var power = int.Parse(input1[1]);
            var input2 = input.Skip(2).ToList();
            var displacement = 0;
            var efficiency = "n/a";
            for (int j = 0; j < input2.Count; j++)
            {
                var number = 0;
                bool result = Int32.TryParse(input2[j], out number);
                if (result == false)
                {
                    efficiency = input2[j];
                }
                if (result == true)
                {
                    displacement = int.Parse(input2[j]);
                }
            }
            var engine = new Engine(model, power, displacement, efficiency);
            enginies.Add(engine);
        }

        var cars = new List<Car>();
        var m = int.Parse(Console.ReadLine());
        for (int i = 0; i < m; i++)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var input1 = input.Take(2).ToList();
            var model = input1[0];
            var engineModel = input1[1];
            var input2 = input.Skip(2).ToList();
            var weight = 0;
            var color = "n/a";
            for (int j = 0; j < input2.Count; j++)
            {
                var number = 0;
                bool result=Int32.TryParse(input2[j], out number);
                if (result == false)
                {
                    color = input2[j];
                }
                if (result ==true)
                {
                    weight = int.Parse(input2[j]);
                }
            }

            var carEngine = enginies.SingleOrDefault(x => x.Model == engineModel);
            var car = new Car(model, carEngine, weight, color);
            cars.Add(car);
        }

        foreach(var item in cars)
        {
            Console.WriteLine($"{item.Model}:");
            Console.WriteLine($"  {item.Engine.Model}:");
            Console.WriteLine($"    Power: {item.Engine.Power}");
            if (item.Engine.Displacement == 0)
            {
                Console.WriteLine($"    Displacement: n/a");
            }
            else
            {
                Console.WriteLine($"    Displacement: {item.Engine.Displacement}");
            }
            Console.WriteLine($"    Efficiency: {item.Engine.Efficiency}");
            if (item.Weight == 0)
            {
                Console.WriteLine($"  Weight: n/a");
            }
            else
            {
                Console.WriteLine($"  Weight: {item.Weight}");
            }
            Console.WriteLine($"  Color: {item.Color}");

        }

    }
}

