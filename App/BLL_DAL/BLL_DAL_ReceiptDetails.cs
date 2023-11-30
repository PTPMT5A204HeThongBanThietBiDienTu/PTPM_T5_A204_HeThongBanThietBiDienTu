using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_ReceiptDetails
    {
        QLBHDataContext qlbh = new QLBHDataContext();
        public int insert(ReceiptDetail rcd)
        {
            try
            {
                if (qlbh.ReceiptDetails.FirstOrDefault(t => t.id == rcd.id) == null)
                {
                    qlbh.ReceiptDetails.InsertOnSubmit(rcd);
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
