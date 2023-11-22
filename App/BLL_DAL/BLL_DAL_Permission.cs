using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL_DAL
{
    public class BLL_DAL_Permission
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Permission() { }

        public List<object> getAllByRoleId(string id)
        {
            var pers = from ps in qlbh.Permissions
                       where ps.roleId == id && ps.is_Permission == true
                       join sc in qlbh.Screens on ps.screenId equals sc.id
                       select new { ps.screenId, sc.screenName, ps.is_Permission };

            return pers.ToList<object>();
        }

        public List<PermissionUI> getAll(string roleId)
        {
            var pers = from ps in qlbh.Permissions
                       where ps.roleId == roleId
                       join r in qlbh.Roles
                       on ps.roleId equals r.id
                       join sc in qlbh.Screens on ps.screenId equals sc.id
                       select new { ps.screenId, sc.screenName, ps.is_Permission };

            List<PermissionUI> pUIs = new List<PermissionUI>();
            foreach(var per in pers)
            {
                PermissionUI pUI = new PermissionUI();
                pUI.id = per.GetType().GetProperty("screenId").GetValue(per).ToString();
                pUI.screenName = per.GetType().GetProperty("screenName").GetValue(per).ToString();
                pUI.isPermission = bool.Parse(per.GetType().GetProperty("is_Permission").GetValue(per).ToString());
                pUIs.Add(pUI);
            }
            return pUIs;
        }

        public int insert(Permission permisson)
        {
            try
            {
                qlbh.Permissions.InsertOnSubmit(permisson);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int deleteByRoleIdAndScreenId(string roleId, string screenId)
        {
            try
            {
                Permission per = qlbh.Permissions.Where(p => p.roleId == roleId && p.screenId == screenId).FirstOrDefault();
                qlbh.Permissions.DeleteOnSubmit(per);
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(string roleId, string screenId, bool is_Per)
        {
            try
            {
                Permission permission = qlbh.Permissions.Where(p => p.roleId == roleId && p.screenId == screenId).FirstOrDefault();
                permission.is_Permission = is_Per;
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
