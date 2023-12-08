using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTechTestLib.JSON
{
    public class OrderObj
    {
        public required int CustomerId { get; set; }
        public required int ItemId { get; set; }
        public int Quainty { get; set; }
        public DateTime OrderDateTime { get; set; } 
        
    }
}
