using ProductServer.Models.ProductModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductServer.DAL
{
    interface ISupplierRepository : IRepository<Supplier>
    {
        //method implemented in SupplierProductRepository
        Task<IList<Product>> SupplierProducts();
    }
}
