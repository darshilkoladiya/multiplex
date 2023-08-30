using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IMovie_Multiplex_Serviece
    {
        IEnumerable<Movie_Multiplex> GetAllMovieMultiplex();
        Movie_Multiplex GetMovieMultiplexById(int id);
        void InsertMovieMultiplex(Movie_Multiplex movie);
        void UpdateMovieMultiplex(Movie_Multiplex movie);
        void DeleteMovieMultiplex(int id);

        IEnumerable<Movie_Multiplex> GetMovieByMultiplexId(int id);

    }
}
