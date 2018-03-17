using ProductServer.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProductServer.DAL
{
    public class SupplierProductRepository : IProductRepository, ISupplierRepository, IDisposable
    {
        private ProductDbContext context;

        public SupplierProductRepository(ProductDbContext context)
        {
            this.context = context;
        }
        async Task<IList<Product>> ISupplierRepository.SupplierProducts()
        {
            // As connection only goes from Products to Supplier we have to do it from the 
            // Product side but the supplier will be included
            // This would not be great in production (we would implement the navigation on Both sides)
            return await context.Products.Include("associatedSupplier").ToListAsync();
        }

        async Task<Product> IProductRepository.OrderItem(Product p, int Quantity)
        {
            if (p.Quantity - Quantity > 0)
            {
                p.Quantity -= Quantity;
                context.Entry(p).State = EntityState.Modified;
                try
                {
                    await context.SaveChangesAsync();
                    return p; // Return changed product accepted
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(p.ProductID))
                    {
                        return null;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return p; // return unchanged product
        }

        public void Dispose()
        {
            this.Dispose();
        }

        bool IProductRepository.CheckStock(int productID, int quantityPurchased)
        {
            var found = context.Products.Find(productID);
            if (found != null)
            {
                if (found.Quantity - quantityPurchased < 0) return false;
                else return true;
            }
            return false;

        }


        async Task<IList<Supplier>> IRepository<Supplier>.getEntities()
        {
            return await context.Suppliers.ToListAsync();
        }

        async Task<IList<Product>> IRepository<Product>.getEntities()
        {
            return await context.Products.ToListAsync();
        }

        async Task<Product> IRepository<Product>.GetEntity(int id)
        {
            Product product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            return product;

        }

        async Task<Supplier> IRepository<Supplier>.GetEntity(int id)
        {
            Supplier supplier = await context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return null;
            }

            return supplier;
        }

        async Task<IList<Product>> IProductRepository.GetReorderList()
        {
            return await context.Products.Where(p => p.Quantity <= p.ReOrderLevel).ToListAsync();
        }


        float IProductRepository.GetStockCost(int ProductID)
        {
            Product p = context.Products.Find(ProductID);
            if (p != null)
            {
                return (p.Price * p.Quantity);
            }
            return -999f;
        }

        async Task<Supplier> IRepository<Supplier>.PostEntity(Supplier Entity)
        {
            context.Suppliers.Add(Entity);
            await context.SaveChangesAsync();
            return Entity;
        }

        async Task<Product> IRepository<Product>.PostEntity(Product Entity)
        {
            context.Products.Add(Entity);
            await context.SaveChangesAsync();
            return Entity;

        }

        async Task<Supplier> IRepository<Supplier>.PutEntity(Supplier Entity)
        {
            context.Entry(Entity).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                return Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(Entity.SupplierID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

        }
        private bool SupplierExists(int id)
        {
            return context.Suppliers.Count(e => e.SupplierID == id) > 0;
        }
        private bool ProductExists(int id)
        {
            return context.Products.Count(e => e.ProductID == id) > 0;
        }
        async Task<Product> IRepository<Product>.PutEntity(Product Entity)
        {
            context.Entry(Entity).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
                return Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Entity.ProductID))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

        }

        async Task<Supplier> IRepository<Supplier>.delete(int id)
        {
            Supplier supplier = await context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return null;
            }
            context.Suppliers.Remove(supplier);
            await context.SaveChangesAsync();

            return supplier;

        }

        async Task<Product> IRepository<Product>.delete(int id)
        {
            Product product = await context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}