using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface IScreenService
    {
        IEnumerable<Screen> GetScreen();
        IEnumerable<Screen> GetScreenByMultiplexId(int id);
        Screen GetScreenById(int id);
        void InsertScreen(Screen screen);
        void UpdateScreen(Screen screen);
        void DeleteScreen(int id);
    }
}
