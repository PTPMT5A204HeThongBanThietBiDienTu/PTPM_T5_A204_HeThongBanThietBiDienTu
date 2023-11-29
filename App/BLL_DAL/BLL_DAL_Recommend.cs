using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL_DAL
{
    public class BLL_DAL_Recommend
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        private List<TWU> createListTWU(List<Product> products, List<BillProduct> billProducts)
        {
            List<TWU> twus = new List<TWU>();
            foreach (Product product in products)
            {
                TWU twu = new TWU();
                twu.Item = product.id;
                twu.SumProfit = 0;

                List<BillProduct> bPs = billProducts.FindAll(bd => bd.proId == product.id);
                foreach (BillProduct bp in bPs)
                    twu.SumProfit += (bp.price * bp.quantity);

                twus.Add(twu);
            }

            sortUpListTWU(ref twus);

            return twus;
        }

        private void sortUpListTWU(ref List<TWU> twus)
        {
            int n = twus.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (twus[i].SumProfit > twus[j].SumProfit)
                    {
                        TWU temp = twus[i];
                        twus[i] = twus[j];
                        twus[j] = temp;
                    }
                }
            }
        }

        private void filterByMinUtil(ref List<TWU> twus, ref List<Product> products, ref List<BillProduct> billProducts, double? minUtil)
        {
            List<TWU> blackList = twus.FindAll(twu => twu.SumProfit < minUtil);

            twus.RemoveAll(twu => twu.SumProfit < minUtil);

            foreach (TWU twu in blackList)
            {
                products.RemoveAll(p => p.id == twu.Item);
                billProducts.RemoveAll(bd => bd.proId == twu.Item);
            }
        }

        private void sortUpListBillProductByTWU(List<Bill> bills, ref List<BillProduct> billProducts, List<TWU> twus)
        {
            foreach (Bill b in bills)
            {
                int quantity = billProducts.FindAll(bd => bd.billId == b.id).Count;
                int index = billProducts.FindIndex(bd => bd.billId == b.id);
                int n = quantity + index;

                for (int i = index; i < n - 1; i++)
                {
                    for (int j = i + 1; j < n; j++)
                    {
                        string item1 = billProducts[i].proId;
                        TWU twu1 = twus.Find(twu => twu.Item == item1);

                        string item2 = billProducts[j].proId;
                        TWU twu2 = twus.Find(twu => twu.Item == item2);

                        if (twu1.SumProfit > twu2.SumProfit)
                        {
                            BillProduct temp = billProducts[i];
                            billProducts[i] = billProducts[j];
                            billProducts[j] = temp;
                        }
                    }
                }
            }
        }

        private List<UtilityList> createUtilityList(List<Bill> bills, List<BillProduct> billProducts)
        {
            List<UtilityList> uls = new List<UtilityList>();
            foreach (Bill b in bills)
            {
                List<BillProduct> bPs = billProducts.FindAll(bp => bp.billId == b.id);
                int n = bPs.Count;

                for (int i = 0; i < n; i++)
                {
                    ULRow ulR = new ULRow();
                    ulR.TID = bPs[i].billId;
                    ulR.Util = bPs[i].quantity * bPs[i].price;
                    ulR.RUtil = 0;
                    for (int j = i + 1; j < n; j++)
                        ulR.RUtil += (bPs[j].quantity * bPs[j].price);

                    int index = uls.FindIndex(ul => ul.Item == bPs[i].proId);
                    if (index == -1)
                    {
                        UtilityList ul = new UtilityList();
                        ul.Item = bPs[i].proId;
                        ul.SumUtil += ulR.Util;
                        ul.SumRUtil += ulR.RUtil;
                        ul.UlRows.Add(ulR);

                        uls.Add(ul);
                    }
                    else
                    {
                        uls[index].SumUtil += ulR.Util;
                        uls[index].SumRUtil += ulR.RUtil;
                        uls[index].UlRows.Add(ulR);
                    }
                }
            }
            return uls;
        }

        private void sortUpUtilityListByTWU(ref List<UtilityList> uls, List<TWU> twus)
        {
            int n = uls.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    string item1 = uls[i].Item;
                    TWU twu1 = twus.Find(twu => twu.Item == item1);

                    string item2 = uls[j].Item;
                    TWU twu2 = twus.Find(twu => twu.Item == item2);

                    if (twu1.SumProfit > twu2.SumProfit)
                    {
                        UtilityList temp = uls[i];
                        uls[i] = uls[j];
                        uls[j] = temp;
                    }
                }
            }
        }

        private string combineName(string item1, string item2, ref string commonName, ref int quantity)
        {
            string[] name1 = item1.Split(',');
            string[] name2 = item2.Split(',');
            string name = "";

            if (name1.Length > 1)
            {

                for (int i = 0; i < name1.Length; i++)
                {
                    if (name1[i] == name2[i])
                    {
                        commonName += (name1[i] + ",");
                        quantity++;
                        name = commonName;
                    }
                    else
                        name += (name1[i] + "," + name2[i] + ",");
                }

                commonName = commonName.Remove(commonName.Length - 1);
                name = name.Remove(name.Length - 1);
            }
            else
                name = item1 + "," + item2;

            return name;
        }

        private UtilityList combine2UtilityList(UtilityList ul1, UtilityList ul2, List<UtilityList> uls)
        {
            UtilityList ulNew = new UtilityList();
            string commonName = ""; int quantity = 0;

            ulNew.Item = combineName(ul1.Item, ul2.Item, ref commonName, ref quantity);

            foreach (ULRow ulRow1 in ul1.UlRows)
            {
                foreach (ULRow ulRow2 in ul2.UlRows)
                {
                    if (ulRow2.TID == ulRow1.TID)
                    {
                        ULRow ulRowNew = new ULRow();
                        ulRowNew.TID = ulRow1.TID;
                        ulRowNew.Util = (ulRow2.Util + ulRow1.Util);
                        ulRowNew.RUtil = ulRow2.RUtil;
                        if (quantity != 0)
                        {
                            UtilityList ulCommon = uls.Find(ul => ul.Item == commonName);
                            ULRow ulRow = ulCommon.UlRows.Find(ulr => ulr.TID == ulRow1.TID);
                            ulRowNew.Util -= ulRow.Util;
                        }

                        ulNew.SumUtil += ulRowNew.Util;
                        ulNew.SumRUtil += ulRowNew.RUtil;
                        ulNew.UlRows.Add(ulRowNew);
                    }
                }
            }
            return ulNew;
        }

        private void HUIMiner(List<UtilityList> uls, double? minUtil, List<UtilityList> ulsPrevious, ref List<UtilityList> huis)
        {
            int n = uls.Count;
            for (int i = 0; i < n; i++)
            {
                if (uls[i].SumUtil >= minUtil)
                    huis.Add(uls[i]);

                if (uls[i].SumUtil + uls[i].SumRUtil >= minUtil)
                {
                    List<UtilityList> ulsNew = new List<UtilityList>();
                    for (int j = i + 1; j < n; j++)
                    {
                        UtilityList ulNew = combine2UtilityList(uls[i], uls[j], ulsPrevious);
                        ulsNew.Add(ulNew);
                    }

                    HUIMiner(ulsNew, minUtil, uls, ref huis);
                }
            }
        }

        public void runAlgorithm()
        {
            List<Product> products = qlbh.Products.Select(p => p).ToList<Product>();
            List<Bill> bills = qlbh.Bills.Select(b => b).ToList<Bill>();
            List<BillProduct> billProducts = new List<BillProduct>();
            foreach (Bill bill in bills)
            {
                List<BillProduct> bPs = qlbh.BillProducts.Where(bp => bp.billId == bill.id).ToList<BillProduct>();
                foreach (BillProduct bp in bPs)
                    billProducts.Add(bp);
            }

            List<TWU> twus = createListTWU(products, billProducts);
            double? minUtil = (twus.Sum(t => t.SumProfit) / twus.Count) / 2;

            filterByMinUtil(ref twus, ref products, ref billProducts, minUtil);
            sortUpListBillProductByTWU(bills, ref billProducts, twus);

            List<UtilityList> uls = createUtilityList(bills, billProducts);
            sortUpUtilityListByTWU(ref uls, twus);

            List<UtilityList> huis = new List<UtilityList>();

            HUIMiner(uls, minUtil, uls, ref huis);

            List<Combo> cbs = new List<Combo>();
            foreach (UtilityList hui in huis)
            {
                string[] names = hui.Item.Split(',');
                if (names.Length >= 2)
                {
                    int n = names.Length;

                    int index = cbs.FindIndex(cb => cb.Father == names[n - 1]);
                    if (index == -1)
                    {
                        Combo cb = new Combo();
                        cb.Father = names[n - 1];
                        for (int i = 0; i < n - 1; i++)
                            cb.Children.Add(names[i]);
                        cbs.Add(cb);
                    }
                    else
                    {
                        for (int i = 0; i < n - 1; i++)
                        {
                            string children = cbs[index].Children.Find(c => c == names[i]);
                            if (children == null)
                                cbs[index].Children.Add(names[i]);
                        }
                    }
                }
            }

            List<Recommend> lsR = qlbh.Recommends.Select(r => r).ToList<Recommend>();
            qlbh.Recommends.DeleteAllOnSubmit(lsR);
            qlbh.SubmitChanges();

            foreach (var cb in cbs)
            {
                Recommend rc = new Recommend();
                rc.id = Guid.NewGuid().ToString();
                rc.proId = cb.Father;
                foreach (var c in cb.Children)
                    rc.accompany += (c + ",");

                qlbh.Recommends.InsertOnSubmit(rc);
                qlbh.SubmitChanges();
            }

            Console.WriteLine("Run success!");
        }
    }
}
