using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IShowService
    {
        IEnumerable<Show> GetAllShow();
        Show GetShowById(int id);
        void InsertShow(Show show);
        void UpdateShow(Show show);
        void DeleteShow(int id);

        IEnumerable<Show> GetMultiplexIdByShow(int id);

        IEnumerable<Show> GetShowByScreenId(int id);
    }
}
