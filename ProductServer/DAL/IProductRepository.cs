using ProductServer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServer.DAL
{
    interface IProductRepository : IRepository<Product>
    {
        //method implemented in SupplierProductRepository 
        Task<IList<Product>> GetReorderList();
        float GetStockCost(int ProductID);
        bool CheckStock(int productID, int quantityPurchased);
        Task<Product> OrderItem(Product p, int Quantity);
    }
}
