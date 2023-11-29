using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TotalInDate
    {
        string _ngay;
        double _total;

        public double Total { get => _total; set => _total = value; }
        public string Ngay { get => _ngay; set => _ngay = value; }
        public TotalInDate()
        {
        }
    }
}
