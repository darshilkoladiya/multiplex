using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data.Context;
using Data.Entity;
using Data.Models;
using Multiplex.Factory;
using Multiplex.Factory.ModelPrepare;
using PagedList;
using ServiceLayer;
using ServiceLayer.Dropdown;

namespace Multiplex.Controllers
{
    public class MultiplexController : Controller
    {

        private readonly ICityService _cityService;
        private readonly IStateService _stateSevice;
        private readonly IMultiplexService _multiplexService;
        private readonly IMultiplexFactory _MultiplexFactory;
        private readonly IMultiplexModelFactory _multiplexModelFactory;

        public MultiplexController(IMultiplexService multiplexService, IMultiplexFactory MultiplexFactory, IStateService stateSevice,
                                    ICityService cityService, IMultiplexModelFactory multiplexModelFactory)
        {
            _multiplexModelFactory = multiplexModelFactory;
            _cityService = cityService;
            _stateSevice = stateSevice;
            _multiplexService = multiplexService;
            _MultiplexFactory = MultiplexFactory;
        }

        #region Multiplex

        public ActionResult Index(string searchString, int? page, string searchState, string searchCity)
        {
            var multiplex = _multiplexModelFactory.PrepareMultiplexModelForIndex(searchString, page, searchState, searchCity);
            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");
            //ViewBag.CurrentFilter = searchString;

            if (Request.IsAjaxRequest())
            {
                return PartialView("_IndexPartiaView", multiplex);
            }

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(multiplex);
        }

        public ActionResult MultiplexCreate()
        {
            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");

            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return PartialView("_CreatePartialView");
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(MultiplexModel model)
        {

            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");
            if (ModelState.IsValid)
            {
                var multiplex = _MultiplexFactory.PrepareMultiplexModelToMultiplex(model);
                var check = _multiplexService.CheckData(multiplex);
                if (check == null)
                {
                    _multiplexService.InsertMultiplex(multiplex);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            return PartialView("_CreatePartialView", model);
        }

        public ActionResult EditMultiplexs(int multiplexId)
        {
            var multiplex = _multiplexModelFactory.PrepareMultiplexModelForEdit(multiplexId);

            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");
            return PartialView("_EditPartialView", multiplex);
        }

        [HttpPost]
        public ActionResult Edit(MultiplexModel model)
        {
            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");
            if (ModelState.IsValid)
            {
                _multiplexModelFactory.PrepareMultiplexModelForEditPost(model);
                return Json(true, JsonRequestBehavior.AllowGet);
            };

            var multiplex = _multiplexModelFactory.PrepareMultiplexModelForEdit(model.Id);
            return PartialView("_EditPartialView", model);
        }

        public ActionResult Delete(int id)
        {
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            _multiplexService.DeleteMultiplex(id);
            //TempData["Success"] = "Record Deleted successfully...!";
            return RedirectToAction("Index");

        }

        public ActionResult IndexPartialViewAsync(string searchString, int? page, string searchState, string searchCity)
        {
            var multiplex = _multiplexModelFactory.PrepareMultiplexModelForIndex(searchString, page, searchState, searchCity);
            ViewBag.CurrentFilter = searchString;

            return PartialView("_IndexPartiaView", multiplex);
        }

        #endregion
        public ActionResult ScreenList(int id)
        {
            return RedirectToAction("ScreenList", "Screen", new { multiplexId = id });
        }

        public ActionResult MovieList(int id)
        {
            return RedirectToAction("MovieMultiplexList", "Screen", new { multiplexId = id });
        }

        public ActionResult ShowList(int id)
        {
            return RedirectToAction("ShowList", "Screen", new { multiplexId = id });
        }
    }
}
