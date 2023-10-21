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
            int skip = (page - 1) * 5;
            int take = 5;
            List<Product> products = qlbh.Products.Select(p => p).Skip(skip).Take(take).ToList<Product>();
            return products;
        }

        public List<Product> getAllByCatIdAndBraId(string catId, string braId, int page)
        {
            int skip = (page - 1) * 5;
            int take = 5;
            List<Product> products = qlbh.Products.Where(p => p.catId == catId && p.braId == braId).Skip(skip).Take(take).ToList<Product>();
            return products;
        }

        public int getQuantityOfProduct(string id)
        {
            Product product = qlbh.Products.Where(p => p.id == id).FirstOrDefault();
            return product.quantity;
        }
    }
}
