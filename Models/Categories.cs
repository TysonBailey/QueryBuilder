using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class Categories : IClassModel
    {
        public int ID { get; set; }
        string Name { get; set; }    
    }
}
