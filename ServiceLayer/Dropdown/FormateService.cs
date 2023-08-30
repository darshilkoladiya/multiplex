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
   public class FormateService : IFormateService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMovieRepository<Formate_Movie> formateRepository;
        public FormateService(IMovieRepository<Formate_Movie> formateRepository)
        {
            this.formateRepository = formateRepository;
        }

        public void DeleteFormateMovie(int id)
        {
            Formate_Movie formate = GetFormateMovieById(id);
            formateRepository.Remove(formate);
            formateRepository.SaveChanges();
        }

        public IEnumerable<Formate_Movie> GetAllFormateMovie()
        {
            return formateRepository.GetAll();
        }

        public Formate_Movie GetFormateMovieById(int id)
        {
            return formateRepository.Get(id);
        }

        public void InsertFormateMovie(Formate_Movie formate)
        {
            formateRepository.Insert(formate);
        }

        public void UpdateFormateMovie(Formate_Movie formate)
        {
            formateRepository.Update(formate);
        }

        public IEnumerable<Formate_Movie> GetFormateByMovieId(int id)
        {
            var formate = db.formateMovies.Where(x => x.MovieId == id).ToList();
            return formate;
        }

    }
}
