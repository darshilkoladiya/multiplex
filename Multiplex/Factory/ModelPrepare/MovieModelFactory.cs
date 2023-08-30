using Data;
using Data.Entity;
using Data.Models;
using PagedList;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using static Multiplex.Controllers.MovieController;

namespace Multiplex.Factory.ModelPrepare
{
    public class MovieModelFactory : IMovieModelFactory
    {
        private readonly IMultiplexFactory _multiplexFactory;
        private readonly IMovieService _movieService;
        private readonly ILanguageService _languageService;
        private readonly IGenresService _genresService;
        private readonly IFormateService _formateService;
        private readonly IMovieLanguageService _movieLanguageService;

        public MovieModelFactory(IMovieLanguageService movieLanguageService, IFormateService formateService, IGenresService genresService, IMovieService movieService, IMultiplexFactory multiplexFactory, ILanguageService languageService)
        {
            _movieLanguageService = movieLanguageService;
            _formateService = formateService;
            _genresService = genresService;
            _languageService = languageService;
            _multiplexFactory = multiplexFactory;
            _movieService = movieService;

        }

        #region Main Tables Models

        #region Movie Models
        public PagedList<MovieModel> PrepareMovieForIndex(string searchString, string currentFilter, int? page)
        {
            var movie = _movieService.GetAllMovie().Where(s => s.IsDeleted == false);
            var movieModelList = new List<MovieModel>();
            foreach (var item in movie)
            {
                var model = new MovieModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    ReleaseDate = item.ReleaseDate,
                    Image = item.Image,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted
                };
                if (model.IsDeleted)
                {
                    continue;
                }
                movieModelList.Add(model);

            }
            var movieList = from a in movieModelList select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                movieList = movieModelList.Where(z => z.Name.Contains(searchString));
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return (PagedList<MovieModel>)movieList.ToPagedList(pageNumber, pageSize);
        }

        public Movie PrepareMovieForCreateMovie(MovieModel model)
        {
            var movie = _multiplexFactory.PrepareMovieModelToMovie(model);
            foreach (var file in model.Files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extention = Path.GetExtension(file.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;

                if (extention == ".jpg" || extention == ".png")
                {
                    model.Image = fileName;
                    fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Image/"), fileName);
                    file.SaveAs(fileName);
                }
            }
            movie.Image = model.Image;
            return movie;
        }

        public MovieModel prepareMovieModelForEditMovie(int id)
        {
            var getRecord = _movieService.GetMovieById(id);
            var movieModel = new MovieModel()
            {
                Name = getRecord.Name,
                ReleaseDate = getRecord.ReleaseDate,
                Image = getRecord.Image,
                IsActive = getRecord.IsActive,
                IsDeleted = getRecord.IsDeleted
            };
            return movieModel;
        }

        public Movie PrepareMovieForEditPostMovie(MovieModel model)
        {
            var movie = _movieService.GetMovieById(model.Id);
            foreach (var file in model.Files)
            {
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        string extention = Path.GetExtension(file.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;

                        if (extention == ".jpg" || extention == ".png")
                        {
                            model.Image = fileName;
                            fileName = Path.Combine(HttpContext.Current.Server.MapPath("~/Image/"), fileName);
                            file.SaveAs(fileName);
                            movie.Image = model.Image;
                        }
                    }
                }
            }
            movie.Id = model.Id;
            movie.Name = model.Name;
            movie.ReleaseDate = model.ReleaseDate;
            movie.IsActive = model.IsActive;
            movie.IsDeleted = model.IsDeleted;
            _movieService.UpdateMovie(movie);
            return movie;
        }

        #endregion

        #region Language Models
        public PagedList<Language> PrepareLanguageForList(string searchString, string currentFilter, int? page)
        {
            var language = _languageService.GetAllLanguage().Where(s => s.IsDeleted == false);
            var languagelist = from a in language select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                languagelist = language.Where(z => z.Name.Contains(searchString));
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return (PagedList<Language>)languagelist.ToPagedList(pageNumber, pageSize);
        }

        public Language PrepareLanguageForEdit(int id)
        {
            var getLanguage = _languageService.GetLanguageById(id);
            var language = new Language()
            {
                Id = getLanguage.Id,
                Name = getLanguage.Name,
                IsActive = getLanguage.IsActive,
                IsDeleted = getLanguage.IsDeleted
            };
            return language;
        }

        public Language PrepareLanguageForEditPost(Language model)
        {
            var language = _languageService.GetLanguageById(model.Id);
            language.Name = model.Name;
            language.IsActive = model.IsActive;
            language.IsDeleted = model.IsDeleted;
            _languageService.UpdateLanguage(language);
            return language;
        }

        #endregion

        #endregion

        #region Genres Model
        public MovieListModel PrepareGenresMovieModelForList(int movieId)
        {
            var genres = _genresService.GetGenresByMovieId(movieId);
            var movie = _movieService.GetMovieById(movieId);
            var movieListModel = new MovieListModel();
            var genresList = new List<Genres_Movie_Model>();
            var EnumData = from MovieEnum.Genres e in Enum.GetValues(typeof(MovieEnum.Genres))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };

            foreach (var item in genres)
            {
                var genresEnum = EnumData.Where(x => x.Id == item.GenresId).FirstOrDefault();
                var genresModel = new Genres_Movie_Model()
                {
                    Id = item.Id,
                    MovieId = item.MovieId,
                    GenresName = genresEnum.Name
                };
                genresList.Add(genresModel);
            }
            movieListModel.genresMovieModelsList = genresList;
            if (genres != null)
            {
                var firstReco = genres.FirstOrDefault();
                if (firstReco != null)
                {
                    movieListModel.Id = firstReco.MovieId;
                }
                else
                {
                    movieListModel.Id = movieId;
                }
                movieListModel.MovieName = movie.Name;

            }
            return movieListModel;
        }

        public MovieListModel PrepareGenresMovieModelForCreateGenres(int id)
        {
            var movie = _movieService.GetMovieById(id);
            var movieListModel = new MovieListModel();
            var genresList = new List<Genres_Movie_Model>();
            if (movie != null)
            {
                movieListModel.MovieId = movie.Id;
                movieListModel.MovieName = movie.Name;
            }
            var EnumData = from MovieEnum.Genres e in Enum.GetValues(typeof(MovieEnum.Genres))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };

            foreach (var item in EnumData)
            {
                var model = new Genres_Movie_Model()
                {
                    GenresId = item.Id,
                    GenresName = item.Name
                };
                var genre = _genresService.GetAllGenresMovie().Where(x => x.MovieId == movie.Id).FirstOrDefault(s => s.GenresId == item.Id);
                if (genre != null)
                {
                    continue;
                }
                genresList.Add(model);
            }
            movieListModel.genresMovieModelsList = genresList;
            return movieListModel;
        }

        #endregion

        #region Formate Model
        public MovieListModel PrepareFormateMovieModelForList(int movieId)
        {
            var formate = _formateService.GetFormateByMovieId(movieId);
            var movie = _movieService.GetMovieById(movieId);
            var movieListModel = new MovieListModel();
            var formateList = new List<Formate_Movie_Model>();
            var EnumData = from MovieEnum.Formate e in Enum.GetValues(typeof(MovieEnum.Formate))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };

            foreach (var item in formate)
            {
                var formateEnum = EnumData.Where(s => s.Id == item.FormateId).FirstOrDefault();
                var formateModel = new Formate_Movie_Model()
                {
                    Id = item.Id,
                    MovieId = item.MovieId,
                    FormateName = formateEnum.Name
                };
                formateList.Add(formateModel);
            }
            movieListModel.formateMovieModelsList = formateList;
            if (formate != null)
            {
                var firstReco = formate.FirstOrDefault();
                if (firstReco != null)
                {
                    movieListModel.Id = firstReco.MovieId;
                }
                else
                {
                    movieListModel.Id = movieId;
                }
                movieListModel.MovieName = movie.Name;
            }
            return movieListModel;
        }

        public MovieListModel PrepareFormateMovieModelForCreateFormate(int id)
        {
            var movie = _movieService.GetMovieById(id);
            var movieListModel = new MovieListModel();
            var formateList = new List<Formate_Movie_Model>();
            if (movie != null)
            {
                movieListModel.MovieId = movie.Id;
                movieListModel.MovieName = movie.Name;
            }
            var EnumData = from MovieEnum.Formate e in Enum.GetValues(typeof(MovieEnum.Formate))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };

            foreach (var item in EnumData)
            {
                var model = new Formate_Movie_Model()
                {
                    FormateId = item.Id,
                    FormateName = item.Name
                };
                var formate = _formateService.GetAllFormateMovie().Where(x => x.MovieId == movie.Id).FirstOrDefault(s => s.FormateId == item.Id);
                if (formate != null)
                {
                    continue;
                }
                formateList.Add(model);
            }
            movieListModel.formateMovieModelsList = formateList;
            return movieListModel;
        }
        #endregion

        public MovieListModel PrepareLanguageMovieModelForList(int movieId)
        {
            var laguage = _movieLanguageService.GetLanguageByMovieId(movieId);
            var movie = _movieService.GetMovieById(movieId);
            var movieListModel = new MovieListModel();
            var languageList = new List<Language_Movie_Model>();
            var languageDb = _languageService.GetAllLanguage();

            foreach (var item in laguage)
            {
                var lan = languageDb.Where(s => s.Id == item.LanguageId).FirstOrDefault();

                var model = new Language_Movie_Model()
                {
                    Id = item.Id,
                    MovieId = item.MovieId,
                    LanguageName = lan.Name
                };
                languageList.Add(model);
            }
            movieListModel.languageMovieModelsList = languageList;
            if (laguage != null)
            {
                var firstReco = laguage.FirstOrDefault();
                if (firstReco != null)
                {
                    movieListModel.Id = firstReco.MovieId;
                }
                else
                {
                    movieListModel.Id = movieId;
                }
                movieListModel.MovieName = movie.Name;
            }
            return movieListModel;
        }

        public MovieListModel PrepareLanguageMovieModelForCreateLanguage(int id)
        {
            var movie = _movieService.GetMovieById(id);
            var movieListModel = new MovieListModel();
            var languageList = new List<Language_Movie_Model>();
            if (movie != null)
            {
                movieListModel.MovieId = movie.Id;
                movieListModel.MovieName = movie.Name;
            }
            var languageDb = _languageService.GetAllLanguage().Where(x=>x.IsDeleted == false);

            foreach (var item in languageDb)
            {

                var model = new Language_Movie_Model()
                {
                    LanguageId = item.Id,
                    LanguageName = item.Name
                };

                var language = _movieLanguageService.GetAllLanguageMovie().Where(x => x.MovieId == movie.Id).FirstOrDefault(s => s.LanguageId == item.Id);
                if(language != null)
                {
                    continue;
                }
                languageList.Add(model);
            }
            movieListModel.languageMovieModelsList = languageList;
            return movieListModel;
        }
    }
}