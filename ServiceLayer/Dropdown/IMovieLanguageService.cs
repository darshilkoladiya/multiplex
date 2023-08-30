using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
   public interface IMovieLanguageService
    {
        IEnumerable<Language_Movie> GetAllLanguageMovie();
        Language_Movie GetLanguageMovieById(int id);
        void InsertLanguageMovie(Language_Movie language);
        void UpdateLanguageMovie(Language_Movie language);
        void DeleteLanguageMovie(int id);

        IEnumerable<Language_Movie> GetLanguageByMovieId(int id);
    }
}
