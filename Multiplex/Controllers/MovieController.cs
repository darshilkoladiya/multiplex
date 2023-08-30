using Data.Entity;
using Data.Models;
using Multiplex.Factory;
using Multiplex.Factory.ModelPrepare;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Multiplex.Controllers
{
    public class MovieController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly IMovieService _movieService;
        private readonly IGenresService _genresService;
        private readonly IFormateService _formateService;
        private readonly IMovieLanguageService _movieLanguageService;

        private readonly IMovieModelFactory _movieModelFactory;

        public MovieController(IMovieLanguageService movieLanguageService, IFormateService formateService, IGenresService genresService, IMovieService movieService, IMovieModelFactory movieModelFactory,
                                ILanguageService languageService)
        {
            _movieLanguageService = movieLanguageService;
            _formateService = formateService;
            _genresService = genresService;
            _languageService = languageService;
            _movieModelFactory = movieModelFactory;
            _movieService = movieService;
        }

        #region Main Table

        #region Movie Crud
        public ActionResult MovieList(string searchString, string currentFilter, int? page)
        {
            var movie = _movieModelFactory.PrepareMovieForIndex(searchString, currentFilter, page);
            ViewBag.CurrentFilter = searchString;
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(movie);
        }
        public ActionResult CreateMovie()
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult CreateMovie(MovieModel model)
        {

            if (ModelState.IsValid)
            {
                var movie = _movieModelFactory.PrepareMovieForCreateMovie(model);
                _movieService.InsertMovie(movie);
                TempData["Success"] = "Movie Added successfully...!";
                return RedirectToAction("MovieList");
            }
            return View();
        }

        public ActionResult EditMovie(int id)
        {
            var movie = _movieModelFactory.prepareMovieModelForEditMovie(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(movie);
        }

        [HttpPost]
        public ActionResult EditMovie(MovieModel model)
        {

            if (ModelState.IsValid)
            {
                _movieModelFactory.PrepareMovieForEditPostMovie(model);
                TempData["Success"] = "Movie Updated successfully...!";
                return RedirectToAction("MovieList");

            }
            var movie = _movieModelFactory.prepareMovieModelForEditMovie(model.Id);
            return View(movie);
        }

        public ActionResult DeleteMovie(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var movie = _movieService.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            _movieService.DeleteMovie(id);
            TempData["Success"] = "Movie Deleted successfully...!";
            return RedirectToAction("MovieList");
        }

        public ActionResult MovieGenreList(int Id)
        {
            return RedirectToAction("MovieGenres", new { movieId = Id });
        }
        
        public ActionResult MovieFormateList(int Id)
        {
            return RedirectToAction("MovieFormate", new { movieId = Id });
        }
        public ActionResult MovieLanguageList(int Id)
        {
            return RedirectToAction("MovieLanguage", new { movieId = Id });
        }

        #endregion

        #region Language Crud
        public ActionResult LanguageList(string searchString, string currentFilter, int? page)
        {
            var language = _movieModelFactory.PrepareLanguageForList(searchString, currentFilter, page);
            ViewBag.CurrentFilter = searchString;
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(language);
        }
        public ActionResult CreateLanguage()
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }
        [HttpPost]
        public ActionResult CreateLanguage(Language model)
        {
            if (ModelState.IsValid)
            {
                _languageService.InsertLanguage(model);
                TempData["Success"] = "Language Added successfully...!";
                return RedirectToAction("LanguageList");
            }
            return View();
        }
        public ActionResult EditLanguage(int id)
        {
            var language = _movieModelFactory.PrepareLanguageForEdit(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(language);
        }
        [HttpPost]
        public ActionResult EditLanguage(Language model)
        {
            _movieModelFactory.PrepareLanguageForEditPost(model);
            TempData["Success"] = "Language Updated successfully...!";
            return RedirectToAction("LanguageList");
        }

        public ActionResult DeleteLanguage(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var language = _languageService.GetLanguageById(id);
            if (language == null)
            {
                return HttpNotFound();
            }
            _languageService.DeleteLanguage(id);
            TempData["Success"] = "Language Deleted successfully...!";
            return RedirectToAction("LanguageList");
        }
        #endregion

        #endregion

        #region Genres
        public ActionResult MovieGenres(int movieId)
        {
            var genresList = _movieModelFactory.PrepareGenresMovieModelForList(movieId);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(genresList);
        }
        public ActionResult CreateGenres(int id)
        {
            var genres = _movieModelFactory.PrepareGenresMovieModelForCreateGenres(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(genres);
        }

        public ActionResult SaveSelectedGenre(string genreIds, int movieId)
        {
            string[] Id = genreIds.Split(',');
            foreach (var item in Id)
            {
                if (item != "")
                {
                    var genreMovie = new Genres_Movie()
                    {
                        MovieId = movieId,
                        GenresId = Convert.ToInt32(item)
                    };
                    _genresService.InsertGenresMovie(genreMovie);
                    TempData["Success"] = "Genre Added successfully...!";
                }

            }
            return Json(new
            {
                status = true,
                movieId = movieId
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteGenre(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var genres = _genresService.GetGenresMovieById(id);
            _genresService.DeleteGenresMovie(id);
            TempData["Success"] = "Genre Deleted successfully...!";
            return RedirectToAction("MovieGenres", "Movie", new { movieId = genres.MovieId });
        }
        #endregion

        #region Formate
        public ActionResult MovieFormate(int movieId)
        {
            var formate = _movieModelFactory.PrepareFormateMovieModelForList(movieId);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(formate);
        }

        public ActionResult CreateFormate(int id)
        {
            var formate = _movieModelFactory.PrepareFormateMovieModelForCreateFormate(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(formate);
        }

        public ActionResult SaveSelectedFormate(string formateIds, int movieId)
        {
            string[] Id = formateIds.Split(',');
            foreach (var item in Id)
            {
                if (item != "")
                {
                    var fomateMovie = new Formate_Movie()
                    {
                        MovieId = movieId,
                        FormateId = Convert.ToInt32(item)
                    };
                   _formateService.InsertFormateMovie(fomateMovie);
                    TempData["Success"] = "Formate Added successfully...!";
                }
            }
            return Json(new
            {
                status = true,
                movieId = movieId
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteFormate(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var formates = _formateService.GetFormateMovieById(id);
            _formateService.DeleteFormateMovie(id);
            TempData["Success"] = "Formate Deleted successfully...!";
            return RedirectToAction("MovieFormate", "Movie", new { movieId = formates.MovieId });
        }
        #endregion

        #region Language
        public ActionResult MovieLanguage(int movieId)
        {
            var language = _movieModelFactory.PrepareLanguageMovieModelForList(movieId);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(language);
        }

        public ActionResult CreateMovieLanguage(int id)
        {
            var language = _movieModelFactory.PrepareLanguageMovieModelForCreateLanguage(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(language);
        }
        public ActionResult SaveSelectedLanguage (string languageIds, int movieId)
        {
            string[] Id = languageIds.Split(',');
            foreach (var item in Id)
            {
                if (item != "")
                {
                    var languageMovie = new Language_Movie()
                    {
                        MovieId = movieId,
                        LanguageId = Convert.ToInt32(item)
                    };
                    _movieLanguageService.InsertLanguageMovie(languageMovie);
                    TempData["Success"] = "Language Added successfully...!";
                }

            }
            return Json(new
            {
                status = true,
                movieId = movieId
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMovieLanguage(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var laguage = _movieLanguageService.GetLanguageMovieById(id);
            _movieLanguageService.DeleteLanguageMovie(id);
            TempData["Success"] = "Language Deleted successfully...!";
            return RedirectToAction("MovieLanguage", "Movie", new { movieId = laguage.MovieId });
        }
        #endregion




    }
}