using Data.Context;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.Entity;
using System.Web.Security;
using ServiceLayer;
using ServiceLayer.Dropdown;
using Data.Models;
using System.Collections.Generic;
using System.Net;
using Multiplex.Factory;
using Multiplex.Factory.ModelPrepare;
using Data;

namespace Multiplex.Controllers
{

    public class HomeController : Controller
    {
        private readonly IMultiplexFactory _multiplexFactory;
        private readonly IStateService _stateSevice;
        private readonly ICityService _cityService;
        private readonly ICustomerService _customerService;
        private readonly IStateCityModelFactory _stateCityModelFactory;
        private readonly IScreenService _screenService;
        private readonly IMovie_Multiplex_Serviece _movieMultiplexServiece;
        private readonly IFormateService _formateService;
        private readonly IMovieLanguageService _movieLanguageService;
        private readonly ILanguageService _languageService;

        public HomeController(IMovieLanguageService movieLanguageService, IScreenService screenService, ICustomerService customerService, ICityService cityService, IStateCityModelFactory stateCityModelFactory,
                                IStateService stateSevice, IMultiplexFactory multiplexFactory, IMovie_Multiplex_Serviece movieMultiplexServiece, IFormateService formateService,
                                ILanguageService languageService)
        {
            _movieLanguageService = movieLanguageService;
            _formateService = formateService;
            _movieMultiplexServiece = movieMultiplexServiece;
            _screenService = screenService;
            _stateCityModelFactory = stateCityModelFactory;
            _multiplexFactory = multiplexFactory;
            _stateSevice = stateSevice;
            _customerService = customerService;
            _cityService = cityService;
            _languageService = languageService;
        }
        public ActionResult Index()
        {
            return View();
        }

        #region login Logout

        public ActionResult Login()
        {
            string User_Email = string.Empty;
            string User_Password = string.Empty;

            HttpCookie reqCookies = Request.Cookies["login"];

            if (reqCookies != null)
            {
                User_Email = reqCookies["Email"].ToString();
                User_Password = reqCookies["Password"].ToString();
            }
            var login = new Login()
            {
                Email = User_Email,
                Password = User_Password
            };
                
            if (reqCookies != null)
            {
                var customer = _customerService.Login(login);
                if (customer != null)
                {
                    if (customer.IsActive)
                    {
                        Session["Id"] = customer;
                        Session["Name"] = customer.Name;
                        return RedirectToAction("Index", "Multiplex");
                    }
                    TempData["Wrong"] = "Your Account is Disable. Please contact to admin";
                }
                else
                {
                    TempData["Wrong"] = "User Login Details Failed !";
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (login.RememberMe)
                {
                    HttpCookie loginInfo = new HttpCookie("login");
                    loginInfo["Email"] = login.Email;
                    loginInfo["Password"] = login.Password;
                    Response.Cookies.Add(loginInfo);
                }
                else
                {
                    var customer = _customerService.Login(login);
                    if (customer != null)
                    {
                        if (customer.IsActive)
                        {
                            Session["Id"] = customer;
                            Session["Name"] = customer.Name;
                            return RedirectToAction("Index", "Multiplex");
                        }
                        TempData["Wrong"] = "Your Account is Disable. Please contact to admin";
                    }
                    else
                    {
                        TempData["Wrong"] = "User Login Details Failed !";
                    }
                }

            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            Session.Clear();

            HttpCookie reqCookies = Request.Cookies["login"];
            Response.Cookies["login"].Expires = DateTime.Now;
            return RedirectToAction("Login");
        }
        #endregion

        #region Ajax Method

        [HttpPost]
        public ActionResult getCities(int stateId)
        {
            if (stateId != 0)
            {
                var dropDownList = new List<DropDownModel>();
                var citylist = _cityService.GetStateByCity(stateId);
                foreach (var item in citylist)
                {
                    var model = new DropDownModel()
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
                    dropDownList.Add(model);
                }
                return PartialView("DisplayDropdown", dropDownList);
            }
            else
            {

                var dropDownList = new List<DropDownModel>();
                var citylist = _cityService.GetAllCities();
                foreach (var item in citylist)
                {
                    var model = new DropDownModel()
                    {
                        Id = item.Id,
                        Name = item.Name
                    };
                    dropDownList.Add(model);
                }
                return PartialView("DisplayDropdown", dropDownList);
            }
        }


        public ActionResult GetLanguage(int movieId)
        {
            var dropDownList = new List<DropDownModel>();
            var language = _movieLanguageService.GetLanguageByMovieId(movieId);
            foreach (var item in language)
            {
                var model = new DropDownModel()
                {
                    Id = item.Id,
                    Name = _languageService.GetLanguageById(item.LanguageId).Name
                };
                dropDownList.Add(model);
            }
            return PartialView("DisplayDropdown", dropDownList);
        }

        public ActionResult GetFormate(int movieId)
        {
            var dropDownList = new List<DropDownModel>();
            var formate = _formateService.GetFormateByMovieId(movieId);
            var EnumData = from MovieEnum.Formate e in Enum.GetValues(typeof(MovieEnum.Formate))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };
            foreach (var item in formate)
            {
                var Enum = EnumData.Where(x => x.Id == item.FormateId).FirstOrDefault();
                var model = new DropDownModel()
                {
                    Id = item.Id,
                    Name = Enum.Name
                };
                dropDownList.Add(model);
            }
            return PartialView("DisplayDropdown", dropDownList);
        }



        #endregion

        #region State Crud

        public ActionResult StateList(string searchString, string currentFilter, int? page)
        {
            var state = _stateCityModelFactory.PrepareStateForList(searchString, currentFilter, page);
            ViewBag.CurrentFilter = searchString;

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");

            return View(state);
        }

        public ActionResult CreateState()
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult CreateState(States model)
        {
            if (ModelState.IsValid)
            {
                _stateSevice.InsertState(model);
                TempData["Success"] = "State Created successfully...!";
                return RedirectToAction("StateList");
            }
            return View();
        }

        public ActionResult EditState(int id)
        {
            var state = _stateCityModelFactory.PrepareStateForEdit(id);

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(state);
        }

        [HttpPost]
        public ActionResult EditState(States model)
        {
            if (ModelState.IsValid)
            {
                _stateCityModelFactory.PrepareStateForEditPost(model);
                TempData["Success"] = "State Edited successfully...!";
                return RedirectToAction("StateList");
            }
            return View();
        }

        public ActionResult DeleteState(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var state = _stateSevice.GetStateById(id);
            if (state == null)
            {
                return HttpNotFound();
            }
            _stateSevice.DeleteState(id);
            TempData["Success"] = "State Deleted successfully...!";
            return RedirectToAction("StateList");
        }

        #endregion

        #region City Crud
        public ActionResult CityList(string searchString, string currentFilter, int? page)
        {
            var city = _stateCityModelFactory.PrepareCityModelForCityList(searchString, currentFilter, page);
            ViewBag.CurrentFilter = searchString;

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(city);
        }

        public ActionResult CreateCity()
        {
            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(s => s.IsDeleted == false), "Id", "Name");

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        [HttpPost]
        public ActionResult CreateCity(CityModel model)
        {
            if (ModelState.IsValid)
            {
                var city = _multiplexFactory.PrepareCitiesModelToCities(model);
                _cityService.InsertCities(city);
                TempData["Success"] = "City Created successfully...!";
                return RedirectToAction("CityList");
            }

            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(s => s.IsDeleted == false), "Id", "Name");

            return View(model);
        }

        public ActionResult EditCity(int id)
        {
            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(s => s.IsDeleted == false), "Id", "Name");

            var city = _stateCityModelFactory.prepareCityModelForEditCity(id);
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(city);
        }

        [HttpPost]
        public ActionResult EditCity(CityModel model)
        {
            if (ModelState.IsValid)
            {
                _stateCityModelFactory.PrepareCitiesForEditPostCity(model);
                TempData["Success"] = "City Edited successfully...!";
                return RedirectToAction("CityList");
            }
            return View();
        }


        public ActionResult DeleteCity(int id)
        {
            if (Session["Id"] != null)
            {
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var city = _cityService.GetCitiesById(id);
                if (city == null)
                {
                    return HttpNotFound();
                }
                _cityService.DeleteCities(id);
                TempData["Success"] = "City Deleted successfully...!";
                return RedirectToAction("CityList");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        #endregion

    }
}