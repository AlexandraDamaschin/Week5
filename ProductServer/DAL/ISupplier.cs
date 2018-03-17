using ProductServer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductServer.DAL
{
    interface ISupplierRepository : IRepository<Supplier>
    {
        Task<IList<Product>> SupplierProducts();
    }
}
