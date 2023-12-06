using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Order
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Order() { }
        public int insert(Order order)
        {
            try
            {
                if(qlbh.Orders.FirstOrDefault(t=>t.id==order.id)==null)
                {
                    qlbh.Orders.InsertOnSubmit(order);
                    qlbh.SubmitChanges();
                }    
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public int update(string id,string status)
        {
            try
            {
                Order o = qlbh.Orders.Where(p => p.id == id).FirstOrDefault();
                o.status = status;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public string getIdOrderInPending()
        {
            var order = qlbh.Orders.FirstOrDefault(x => x.status == "pending");
            if(order!=null)
                return order.id ;
            return "";
        }
        public List<Object> getAllOrders()
        {
            var orders = (from o in qlbh.Orders
                          where o.status == "proccessing"
                          join u in qlbh.Users on o.userId equals u.id
                          join d in qlbh.OrderDetails on o.id equals d.orderId
                          group d by new
                          {
                              o.id,
                              u.name,
                              o.createdAt,
                          } into grouped
                          orderby grouped.Key.createdAt descending
                          select new { id = grouped.Key.id, Created = grouped.Key.createdAt, Name = grouped.Key.name, TotalQuantity = grouped.Sum(d => d.quantity) }); ; ;
            return orders.ToList<Object>();
        }
    }
}
