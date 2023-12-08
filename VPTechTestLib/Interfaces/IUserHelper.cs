using DatabaseAccessLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPTechTestLib.Interfaces
{
    public interface IUserHelper
    {
        public Task<Customer?> GetCustomer(int userId);
    }
}
