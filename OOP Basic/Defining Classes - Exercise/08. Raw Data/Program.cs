using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
   public static void Main(string[] args)
    {
        var n = int.Parse(Console.ReadLine());
        var cars = new List<Car>();
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var model= input[0];
            var eSpeed = int.Parse(input[1]);
            var ePower = int.Parse(input[2]);
            var cWeight= int.Parse(input[3]);
            var cType = input[4];
            var tire1 = new Tire();
            tire1.TirePressure = double.Parse(input[5]);
            tire1.TireAge = int.Parse(input[6]);
            var tire2 = new Tire();
            tire2.TirePressure = double.Parse(input[7]);
            tire2.TireAge = int.Parse(input[8]);
            var tire3 = new Tire();
            tire3.TirePressure = double.Parse(input[9]);
            tire3.TireAge = int.Parse(input[10]);
            var tire4 = new Tire();
            tire4.TirePressure = double.Parse(input[11]);
            tire4.TireAge = int.Parse(input[12]);

            var cargo = new Cargo();
            cargo.CargoType = cType;
            cargo.CargoWeight = cWeight;

            var engine = new Engine();
            engine.EnginePower = ePower;
            engine.EngineSpeed = eSpeed;

            var car = new Car();
            car.Model = model;
            car.Cargo = cargo;
            car.Engine = engine;
            car.Tires.Add(tire1);
            car.Tires.Add(tire2);
            car.Tires.Add(tire3);
            car.Tires.Add(tire4);

            cars.Add(car);
        }

        var type = Console.ReadLine();
        if (type == "fragile")
        {
            var carsFragile = cars.Where(x => x.Tires.Any(y => y.TirePressure < 1)).ToList();
            foreach(var c in carsFragile)
            {
                Console.WriteLine(c.Model);
            }
        }
        if(type== "flamable")
        {
            var carsFlamable = cars.Where(x => x.Engine.EnginePower > 250).ToList();
            foreach (var c in carsFlamable)
            {
                Console.WriteLine(c.Model);
            }
        }
    }
}

