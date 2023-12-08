using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccessLib.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        [Column (TypeName= "decimal(6, 2)")]
        public int Price { get; set; }
    }
}
