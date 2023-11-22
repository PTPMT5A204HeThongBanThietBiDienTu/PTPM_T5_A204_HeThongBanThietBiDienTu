using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DAL
{
    public class BLL_DAL_Screen
    {
        QLBHDataContext qlbh = new QLBHDataContext();

        public BLL_DAL_Screen() { }

        public List<Screen> getAll()
        {
            List<Screen> screens = qlbh.Screens.Select(s => s).ToList<Screen>();
            return screens;
        }

        public Screen getById(string id)
        {
            Screen screen = qlbh.Screens.Where(s => s.id == id).FirstOrDefault();
            return screen;
        }

        public bool is_ExistsName(string screenName)
        {
            Screen screen = qlbh.Screens.Where(s => s.screenName == screenName).FirstOrDefault();
            if (screen != null)
                return true;
            return false;
        }

        public int insert(Screen screen)
        {
            try
            {
                qlbh.Screens.InsertOnSubmit(screen);
                qlbh.SubmitChanges();
                return 1;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public int update(Screen screenN)
        {
            try
            {
                Screen screen = qlbh.Screens.Where(s => s.id == screenN.id).FirstOrDefault();
                screen.screenName = screenN.screenName;
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
                Screen screen = qlbh.Screens.Where(s => s.id == id).FirstOrDefault();
                qlbh.Screens.DeleteOnSubmit(screen);
                qlbh.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
    }
}
