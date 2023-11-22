using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Role
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Role() { }

        public List<Role> getAll()
        {
            List<Role> roles = qlbh.Roles.Select(r => r).ToList<Role>();
            return roles;
        }

        public Role getById(string id)
        {
            Role role = qlbh.Roles.Where(r => r.id == id).FirstOrDefault();
            return role;
        }

        public bool is_ExistsName(string roleName)
        {
            Role role = qlbh.Roles.Where(r => r.roleName == roleName).FirstOrDefault();
            if (role != null)
                return true;
            return false;
        }

        public int insert(Role role)
        {
            try
            {
                qlbh.Roles.InsertOnSubmit(role);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(Role roleN)
        {
            try
            {
                Role role = qlbh.Roles.Where(r => r.id == roleN.id).FirstOrDefault();
                role.roleName = roleN.roleName;
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int delete(string id)
        {
            try
            {
                Role role = qlbh.Roles.Where(r => r.id == id).FirstOrDefault();
                qlbh.Roles.DeleteOnSubmit(role);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<Role> getAllForPermission()
        {
            List<Role> roles = qlbh.Roles.Where(r=>r.roleName != "user").ToList<Role>();
            return roles;
        }
    }
}
