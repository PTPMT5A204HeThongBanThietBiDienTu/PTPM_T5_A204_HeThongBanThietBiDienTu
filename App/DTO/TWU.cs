using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TWU
    {
        string item;
        double? sumProfit;

        public string Item { get => item; set => item = value; }
        public double? SumProfit { get => sumProfit; set => sumProfit = value; }
    }
}
