using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Program
{
   public static void Main(string[] args)
    {
        var cats = new List<Cat>();

        while (true)
        {
            var input = Console.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (input[0] == "End")
            {
                break;
            }
            var catType = input[0];
            if (catType == "Cymric")
            {
                var cymricName = input[1];
                var furLen = double.Parse(input[2]);
                var cymric = new Cymric(cymricName, furLen);
                cats.Add(cymric);
            }
            if (catType == "Siamese")
            {
                var siameseName = input[1];
                var earSize = int.Parse(input[2]);
                var siamese = new Siamese(siameseName, earSize);
                cats.Add(siamese);
            }
            if (catType == "StreetExtraordinaire")
            {
                var streetExtraordinaireName = input[1];
                var decibelsOfMeows = int.Parse(input[2]);
                var streetExtraordinaire = new StreetExtraordinaire(streetExtraordinaireName, decibelsOfMeows);
                cats.Add(streetExtraordinaire);
            }
        }
        var searchedName = Console.ReadLine();
        foreach(var item in cats)
        {
            try
            {
                var cymbric = (Cymric)item;
                if(cymbric.Name== searchedName)
                {
                    Console.WriteLine(cymbric.ToString());
                    continue;
                }
                
            }
            catch
            {
                
            }
            try
            {
                var siamese = (Siamese)item;
                if (siamese.Name == searchedName)
                {
                    Console.WriteLine(siamese.ToString());
                    continue;
                }
                
            }
            catch
            {

            }
            try
            {
                var streetCat = (StreetExtraordinaire)item;
                if (streetCat.Name == searchedName)
                {
                    Console.WriteLine(streetCat.ToString());
                    continue;
                }
            }
            catch
            {

            }
            

        }

    }
}

