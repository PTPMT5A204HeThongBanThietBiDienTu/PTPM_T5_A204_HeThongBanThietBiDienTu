using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Receipt
    {
        QLBHDataContext qlbh = new QLBHDataContext();
        public int insert(Receipt rc)
        {
            try
            {
                if (qlbh.Receipts.FirstOrDefault(t => t.id == rc.id) == null)
                {
                    qlbh.Receipts.InsertOnSubmit(rc);
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
    }
}
