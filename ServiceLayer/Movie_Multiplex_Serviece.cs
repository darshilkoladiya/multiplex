

using Data.Context;
using Data.Entity;
using Repository;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer
{
    public class Movie_Multiplex_Serviece : IMovie_Multiplex_Serviece
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Movie_Multiplex> movieMultiplexRepository;
        public Movie_Multiplex_Serviece(IMultiRepository<Movie_Multiplex> movieMultiplexRepository)
        {
            this.movieMultiplexRepository = movieMultiplexRepository;
        }

        public void DeleteMovieMultiplex(int id)
        {
            Movie_Multiplex movie = GetMovieMultiplexById(id);
            movie.IsDeleted = true;
            UpdateMovieMultiplex(movie);
            //movieMultiplexRepository.Remove(movie);
            //movieMultiplexRepository.SaveChanges();
        }

        public IEnumerable<Movie_Multiplex> GetAllMovieMultiplex()
        {
            return movieMultiplexRepository.GetAll();
        }



        public Movie_Multiplex GetMovieMultiplexById(int id)
        {
            return movieMultiplexRepository.Get(id);
        }

        public void InsertMovieMultiplex(Movie_Multiplex movie)
        {
            movieMultiplexRepository.Insert(movie);
        }

        public void UpdateMovieMultiplex(Movie_Multiplex movie)
        {
            movieMultiplexRepository.Update(movie);
        }

        public IEnumerable<Movie_Multiplex> GetMovieByMultiplexId(int id)
        {
            var movie = db.movieMultiplexes.Where(x => x.MultiplexId == id).ToList();
            return movie;
        }
    }
}
