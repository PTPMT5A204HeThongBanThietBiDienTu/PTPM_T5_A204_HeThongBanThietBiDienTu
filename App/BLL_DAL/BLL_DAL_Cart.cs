using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Cart
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Cart() {  }

        public List<Object> getAllByUserId(string userId)
        {
            var carts = from c in qlbh.Carts where c.userId == userId
                        join p in qlbh.Products on c.proId equals p.id
                        select new { p.id, p.name, c.quantity };
            return carts.ToList<Object>();
        }

        public bool isExistsProduct(Cart cartN)
        {
            Cart cart = qlbh.Carts.Where(c => c.proId == cartN.proId && c.userId == cartN.userId).FirstOrDefault();
            if (cart != null)
                return true;
            return false;
        }

        public int insert(Cart cart)
        {
            try
            {
                qlbh.Carts.InsertOnSubmit(cart);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int updateAdd(Cart cartN)
        {
            try
            {
                Cart cart = qlbh.Carts.Where(c => c.proId == cartN.proId && c.userId == cartN.userId).FirstOrDefault();
                cart.quantity += 1;
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(Cart cartN)
        {
            try
            {
                Cart cart = qlbh.Carts.Where(c => c.proId == cartN.proId && c.userId == cartN.userId).FirstOrDefault();
                cart.quantity = cartN.quantity;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int delete(string proId, string userId)
        {
            try
            {
                Cart cart = qlbh.Carts.Where(c => c.proId == proId && c.userId == userId).FirstOrDefault();
                qlbh.Carts.DeleteOnSubmit(cart);
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
