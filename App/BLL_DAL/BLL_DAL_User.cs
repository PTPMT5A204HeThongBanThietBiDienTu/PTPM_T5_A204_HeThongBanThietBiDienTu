using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt;

namespace BLL_DAL
{
    public class BLL_DAL_User
    {
        QLBHDataContext qlbh = new QLBHDataContext();
        public BLL_DAL_User() { }

        public int checkEmailAndPassword(string email, string password)
        {
            User user = qlbh.Users.Where(u => u.email == email).FirstOrDefault();
            if (user == null)
                return -1;
            else
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.password))
                    return 1;

                return 0;
            }
        }

        public User getInfo(string email)
        {
            User user = qlbh.Users.Where(u => u.email == email).FirstOrDefault();
            return user;
        }

        public List<User> getAllByRoleId(string roleId)
        {
            List<User> users = qlbh.Users.Where(u => u.roleId == roleId).ToList<User>();
            return users;
        }
    }
}
