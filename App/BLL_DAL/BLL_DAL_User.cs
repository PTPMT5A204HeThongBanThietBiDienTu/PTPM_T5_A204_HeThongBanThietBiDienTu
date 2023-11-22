using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, user.password))
                {
                    if (user.is_Active == false)
                        return 2;

                    return 1;
                }

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

        public List<Object> getAll()
        {
            var users = from u in qlbh.Users
                        where SqlMethods.Like(u.password, "$2a%")
                        join r in qlbh.Roles on u.roleId equals r.id
                        select new { u.id, u.name, u.email, u.address, u.phone, u.password, r.roleName, u.is_Active };

            return users.ToList<object>();
        }

        public bool is_ExistsPhone(string phone)
        {
            User user = qlbh.Users.Where(u => u.phone == phone).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }

        public bool is_ExistsEmail(string email)
        {
            User user = qlbh.Users.Where(u => u.email == email).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }

        public int insert(User user)
        {
            try
            {
                string passHash = BCrypt.Net.BCrypt.EnhancedHashPassword(user.password, 11);
                user.password = passHash;
                qlbh.Users.InsertOnSubmit(user);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(User newUser)
        {
            try
            {
                User user = qlbh.Users.Where(u => u.id == newUser.id).FirstOrDefault();
                if(newUser.password != string.Empty)
                {
                    if (!BCrypt.Net.BCrypt.EnhancedVerify(newUser.password, user.password))
                    {
                        string passHash = BCrypt.Net.BCrypt.EnhancedHashPassword(newUser.password, 11);
                        user.password = passHash;
                    }
                }

                user.name = newUser.name;
                user.phone = newUser.phone;
                user.email = newUser.email;
                user.address = newUser.address;
                user.roleId = newUser.roleId;
                user.is_Active = newUser.is_Active;
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
                User user = qlbh.Users.Where(u => u.id == id).FirstOrDefault();
                qlbh.Users.DeleteOnSubmit(user);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public List<Object> getAllByName(string name)
        {
            var users = from u in qlbh.Users
                        where SqlMethods.Like(u.name, "%" + name + "%") && SqlMethods.Like(u.password, "$2a%")
                        join r in qlbh.Roles on u.roleId equals r.id
                        select new { u.id, u.name, u.email, u.address, u.phone, u.password, r.roleName, u.is_Active };

            return users.ToList<object>();
        }

        public int updateInfo(User userNew)
        {
            try
            {
                User user = qlbh.Users.Where(u => u.id == userNew.id).FirstOrDefault();
                user.name = userNew.name;
                user.email = userNew.email;
                user.address = userNew.address;
                user.phone = userNew.phone;
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int updatePassword(string id, string oldPass, string newPass)
        {
            try
            {
                User user = qlbh.Users.Where(u => u.id == id).FirstOrDefault();
                if (BCrypt.Net.BCrypt.EnhancedVerify(oldPass, user.password))
                {
                    string passHash = BCrypt.Net.BCrypt.EnhancedHashPassword(newPass, 11);
                    user.password = passHash;
                    qlbh.SubmitChanges();
                    return 1;
                }
                else
                    return 2;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
