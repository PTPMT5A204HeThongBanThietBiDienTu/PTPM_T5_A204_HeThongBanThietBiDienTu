using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PermissionUI
    {
        string _id;
        string _screenName;
        bool _isPermission;

        public string id { get => _id; set => _id = value; }
        public string screenName { get => _screenName; set => _screenName = value; }
        public bool isPermission { get => _isPermission; set => _isPermission = value; }

        public PermissionUI() { }
    }
}
