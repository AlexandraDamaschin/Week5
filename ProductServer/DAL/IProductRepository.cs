using ProductServer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServer.DAL
{
    interface IProductRepository : IDisposable
    {
        List<Product> GetProducts();
        Product GetProductByID(int id);
    }
}
