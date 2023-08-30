using Data.Context;
using Data.Models;
using Multiplex.Factory;
using Multiplex.Factory.ModelPrepare;
using PagedList;
using Repository;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Multiplex.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateSevice;
        private readonly ICustomerService _customerService;
        private readonly ICustomerModelFactory _customerModelFactory;
        private readonly IMultiplexFactory _multiplexFactory;

        public CustomerController(IMultiplexFactory multiplexFactory, ICustomerService customerService, ICustomerModelFactory customerModelFactory, IStateService stateSevice, ICityService cityService)
        {
            _cityService = cityService;
            _stateSevice = stateSevice;
            _customerService = customerService;
            _customerModelFactory = customerModelFactory;
            _multiplexFactory = multiplexFactory;
        }

        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var customer = _customerModelFactory.PrepareCustomerModelForIndex(searchString, currentFilter,  page);
            ViewBag.CurrentFilter = searchString;
            if (Session["Id"] == null)
                return RedirectToAction("Login", "Home");
            return View(customer);
        }

        public ActionResult Create()
        {
            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerModel model)
        {
            var customer = _customerService.GetCustomer();
            var customerlist = customer.Where(x => x.Email.Equals(model.Email)).FirstOrDefault();
            if (customerlist == null)
            {
                if (ModelState.IsValid)
                {
                    model.IsActive = true;
                    var cust = _multiplexFactory.PrepareCustomerModelToCustomer(model);
                    _customerService.InsertCustomer(cust);
                    TempData["Success"] = "Registration completed successfully. Now you can login with your Email..!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["Wrong"] = "This email already exist, Please try other mail type..!";
            }

            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");
            return View(model);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");

            var customer = _customerModelFactory.PrepareCustomerModelForEdit(id);
            if (Session["Id"] != null)
            {
                return View(customer);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Edit(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                _customerModelFactory.PrepareCustomerModelForEditPost(model);
                TempData["Success"] = "User Updated successfully...!";
                return RedirectToAction("Index");
            }

            ViewBag.state = new SelectList(_stateSevice.GetAllState().Where(X => X.IsDeleted == false), "Id", "Name");
            ViewBag.city = new SelectList(_cityService.GetAllCities().Where(X => X.IsDeleted == false), "Id", "Name");
            var ct = _customerModelFactory.PrepareCustomerModelForEdit(model.Id);
            return View(ct);
        }

        public ActionResult Delete(int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (Session["Id"] != null)
            {
                var customer = _customerService.GetCustomerById(id);

                if (customer == null)
                {
                    return HttpNotFound();
                }
                _customerService.DeleteCustomer(id);
                TempData["Success"] = "User Deleted successfully...!";
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }



    }
}
