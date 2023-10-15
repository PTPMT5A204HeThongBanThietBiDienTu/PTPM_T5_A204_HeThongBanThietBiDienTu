using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Category
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Category() { }

        public List<Category> getAll()
        {
            List<Category> categories = qlbh.Categories.Select(c => c).ToList<Category>();
            return categories;
        }

        public bool is_ExistsName(string categoryName)
        {
            Category category = qlbh.Categories.Where(c => c.name == categoryName).FirstOrDefault();
            if (category != null)
                return true;
            return false;
        }

        public int insert(Category category)
        {
            try
            {
                qlbh.Categories.InsertOnSubmit(category);
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(Category categoryN)
        {
            try
            {
                Category category = qlbh.Categories.Where(c => c.id == categoryN.id).FirstOrDefault();
                category.name = categoryN.name;
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
                Category category = qlbh.Categories.Where(c => c.id == id).FirstOrDefault();
                qlbh.Categories.DeleteOnSubmit(category);
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
