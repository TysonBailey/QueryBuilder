using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueryBuilder.Models;

public class Author : IClassModel 
    {
    

    public int ID { get; set; }
    public string FirstName {get; set; }
    public string Surname {get; set; }
    public Author()
    {

    }

    public Author(int ID, string FirstName, string Surname)
    {
        this.ID = ID;
        this.FirstName = FirstName;
        this.Surname = Surname;
    }
}

