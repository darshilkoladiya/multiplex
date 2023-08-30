using Data;
using Data.Context;
using Data.Entity;
using Data.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace ServiceLayer.Dropdown
{
    public class DropDownListService : IDropDownListService
    {
        private readonly IMovieService _movieService;
        private readonly IMovie_Multiplex_Serviece _movieMultiplexServiece;
        private readonly IFormateService _formateService;
        private readonly IMovieLanguageService _movieLanguageService;
        private readonly ILanguageService _languageService;
        public DropDownListService(ILanguageService languageService, IMovieLanguageService movieLanguageService, IMovieService movieService,IMovie_Multiplex_Serviece movieMultiplexServiece, IFormateService formateService)
        {
            _languageService = languageService;
            _movieLanguageService = movieLanguageService;
            _movieService = movieService;
            _movieMultiplexServiece = movieMultiplexServiece;
            _formateService = formateService;
        }
        public ShowModel PrepareDropdownForMovie(int id)
        {
            var movieList = _movieMultiplexServiece.GetMovieByMultiplexId(id).Where(x=>x.IsDeleted == false);
            
            var EnumData = from MovieEnum.Formate e in Enum.GetValues(typeof(MovieEnum.Formate))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };

            ShowModel model = new ShowModel();
            var listofMovie = new List<SelectListItem>();
            var listofFormate = new List<SelectListItem>();
            var listofLanguage = new List<SelectListItem>();

            foreach (var item in movieList)
            {
                var movie = _movieService.GetMovieById(item.MovieId);
                listofMovie.Add(new SelectListItem { Text = movie.Name, Value = movie.Id.ToString() });

                var languageList = _movieLanguageService.GetLanguageByMovieId(movie.Id);

                foreach (var l in languageList)
                {
                    var lan = _languageService.GetLanguageById(l.LanguageId);
                    listofLanguage.Add(new SelectListItem { Text = lan.Name, Value = lan.Id.ToString() });
                }
                model.LanguageList = listofLanguage;

                var formateList = _formateService.GetFormateByMovieId(movie.Id);
                foreach (var f in formateList)
                {
                    var mo = EnumData.Where(x => x.Id == f.FormateId).FirstOrDefault();
                    listofFormate.Add(new SelectListItem { Text = mo.Name, Value = mo.Id.ToString() });


                }
                model.FormateList = listofFormate;
            }
            model.MovieList = listofMovie;
            return model;
        }
    }
}
