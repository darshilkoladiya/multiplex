using Data.Context;
using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ScreenService : IScreenService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Screen> screenRepository;
        public ScreenService(IMultiRepository<Screen> screenRepository)
        {
            this.screenRepository = screenRepository;
        }
        public void DeleteScreen(int id)
        {
            Screen screen = GetScreenById(id);
            screen.IsDeleted = true;
            UpdateScreen(screen);
            //screenRepository.Remove(screen);
            //screenRepository.SaveChanges();
        }

        public IEnumerable<Screen> GetScreen()
        {
            return screenRepository.GetAll();
        }

        public Screen GetScreenById(int id)
        {
            return screenRepository.Get(id);
        }



        public void InsertScreen(Screen screen)
        {
            screenRepository.Insert(screen);
        }

        public void UpdateScreen(Screen screen)
        {
            screenRepository.Update(screen);
        }

        public IEnumerable<Screen> GetScreenByMultiplexId(int id)
        {
            var screen = db.screens.Where(x => x.MultiplexId == id).ToList();
            return screen;
        }
    }
}
