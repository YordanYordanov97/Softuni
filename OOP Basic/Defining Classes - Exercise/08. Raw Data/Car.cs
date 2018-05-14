using System;
using System.Collections.Generic;
using System.Text;


public class Car
{
    public string Model{ get; set; }
    public Engine Engine = new Engine();
    public Cargo Cargo = new Cargo();
    public List<Tire> Tires = new List<Tire>();
}