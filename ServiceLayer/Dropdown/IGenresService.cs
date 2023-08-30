using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
    public interface IGenresService
    {
        IEnumerable<Genres_Movie> GetAllGenresMovie();
        Genres_Movie GetGenresMovieById(int id);
        void InsertGenresMovie(Genres_Movie genres);
        void UpdateGenresMovie(Genres_Movie genres);
        void DeleteGenresMovie(int id);

        IEnumerable<Genres_Movie> GetGenresByMovieId(int id);
    }
}
