using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public class MovieService : IMovieService
    {
        private IMultiRepository<Movie> movieRepository;
        public MovieService(IMultiRepository<Movie> movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public void DeleteMovie(int id)
        {
            Movie movie = GetMovieById(id);
            movie.IsDeleted = true;
            UpdateMovie(movie);
        }

        public IEnumerable<Movie> GetAllMovie()
        {
            return movieRepository.GetAll();
        }

        public Movie GetMovieById(int id)
        {
            return movieRepository.Get(id);
        }

        public void InsertMovie(Movie movie)
        {
            movieRepository.Insert(movie);
        }

        public void UpdateMovie(Movie movie)
        {
            movieRepository.Update(movie);
        }
    }
}
