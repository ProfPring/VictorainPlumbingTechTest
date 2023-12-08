using DatabaseAccessLib.Data;
using DatabaseAccessLib.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPTechTestLib.Interfaces;

namespace VPTechTestLib
{
    /// <summary>
    /// a class used for getting customer informaton
    /// </summary>
    public class UserHelper : IUserHelper
    {

        /// <summary>
        /// get customer by their Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Customer?> GetCustomer(int userId)
        {
            try 
            {
                using MerchandiseConextDB DBconext = new MerchandiseConextDB();

                var customer = await DBconext.Customers.Where(x => x.Id == userId).FirstOrDefaultAsync();

                if (customer == null)
                {
                    //adding logging here and returning an error may be best
                    return null;
                }
                return customer;
            }
            catch
            {
                //adding logging here would be a good idea
                return null;
            }
            
        }
    }
}
