using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Product
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Product() { }

        public List<Product> getAllByBrandId(string id)
        {
            List<Product> products = qlbh.Products.Where(p => p.braId == id).ToList<Product>();
            return products;
        }

        public List<Product> getAllByCatId(string id)
        {
            List<Product> products = qlbh.Products.Where(p => p.catId == id).ToList<Product>();
            return products;
        }

        public List<Product> getAll(int page)
        {
            int skip = (page - 1) * 4;
            int take = 4;
            List<Product> products = qlbh.Products.Select(p => p).Skip(skip).Take(take).ToList<Product>();
            return products;
        }

        public List<Product> getAllByCatIdAndBraId(string catId, string braId, int page)
        {
            int skip = (page - 1) * 4;
            int take = 4;
            List<Product> products = qlbh.Products.Where(p => p.catId == catId && p.braId == braId && p.quantity > 0).Skip(skip).Take(take).ToList<Product>();
            return products;
        }

        public int countProductByCatIdAndBraId(string catId, string braId)
        {
            List<Product> products = qlbh.Products.Where(p => p.catId == catId && p.braId == braId).ToList<Product>();
            return products.Count;
        }

        public int getQuantityOfProduct(string id)
        {
            Product product = qlbh.Products.Where(p => p.id == id).FirstOrDefault();
            return product.quantity;
        }

        public Product getByProId(string id)
        {
            Product product = qlbh.Products.Where(p => p.id == id).FirstOrDefault();
            return product;
        }

        public int updateDecreaseQuantity(string id, int quantity = 1)
        {
            try
            {
                Product product = qlbh.Products.Where(p => p.id == id).FirstOrDefault();
                product.quantity = product.quantity - quantity;
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int updateIncreaseQuantity(string id, int quantity)
        {
            try
            {
                Product product = qlbh.Products.Where(p => p.id == id).FirstOrDefault();
                product.quantity = product.quantity + quantity;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public List<Object> getAllDataGridView()
        {
            var products = from p in qlbh.Products
                        join b in qlbh.Brands on p.braId equals b.id
                        join c in qlbh.Categories on p.catId equals c.id
                        select new { proId=p.id, proName = p.name, catName =c.name, braName=b.name, Quantity =p.quantity, Price= p.price };
            return products.ToList<Object>();
        }
        public List<Object> getProductsBySearching(string text)
        {
            if (text != null)
            {
                var products = from p in qlbh.Products
                               join b in qlbh.Brands on p.braId equals b.id
                               join c in qlbh.Categories on p.catId equals c.id
                               where p.name.Contains(text) ||b.name.Contains(text)||c.name.Contains(text)
                               select new { proId = p.id,proName =p.name, catName = c.name, braName = b.name, Quantity = p.quantity, Price = p.price };
                return products.ToList<Object>();
            }
            else
                return getAllDataGridView();

        }
    }
}
