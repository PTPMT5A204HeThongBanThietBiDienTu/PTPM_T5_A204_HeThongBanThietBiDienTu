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
    }
}
