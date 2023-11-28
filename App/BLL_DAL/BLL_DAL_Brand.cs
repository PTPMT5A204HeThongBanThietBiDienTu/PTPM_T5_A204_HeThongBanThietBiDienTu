using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Brand
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Brand() { }

        public List<Brand> getAll()
        {
            List<Brand> brands = qlbh.Brands.Select(b => b).ToList<Brand>();
            return brands;
        }

        public List<Object> getAllByCatId(string catId)
        {
            var brands = from b in qlbh.Brands
                         join cb in qlbh.Category_Brands on b.id equals cb.braId
                         where cb.catId == catId
                         select new { b.id, b.name };

            return brands.ToList<Object>();
        }

        public bool is_ExistsName(string brandName)
        {
            Brand brand = qlbh.Brands.Where(b => b.name == brandName).FirstOrDefault();
            if (brand != null)
                return true;
            return false;
        }

        public int insert(Brand brand)
        {
            try
            {
                qlbh.Brands.InsertOnSubmit(brand);
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(Brand brandN)
        {
            try
            {
                Brand brand = qlbh.Brands.Where(b => b.id == brandN.id).FirstOrDefault();
                brand.name = brandN.name;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int delete(string id)
        {
            try
            {
                Brand brand = qlbh.Brands.Where(b => b.id == id).FirstOrDefault();
                qlbh.Brands.DeleteOnSubmit(brand);
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
