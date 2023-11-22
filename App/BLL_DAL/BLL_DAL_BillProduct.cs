using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_BillProduct
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_BillProduct() { }

        public List<Object> getAllByBillId(string billId)
        {
            var billPs = from bp in qlbh.BillProducts
                         where bp.billId == billId
                         join p in qlbh.Products on bp.proId equals p.id
                         select new { bp.proId, p.name, bp.price, bp.quantity };
            return billPs.ToList<Object>();
        }

        public int insert(BillProduct billProduct)
        {
            try
            {
                qlbh.BillProducts.InsertOnSubmit(billProduct);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int deleteAll(string billId)
        {
            try
            {
                List<BillProduct> billProducts = qlbh.BillProducts.Where(b => b.billId == billId).ToList<BillProduct>();
                qlbh.BillProducts.DeleteAllOnSubmit(billProducts);
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
