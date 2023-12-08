using DatabaseAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VPTechTestLib.JSON;

namespace VPTechTestLib.Interfaces
{
    public interface IHandleDatabaseRequests
    {
        public Task<bool> SaveOrderDetailsAysnc(OrderObj order);
    }
}
