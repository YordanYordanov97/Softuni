using System;
using System.Collections.Generic;
using System.Text;

public class Cymric : Cat
{
    private string name;
    private double furLength;

    public Cymric(string name, double furLength)
    {
        SetName(name);
        this.FurLength = furLength;
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
    private double FurLength
    {
        get
        {
            return this.furLength;
        }
        set
        {
            this.furLength = value;
        }
    }

    public override string ToString()
    {
        return $"Cymric {this.Name} {this.FurLength:f2}";
    }
}
