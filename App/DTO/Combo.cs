using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Combo
    {
        string father;
        List<string> children;

        public string Father { get => father; set => father = value; }
        public List<string> Children { get => children; set => children = value; }

        public Combo()
        {
            children = new List<string>();
        }
    }
}
