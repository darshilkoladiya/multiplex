using Data.Context;
using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
    public class GenresService : IGenresService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMovieRepository<Genres_Movie> genresRepository;
        public GenresService(IMovieRepository<Genres_Movie> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public void DeleteGenresMovie(int id)
        {
            Genres_Movie genres = GetGenresMovieById(id);
            genresRepository.Remove(genres);
            genresRepository.SaveChanges();
        }

        public IEnumerable<Genres_Movie> GetAllGenresMovie()
        {
            return genresRepository.GetAll();
        }

      

        public Genres_Movie GetGenresMovieById(int id)
        {
            return genresRepository.Get(id);
        }

      

        public void InsertGenresMovie(Genres_Movie genres)
        {
            genresRepository.Insert(genres);
        }

        public void UpdateGenresMovie(Genres_Movie genres)
        {
            genresRepository.Update(genres);
        }
        public IEnumerable<Genres_Movie> GetGenresByMovieId(int id)
        {
            var gerner = db.genresMovies.Where(x => x.MovieId == id).ToList();
            return gerner;
        }
    }
}
