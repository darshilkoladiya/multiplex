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
   public class MovieLanguageService : IMovieLanguageService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMovieRepository<Language_Movie> languageRepository;
        public MovieLanguageService(IMovieRepository<Language_Movie> languageRepository)
        {
            this.languageRepository = languageRepository;
        }

        public void DeleteLanguageMovie(int id)
        {
            Language_Movie language = GetLanguageMovieById(id);
            languageRepository.Remove(language);
            languageRepository.SaveChanges();
        }

        public IEnumerable<Language_Movie> GetAllLanguageMovie()
        {
            return languageRepository.GetAll();
        }

        public Language_Movie GetLanguageMovieById(int id)
        {
            return languageRepository.Get(id);
        }

        public void InsertLanguageMovie(Language_Movie language)
        {
            languageRepository.Insert(language);
        }

        public void UpdateLanguageMovie(Language_Movie language)
        {
            languageRepository.Update(language);
        }


        public IEnumerable<Language_Movie> GetLanguageByMovieId(int id)
        {
            var language = db.languageMovies.Where(x => x.MovieId == id).ToList();
            return language;
        }

    }
}
