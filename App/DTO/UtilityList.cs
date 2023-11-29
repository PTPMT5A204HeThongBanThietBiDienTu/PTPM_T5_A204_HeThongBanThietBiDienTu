using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UtilityList
    {
        string item;
        double sumUtil, sumRUtil;
        List<ULRow> ulRows;

        public string Item { get => item; set => item = value; }
        public double SumUtil { get => sumUtil; set => sumUtil = value; }
        public double SumRUtil { get => sumRUtil; set => sumRUtil = value; }
        public List<ULRow> UlRows { get => ulRows; set => ulRows = value; }

        public UtilityList()
        {
            this.sumUtil = 0;
            this.sumRUtil = 0;
            ulRows = new List<ULRow>();
        }
    }
}
