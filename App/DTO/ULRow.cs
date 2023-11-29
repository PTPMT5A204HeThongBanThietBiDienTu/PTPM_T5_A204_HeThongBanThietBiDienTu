using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ULRow
    {
        string tID;
        double util, rUtil;

        public string TID { get => tID; set => tID = value; }
        public double Util { get => util; set => util = value; }
        public double RUtil { get => rUtil; set => rUtil = value; }
    }
}
