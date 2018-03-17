﻿using ProductServer.DAL;
using ProductServer.DTO;
using ProductServer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProductServer.Controllers.API
{
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

        // Task 2
        [HttpPost]
        [Route("add/SupplierProduct")]
        public async Task<Product> postProductSupplier(ProductDto newProduct)
        {
            // Here we get a Product DTO and create a new product based on the DTO 
            // with associated supplier 
            // We call the Post Entity method to add and update the db context
            // We return the resulting inserted object which ha all the fields filled in 
            // as a result of the Insertion
            Product inserted = await (context as IProductRepository).PostEntity(
                new Product
                {
                    Description = newProduct.Description,
                    assocSupplier = new Supplier
                    {
                        Address = newProduct.Supplier.Address,
                        Name = newProduct.Supplier.Name
                    },
                    Price = newProduct.Price,
                    Quantity = newProduct.Quantity,
                    ReOrderLevel = newProduct.ReOrderLevel
                });
            return inserted;
        }
        // Task 3 But you would have to change the model as deletes are cascading at the moment
        [HttpDelete]
        [Route("delete/Supplier/{sid:int}")]
        public async Task<Supplier> DeleteSupplier(int sid)
        {
            return await (context as ISupplierRepository).delete(sid);
        }

        [HttpGet]
        [Route("get/SupplierWithProducts/Sname/{name}")]
        public SupplierDto GetSupplierWithProducts(string name)
        {
            // This query is long winded because the navigation propoerty only goes from Product  to Supplier.
            // Two way navigation or more natural navigation from Supplier to Product Master to Detail would yield 
            // more compact queries.
            // Could and should move all this logic in the Iproduct repository interface  
            Supplier supplier = (context as ISupplierRepository).getEntities().Result // Result of async task is a supplier list
                                 .FirstOrDefault(s => s.Name == name); // Should only be one
            // if there is a supplier then get all the products that this supplier supplies
            if (supplier != null)
            {
                // Construct Supplier DTO. Auto Mapper would be good here.
                SupplierDto sDTO = new SupplierDto
                {
                    ID = supplier.ID,
                    Name = supplier.Name,
                    Address = supplier.Address,
                    Products = new List<ProductDto>()
                };
                // 
                var products = (context as IProductRepository).getEntities().Result.ToList().Where(p => p.SupplierID == supplier.SupplierID);
                foreach (var p in products)
                {
                    sDTO.Products.Add(new ProductDto
                    {
                        ProductID = p.ProductID,
                        Description = p.Description,
                        SupplierID = p.SupplierID,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        ReOrderLevel = p.ReOrderLevel
                    });
                }
                return sDTO;
            }
            return null;
        }
    }
}