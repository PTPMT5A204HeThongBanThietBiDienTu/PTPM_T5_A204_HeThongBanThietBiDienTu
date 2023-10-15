using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Permission
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Permission() { }

        public List<Permission> getAllByRoleId(string id)
        {
            List<Permission> pers = qlbh.Permissions.Where(p => p.roleId == id).ToList<Permission>();
            return pers;
        }
    }
}
