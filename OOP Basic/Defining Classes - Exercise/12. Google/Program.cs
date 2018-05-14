using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
   public static void Main(string[] args)
    {
        var persons = new List<Person>();
        while (true)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (input[0] == "End")
            {
                break;
            }
            var personName = input[0];
            
            if (input[1] == "company")
            {
                var companyName = input[2];
                var companyDep= input[3];
                var salary = decimal.Parse(input[4]);
                var company = new Company(companyName, companyDep, salary);
                if (persons.Any(x => x.Name == personName) == false)
                {
                    var  person = new Person();
                    person.Name = personName;
                    person.Company = company;
                    persons.Add(person);
                }
                else
                {
                    var person = persons.SingleOrDefault(x => x.Name == personName);
                    person.Company = company;
                }
            }
            if (input[1] == "car")
            {
                var carModel = input[2];
                var carSpeed= int.Parse(input[3]);
                var car = new Car(carModel, carSpeed);
                if (persons.Any(x => x.Name == personName) == false)
                {
                    var person = new Person();
                    person.Name = personName;
                    person.Car = car;
                    persons.Add(person);
                }
                else
                {
                    var person = persons.SingleOrDefault(x => x.Name == personName);
                    person.Car = car;
                }
            }
            if (input[1] == "parents")
            {
                var name= input[2];
                var birthday = input[3];
                var parent = new Parent(name, birthday);
                if (persons.Any(x => x.Name == personName) == false)
                {
                    var person = new Person();
                    person.Name = personName;
                    person.Parents.Add(parent);
                    persons.Add(person);
                }
                else
                {
                    var person = persons.SingleOrDefault(x => x.Name == personName);
                    person.Parents.Add(parent);
                }
            }
            if (input[1] == "children")
            {
                var name = input[2];
                var birthday = input[3];
                var children = new Children(name, birthday);
                if (persons.Any(x => x.Name == personName) == false)
                {
                    var person = new Person();
                    person.Name = personName;
                    person.Childrens.Add(children);
                    persons.Add(person);
                }
                else
                {
                    var person = persons.SingleOrDefault(x => x.Name == personName);
                    person.Childrens.Add(children);
                }
            }
            if (input[1] == "pokemon")
            {
                var name = input[2];
                var type = input[3];
                var pokemon = new Pokemon(name, type);
                if (persons.Any(x => x.Name == personName) == false)
                {
                    var person = new Person();
                    person.Name = personName;
                    person.Pokemons.Add(pokemon);
                    persons.Add(person);
                }
                else
                {
                    var person = persons.SingleOrDefault(x => x.Name == personName);
                    person.Pokemons.Add(pokemon);
                }
            }

        }

        var serchingName = Console.ReadLine();
        var serchingPerson = persons.SingleOrDefault(x => x.Name == serchingName);
        Console.WriteLine(serchingPerson.Name);
        Console.WriteLine($"Company:");
        try
        {
            Console.WriteLine($"{serchingPerson.Company.Name} {serchingPerson.Company.Department} {serchingPerson.Company.Salary:f2}");
        }
        catch
        {

        }
        Console.WriteLine($"Car:");
        try
        {
            Console.WriteLine($"{serchingPerson.Car.Model} {serchingPerson.Car.Speed}");
        }
        catch
        {

        }
        Console.WriteLine("Pokemon:");
        foreach(var p in serchingPerson.Pokemons)
        {
            Console.WriteLine($"{p.Name} {p.Type}");
        }
        Console.WriteLine("Parents:");
        foreach (var pa in serchingPerson.Parents)
        {
            Console.WriteLine($"{pa.Name} {pa.Birthday}");
        }
        Console.WriteLine("Children:");
        foreach (var ch in serchingPerson.Childrens)
        {
            Console.WriteLine($"{ch.Name} {ch.Birthday}");
        }



    }
}

