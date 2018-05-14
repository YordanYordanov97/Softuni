using System;
using System.Collections.Generic;
using System.Text;



public class StreetExtraordinaire : Cat
{
    private string name;
    private int decibelsOfMeows;

    public StreetExtraordinaire(string name, int decibelsOfMeows)
    {
        SetName(name);
        this.DecibelsOfMeows = decibelsOfMeows;
    }
    public void SetName(string name)
    {
        this.Name = name;
    }
    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            this.name = value;
        }
    }
    private int DecibelsOfMeows
    {
        get
        {
            return this.decibelsOfMeows;
        }
        set
        {
            this.decibelsOfMeows = value;
        }
    }

    public override string ToString()
    {
        return $"StreetExtraordinaire {this.Name} {this.decibelsOfMeows}";
    }
}
