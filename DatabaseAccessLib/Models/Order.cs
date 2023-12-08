using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderPLaced { get; set; }

        public int CustomerId { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; } = null!;

    }
}
