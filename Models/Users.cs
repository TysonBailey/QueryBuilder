using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    internal class Users : IClassModel
    {
        public int ID { get; set; }
        string UserName { get; set; }
        string UserAddress { get; set; }
        string OtherUserDetails { get; set; } 
        int AmountOfFine { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set;  }
    }
}
