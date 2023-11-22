using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Customer
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Customer() { }

        public int insert(Customer customer)
        {
            try
            {
                qlbh.Customers.InsertOnSubmit(customer);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
