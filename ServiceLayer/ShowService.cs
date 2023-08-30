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
    public class ShowService : IShowService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Show> showRepository;
        public ShowService(IMultiRepository<Show> showRepository)
        {
            this.showRepository = showRepository;
        }

        public void DeleteShow(int id)
        {
            Show show = GetShowById(id);
            show.IsDeleted = true;
            UpdateShow(show);
            //showRepository.Remove(show);
            //showRepository.SaveChanges();
        }

        public IEnumerable<Show> GetAllShow()
        {
            return showRepository.GetAll();
        }

        public Show GetShowById(int id)
        {
            return showRepository.Get(id);
        }

        public void InsertShow(Show show)
        {
            showRepository.Insert(show);
        }

        public void UpdateShow(Show show)
        {
            showRepository.Update(show);
        }
        public IEnumerable<Show> GetMultiplexIdByShow(int id)
        {
            var show = db.shows.Where(x => x.MultiplexId == id).ToList();
            return show;
        }

        public IEnumerable<Show> GetShowByScreenId(int id)
        {
            var show = db.shows.Where(x => x.ScreenId == id);
            return show;
        }
    }
}
