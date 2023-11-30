using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_OrderDetails
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_OrderDetails() { }
        public int insert(OrderDetail detail)
        {
            try
            {
                if (qlbh.OrderDetails.Where(p => p.orderId == detail.orderId && p.proId == detail.proId).FirstOrDefault() == null)
                {
                    qlbh.OrderDetails.InsertOnSubmit(detail);
                    qlbh.SubmitChanges();
                }
                else
                    update(detail);
              
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public int update(OrderDetail detail)
        {
            try
            {
                OrderDetail d = qlbh.OrderDetails.Where(p => p.orderId == detail.orderId && p.proId == detail.proId).FirstOrDefault();
                d.quantity = detail.quantity;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public List<Object> getAllDetailsById(string id)
        {
            var details = from d in qlbh.OrderDetails
                          join p in qlbh.Products on d.proId equals p.id
                            where d.orderId == id
                           select new { id=d.orderId,proId = p.id, proName = p.name, Quantity = d.quantity};
            return details.ToList<Object>();
        }
    }
}
