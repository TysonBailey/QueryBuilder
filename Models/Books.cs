using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class Books : IClassModel
    {
        public int ID { get; set; }
        string Title { get; set; }
        string ISBN { get; set; }
        string DateOfPublication { get; set; }
    }
}
