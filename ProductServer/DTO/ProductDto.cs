using ProductServer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductServer.DTO
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int ReOrderLevel { get; set; }
        public float Price { get; set; }
        public int SupplierID { get; set; }
        public virtual Supplier assocSupplier { get; set; }
    }
}