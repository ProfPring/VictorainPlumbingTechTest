using DatabaseAccessLib.Data;
using DatabaseAccessLib.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using VPTechTestLib.Interfaces;
using VPTechTestLib.JSON;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VPTechTestLib
{
    public class HandleDatabaseRequests : IHandleDatabaseRequests
    {
        /// <summary>
        /// saves the order details and the order to the database
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<bool> SaveOrderDetailsAysnc(OrderObj order) 
        {
            // Using statement ensures that the 'MerchandiseConextDB' is disposed of properly after its use.
            using MerchandiseConextDB dbConext = new MerchandiseConextDB();

            var item = await ItemExists(dbConext, order.ItemId);
            // If the item exists (its name is not null), proceed with saving the order details.
            if (item.Name != null)
            {
                try
                {
                    await dbConext.Orders.AddAsync(new Order
                    {
                        CustomerId = order.CustomerId,
                        OrderPLaced = order.OrderDateTime,
                    });

                    await dbConext.OrderDetails.AddAsync(new OrderDetails
                    {
                        Qauntity = order.Quainty,
                        ProductId = item.Id,
                        OrderId = await CreateOrderId(dbConext)
                    });

                    await dbConext.SaveChangesAsync();
                }
                catch (Exception ex) 
                {
                   /// An exception occurred during the database operations.
                   // Logging the exception would be a good practice for debugging.
                   return false;
                }
            }
            return true;
        }

        /// <summary>
        /// creates the order Id for the current order
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private async Task<int> CreateOrderId (MerchandiseConextDB dbContext) 
        {
            var latestItem =  await dbContext.OrderDetails
                       .OrderByDescending(p => p.Id)
                       .FirstOrDefaultAsync();
            if (latestItem != null)
            {
                return latestItem.OrderId + 1;
            }


            return 0;
        }

        /// <summary>
        /// checks the that the item exists in the database
        /// </summary>
        /// <param name="dbConext"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private async Task<Product> ItemExists(MerchandiseConextDB dbConext, int itemId) 
        {
            // Asynchronously retrieve the product details from the 'Products' table where the 'Id' matches the specified 'itemId'.
            var itemDetails = await dbConext.Products.Where(x => x.Id == itemId).FirstOrDefaultAsync();
            // If 'itemDetails' is not null, return the retrieved product details; otherwise, return a new instance of the 'Product' class.
            return itemDetails != null ? itemDetails : new Product();
        }
    }
}
