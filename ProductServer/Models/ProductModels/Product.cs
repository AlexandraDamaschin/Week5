using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProductServer.Models.ProductModels
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product ID")]
        public int ProductID { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Re-Order Level")]
        public string ReOrderLevel { get; set; }

        [Display(Name = "Price")]
        public float Price { get; set; }

        [ForeignKey("assocSupplier")]
        [Display(Name = "Supplier ID")]
        public int SupplierID { get; set; }

        public virtual Supplier assocSupplier { get; set; }
    }
}