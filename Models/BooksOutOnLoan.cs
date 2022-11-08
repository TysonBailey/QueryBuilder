using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class BooksOutOnLoan : IClassModel
    {
        
        string DateIssued {get; set; }
        string DueDate { get; set; }
        string DateReturned { get; set; }
        public int ID { get; set; }

        //string BookID = Books.ID;
        //string UserID = Users.ID;
    }
}
