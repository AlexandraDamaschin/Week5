using ProductServer.DAL;
using ProductServer.Models.ProductModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonObjects;
using System.Web.Http;

namespace ProductServer.Controllers.API
{
    //authorise controller to be view by purchsesManager role
    [Authorize(Roles = "Purchases Manager")]

    [RoutePrefix("api/Purchases")]
    public class PurchasesController : ApiController
    {

        private SupplierProductRepository context;
        //private MapperConfiguration supConfig, prodConfig;

        public PurchasesController()
        {
            context = new SupplierProductRepository(new ProductDbContext());
            //prodConfig = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductDTO>());
            //supConfig = new MapperConfiguration(cfg => cfg.CreateMap<Supplier, SupplierDTO>());
        }
        // For injection
        public PurchasesController(SupplierProductRepository ctx)
        {
            context = ctx;
        }

        [HttpGet]
        [Route("Suppliers")]
        public async Task<IList<Supplier>> GetSuppliers()
        {
            return await (context as ISupplierRepository).getEntities();
        }

        [HttpGet]
        [Route("Products")]
        public async Task<IList<Product>> GetProducts()
        {
            return await (context as IProductRepository).getEntities();
        }
        // Task 5
        [HttpGet]
        [Route("ReorderList")]
        public async Task<IList<Product>> GetReorderList()
        {
            return await (context as IProductRepository).GetReorderList();
        }
    }
}