using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL_DAL
{
    public class BLL_DAL_Bill
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Bill() { }

        public Bill getById(string id)
        {
            Bill bill = qlbh.Bills.Where(b => b.id == id).FirstOrDefault();
            return bill;
        }

        public int insert(Bill bill)
        {
            try
            {
                qlbh.Bills.InsertOnSubmit(bill);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int updateTotal(string id, double total)
        {
            try
            {
                Bill bill = qlbh.Bills.Where(b => b.id == id).FirstOrDefault();
                bill.total = total;
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int delete(string id)
        {
            try
            {
                Bill bill = qlbh.Bills.Where(b => b.id == id).FirstOrDefault();
                qlbh.Bills.DeleteOnSubmit(bill);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int updateCusId(string id, string cusId)
        {
            try
            {
                Bill bill = qlbh.Bills.Where(b => b.id == id).FirstOrDefault();
                bill.cusId = cusId;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<Bill> getAllByUserId(string userId)
        {
            List<Bill> bills = qlbh.Bills.Where(b => b.userId == userId).ToList<Bill>();
            return bills;
        }

        public int updateUserId(string id, string userId)
        {
            try
            {
                Bill bill = qlbh.Bills.Where(b => b.id == id).FirstOrDefault();
                bill.userId = userId;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<Object> getAllUnpaid()
        {
            var bills = from b in qlbh.Bills
                        orderby b.createdAt descending
                        where b.status == "unpaid"
                        join c in qlbh.Customers on b.cusId equals c.id
                        select new { b.id, b.total, b.status, b.createdAt, c.name, c.phone };

            return bills.ToList<Object>();
        }

        public int updateStatus(string id, string status)
        {
            try
            {
                Bill bill = qlbh.Bills.Where(b => b.id == id).FirstOrDefault();
                bill.status = status;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        public List<Object> getAllPaid()
        {
            var bills = from b in qlbh.Bills
                        where b.status == "paid"
                        join c in qlbh.Customers on b.cusId equals c.id
                        select new { b.id,b.total,b.status,b.createdAt.Value.UtcDateTime, c.name, c.phone };

            return bills.ToList<Object>();
        }
        public List<BillInfo> getAllPaidForExcel()
        {
            var bills = from b in qlbh.Bills
                        where b.status == "paid"
                        join c in qlbh.Customers on b.cusId equals c.id
                        select new BillInfo { Id= b.id,Total =(double)b.total,Status= b.status,CreatedAt= b.createdAt.Value.UtcDateTime,Name= c.name,Phone= c.phone };

            return bills.ToList<BillInfo>();
        }
        public List<Object> getBillBySearching(string text)
        {
            if (text != null)
            {
                var bills = from b in qlbh.Bills
                            where b.status == "paid"
                            join c in qlbh.Customers on b.cusId equals c.id
                            where c.name.Contains(text) == true || c.phone.Contains(text) == true || b.id.Contains(text) == true
                            select new { b.id, b.total, b.status, b.createdAt.Value.UtcDateTime, c.name, c.phone };
                return bills.ToList<Object>();
            }
            else
                return getAllPaid();

        }
    }
}
