using Data.Entity;
using Data.Models;
using PagedList;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Factory.ModelPrepare
{
    public class CustomerModelFactory : ICustomerModelFactory
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateSevice;
        private readonly ICustomerService _customerService;

        public CustomerModelFactory(ICustomerService customerService, IStateService stateSevice, ICityService cityService)
        {
            _cityService = cityService;
            _stateSevice = stateSevice;
            _customerService = customerService;
        }

        public PagedList<CustomerModel> PrepareCustomerModelForIndex(string searchString, string currentFilter, int? page)
        {
            var Customerlist = _customerService.GetCustomer();
            var customer = new List<CustomerModel>();
            foreach (var item in Customerlist)
            {
                var stateList = _stateSevice.GetStateById(item.StateId);
                var cityList = _cityService.GetCitiesById(item.CityId);

                var customerModel = new CustomerModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    MobileNo = item.MobileNo,
                    CityName = cityList.Name,
                    StateName = stateList.Name,
                    Email = item.Email,
                    Password = item.Password,
                    IsDeleted = item.IsDeleted,
                    IsActive = item.IsActive,
                };
                if (customerModel.IsDeleted)
                {
                    continue;
                }
                customer.Add(customerModel);
            }

            var customerlist = from a in customer select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                customerlist = customer.Where(z => z.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return (PagedList<CustomerModel>)customerlist.ToPagedList(pageNumber, pageSize);
        }

        public CustomerModel PrepareCustomerModelForEdit(int id)
        {

            var getrecord = _customerService.GetCustomerById(id);

            var customerModel = new CustomerModel()
            {
                Id = getrecord.Id,
                Name = getrecord.Name,
                MobileNo = getrecord.MobileNo,
                Email = getrecord.Email,
                Password = getrecord.Password,
                UpdatedOn = getrecord.UpdatedOn,
                IsActive = getrecord.IsActive,
                IsDeleted = getrecord.IsDeleted,
                CityId = getrecord.CityId,
                StateId = getrecord.StateId
            };
            return customerModel;
        }

        public Customer PrepareCustomerModelForEditPost(CustomerModel model)
        {
            var customer = _customerService.GetCustomerById(model.Id);
            customer.Name = model.Name;
            customer.MobileNo = model.MobileNo;
            customer.Email = model.Email;
            customer.Password = model.Password;
            customer.IsDeleted = model.IsDeleted;
            customer.IsActive = model.IsActive;
            customer.CityId = model.CityId;
            customer.StateId = model.StateId;
            _customerService.UpdateCustomer(customer);

            return customer;
        }
    }
}