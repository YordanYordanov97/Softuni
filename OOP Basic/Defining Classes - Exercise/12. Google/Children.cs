using System;
using System.Collections.Generic;
using System.Text;

public class Children
{
    public string Name { get; set; }
    public string Birthday { get; set; }

    public Children(string name, string birthday)
    {
        this.Name = name;
        this.Birthday = birthday;
    }
}