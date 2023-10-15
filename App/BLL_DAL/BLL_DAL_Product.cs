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
    }
}
