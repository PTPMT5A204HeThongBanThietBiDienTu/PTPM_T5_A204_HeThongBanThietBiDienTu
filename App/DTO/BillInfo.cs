using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillInfo
    {
        string _id;
        double _total;
        string _status;
        DateTime _createdAt;
        string _name;
        string _phone;

        public string Id { get => _id; set => _id = value; }
        public double Total { get => _total; set => _total = value; }
        public string Status { get => _status; set => _status = value; }
        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public string Name { get => _name; set => _name = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public BillInfo() { }
    }
}
