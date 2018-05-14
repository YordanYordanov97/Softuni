using System;
using System.Collections.Generic;
using System.Text;


public class Siamese : Cat
{
    private string name;
    private int earSize;

    public Siamese(string name, int earSize)
    {
        SetName(name);
        this.EarSize = earSize;
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
    private int EarSize
    {
        get
        {
            return this.earSize;
        }
        set
        {
            this.earSize = value;
        }
    }

    public override string ToString()
    {
        return $"Siamese {this.Name} {this.EarSize}";
    }
}
