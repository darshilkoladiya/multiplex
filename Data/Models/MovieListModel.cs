using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class MovieListModel
    {
        public MovieListModel()
        {
            genresMovieModelsList = new List<Genres_Movie_Model>();
            formateMovieModelsList = new List<Formate_Movie_Model>();
            languageMovieModelsList = new List<Language_Movie_Model>();
  
        }
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string GenreName { get; set; }
        public string FormateName { get; set; }
        public string LanguageName { get; set; }
        

        

        public List<Genres_Movie_Model> genresMovieModelsList { get; set; }
        public List<Formate_Movie_Model> formateMovieModelsList { get; set; }
        public List<Language_Movie_Model> languageMovieModelsList { get; set; }
    }
}
