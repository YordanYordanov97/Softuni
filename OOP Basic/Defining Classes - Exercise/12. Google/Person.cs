using System;
using System.Collections.Generic;
using System.Text;


  public class Person
{
    public string Name { get; set; }
    public Company Company { get; set; }
    public Car Car { get; set; }
    public List<Parent> Parents = new List<Parent>();
    public List<Children> Childrens = new List<Children>();
    public List<Pokemon> Pokemons = new List<Pokemon>();

}

