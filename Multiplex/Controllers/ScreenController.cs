using Data.Context;
using Data.Entity;
using Data.Models;
using Multiplex.Factory;
using Multiplex.Factory.ModelPrepare;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Multiplex.Controllers
{

    public class ScreenController : Controller
    {


        private readonly IShowPriceService _showPriceService;
        private readonly IShowService _showService;
        private readonly IFormateService _formateService;
        private readonly IMovieService _movieService;
        private readonly IMovie_Multiplex_Serviece _movieMultiplexServiece;
        private readonly ScreenService _screenService;
        private readonly SeatTypeService _seatTypeService;
        private readonly IMultiplexFactory _multiplexFactory;
        private readonly IScreenModelFactory _screenModelFactory;
        private readonly IDropDownListService _dropDownListService;

        public ScreenController(IShowService showService, ScreenService screenService, IMultiplexFactory multiplexFactory, IScreenModelFactory screenModelFactory,
                               IShowPriceService showPriceService, SeatTypeService seatTypeService, IMovie_Multiplex_Serviece movieMultiplexServiece,
                                IMovieService movieService, IFormateService formateService, IDropDownListService dropDownListService)
        {
            _showPriceService = showPriceService;
            _formateService = formateService;
            _movieService = movieService;
            _screenModelFactory = screenModelFactory;
            _movieMultiplexServiece = movieMultiplexServiece;
            _showService = showService;
            _seatTypeService = seatTypeService;
            _multiplexFactory = multiplexFactory;
            _screenService = screenService;
            _dropDownListService = dropDownListService;
        }
        #region Screen
        public ActionResult ScreenList(int multiplexId)
        {
            var screenList = _screenModelFactory.PrepareScreenModelForList(multiplexId);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(screenList);
        }

        public ActionResult CreateScreen(int id)
        {
            var screen = _screenModelFactory.PrepareScreenModelForCreateScreen(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(screen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScreen(ScreenModel model)
        {
            if (ModelState.IsValid)
            {
                var screen = _multiplexFactory.PrepareScreenModelToScreen(model);
                _screenService.InsertScreen(screen);
                TempData["Success"] = "Screen Created successfully...!";
                return RedirectToAction("ScreenList", new { multiplexId = model.MultiplexId });
            }
            return View();
        }

        public ActionResult EditScreen(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var screen = _screenModelFactory.PrepareScreenModelForEditScreen(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(screen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditScreen(ScreenModel model)
        {
            if (ModelState.IsValid)
            {
                _screenModelFactory.PrepareScreenForEditPost(model);
                TempData["Success"] = "Screen Updated successfully...!";
                return RedirectToAction("ScreenList", new { multiplexId = model.MultiplexId });
            }
            return View();
        }

        public ActionResult DeleteScreen(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var screen = _screenService.GetScreenById(id);
            _screenService.DeleteScreen(id);
            TempData["AlertMessage"] = "Screen Deleted successfully...!";
            return RedirectToAction("ScreenList", new { multiplexId = screen.MultiplexId });

        }
        #endregion

        #region SeatType

        public ActionResult SeatTypeList(int ScreenId)
        {
            var seatType = _screenModelFactory.PrepareScreenListModelForList(ScreenId);

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");

            return View(seatType);
        }

        public ActionResult CreateSeatType(int ScreenId)
        {
            var seatType = _screenModelFactory.PrepareSeatTypeModelForCreateSeatType(ScreenId);

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");

            return View(seatType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeatType(SeatTypeModel model)
        {
            if (ModelState.IsValid)
            {
                var screen = _multiplexFactory.PrepareSeatTypeModelToSeatType(model);
                _seatTypeService.InsertSeatType(screen);
                TempData["Success"] = "SeatType Added successfully...!";
                return RedirectToAction("SeatTypeList", new { ScreenId = model.ScreenId });
            }

            return View();
        }

        public ActionResult EditSeatType(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var seatType = _screenModelFactory.PrepareSeatTypeModelForEditEditSeatType(id);

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");

            return View(seatType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSeatType(SeatTypeModel model)
        {
            if (ModelState.IsValid)
            {
                _screenModelFactory.PrepareSeatTypeForEditPost(model);
                TempData["Success"] = "SeatType Updated successfully...!";
                return RedirectToAction("SeatTypeList", new { ScreenId = model.ScreenId });
            }
            return View();
        }

        public ActionResult DeleteSeatType(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["Id"] != null)
            {
                var Type = _seatTypeService.GetSeatTypeById(id);
                if (Type == null)
                {
                    return HttpNotFound();
                }
                _seatTypeService.DeleteSeatType(id);
                TempData["Success"] = "SeatType Deleted successfully...!";
                return RedirectToAction("SeatTypeList", new { ScreenId = Type.ScreenId });
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        #endregion

        #region Multiplex Base Movie
        public ActionResult MovieMultiplexList(int multiplexId)
        {
            var movie = _screenModelFactory.PrepareMovieMultiplexModelForList(multiplexId);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(movie);
        }
        public ActionResult AddMovie(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var movie = _screenModelFactory.PrepareMovieMultiplexModelForAddMovie(id);
            ViewBag.movie = new SelectList(_movieService.GetAllMovie().Where(X => X.IsDeleted == false), "Id", "Name");
            return View(movie);
        }

        [HttpPost]
        public ActionResult AddMovie(Movie_Multiplex_Model model)
        {
            if (ModelState.IsValid)
            {
                var movie = _multiplexFactory.PrepareMovieMultiplexModelToMovieMultiplex(model);
                _movieMultiplexServiece.InsertMovieMultiplex(movie);
                TempData["Success"] = "Movie Added successfully...!";
                return RedirectToAction("MovieMultiplexList", new { multiplexId = model.MultiplexId });
            }
            var movieList = _screenModelFactory.PrepareMovieMultiplexModelForAddMovie(model.MultiplexId);
            ViewBag.movie = new SelectList(_movieService.GetAllMovie().Where(X => X.IsDeleted == false), "Id", "Name");
            return View(movieList);
        }
        public ActionResult EditMovieMultiplex(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var movie = _screenModelFactory.PrepareMovieMultiplexModelForEditMovieMultiplex(id);
            ViewBag.movie = new SelectList(_movieService.GetAllMovie().Where(X => X.IsDeleted == false), "Id", "Name");
            return View(movie);
        }
        [HttpPost]
        public ActionResult EditMovieMultiplex(Movie_Multiplex_Model model)
        {

            if (ModelState.IsValid)
            {
                _screenModelFactory.PrepareMovieMultiplexForEditPost(model);
                TempData["Success"] = "Movie Updated successfully...!";
                return RedirectToAction("MovieMultiplexList", new { multiplexId = model.MultiplexId });
            }
            return View();
        }
        public ActionResult DeleteMovieMultiplex(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var movie = _movieMultiplexServiece.GetMovieMultiplexById(id);
            _movieMultiplexServiece.DeleteMovieMultiplex(id);
            TempData["AlertMessage"] = "Movie Deleted successfully...!";
            return RedirectToAction("MovieMultiplexList", new { multiplexId = movie.MultiplexId });
        }
        #endregion

        #region Show
        public ActionResult ShowList(int multiplexId)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var show = _screenModelFactory.PrepareShowModelForList(multiplexId);
            return View(show);
        }
        public ActionResult CreateShow(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var s1 = _dropDownListService.PrepareDropdownForMovie(id);
            var show = _screenModelFactory.PrepareShowModelForCreateShow(id);
            show.MovieList = s1.MovieList;
            show.FormateList = s1.FormateList;
            show.LanguageList = s1.LanguageList;
            ViewBag.Screen = new SelectList(_screenService.GetScreen().Where(X => X.IsDeleted == false && X.MultiplexId == show.MultiplexId), "Id", "Name");
            return View(show);
        }

        [HttpPost]
        public ActionResult CreateShow(ShowModel model)
        {
            if (ModelState.IsValid)
            {
                var show = _multiplexFactory.PrepareShowModelToShow(model);
                _showService.InsertShow(show);
                TempData["Success"] = "Show Added successfully...!";
                return RedirectToAction("ShowList", new { multiplexId = model.MultiplexId });
            }
            return View();
        }

        public ActionResult EditShow(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var show = _screenModelFactory.PrepareShowModelForEditShow(id);
            var s1 = _dropDownListService.PrepareDropdownForMovie(show.MultiplexId);
            show.MovieList = s1.MovieList;
            show.FormateList = s1.FormateList;
            show.LanguageList = s1.LanguageList;

            ViewBag.Screen = new SelectList(_screenService.GetScreen().Where(X => X.IsDeleted == false && X.MultiplexId == show.MultiplexId), "Id", "Name");

            return View(show);
        }

        [HttpPost]
        public ActionResult EditShow(ShowModel model)
        {
            _screenModelFactory.PrepareShowForEditPost(model);
            TempData["Success"] = "Show Updated successfully...!";
            return RedirectToAction("ShowList", new { multiplexId = model.MultiplexId });

        }

        public ActionResult DeleteShow(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var show = _showService.GetShowById(id);
            _showService.DeleteShow(id);
            TempData["AlertMessage"] = "Show Deleted successfully...!";
            return RedirectToAction("ShowList", new { multiplexId = show.MultiplexId });
        }
        #endregion

        #region Show Price
        public ActionResult ShowPriceList(int showId)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var price = _screenModelFactory.PrepareShowPriceModelForList(showId);
            return View(price);
        }
        public ActionResult SetPrice(int seatId, int showId)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var price = _screenModelFactory.PrepareShowPriceModelForSetPrice(seatId, showId);
            return View(price);
        }

        [HttpPost]
        public ActionResult SetPrice(ShowPriceModel model)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            var price = _showPriceService.GetShowPriceById(model.Id);
            price.Price = model.Price;
            _showPriceService.UpdateShowPrice(price);
            return RedirectToAction("ShowPriceList", new { showId = model.ShowId });
        }
        #endregion
    }
}