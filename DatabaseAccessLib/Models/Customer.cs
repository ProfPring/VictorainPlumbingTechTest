using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public required string FullName { get; set; }
    }
}
