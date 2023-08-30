using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
    public interface IFormateService
    {
        IEnumerable<Formate_Movie> GetAllFormateMovie();
        Formate_Movie GetFormateMovieById(int id);
        void InsertFormateMovie(Formate_Movie formate);
        void UpdateFormateMovie(Formate_Movie formate);
        void DeleteFormateMovie(int id);

        IEnumerable<Formate_Movie> GetFormateByMovieId(int id);
    }
}
