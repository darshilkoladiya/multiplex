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
    public class StateCityModelFactory : IStateCityModelFactory
    {
        private readonly StateService _stateSevice;
        private readonly CityService _cityService;
        private readonly CustomerService _customerService;
        public StateCityModelFactory(CustomerService customerService, CityService cityService,
                              StateService stateSevice)
        {
            _stateSevice = stateSevice;
            _customerService = customerService;
            _cityService = cityService;
        }


        public States PrepareStateForEdit(int id)
        {
            var getState = _stateSevice.GetStateById(id);
            var state = new States();
            state.Id = getState.Id;
            state.Name = getState.Name;
            state.IsActive = getState.IsActive;
            state.IsDeleted = getState.IsDeleted;
            state.UpdatedOn = getState.UpdatedOn;
            return state;
        }

        public PagedList<States> PrepareStateForList(string searchString, string currentFilter, int? page)
        {
            var state = _stateSevice.GetAllState().Where(s => s.IsDeleted == false);

            var stateList = from a in state select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                stateList = state.Where(z => z.Name.Contains(searchString));
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
            return (PagedList<States>)stateList.ToPagedList(pageNumber, pageSize);
        }
        public States PrepareStateForEditPost(States model)
        {
            var state = _stateSevice.GetStateById(model.Id);
            state.Name = model.Name;
            state.UpdatedOn = System.DateTime.Now;
            state.IsActive = model.IsActive;
            state.IsDeleted = model.IsDeleted;
            _stateSevice.UpdateState(state);
            return state;
        }

        public PagedList<CityModel> PrepareCityModelForCityList(string searchString, string currentFilter, int? page)
        {
            var allCity = _cityService.GetAllCities();
            var city = new List<CityModel>();
            foreach (var item in allCity)
            {
                var stateName = _stateSevice.GetStateById(item.StateId);
                var cityList = new CityModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    StateId = item.StateId,
                    StateName = stateName.Name,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted
                };
                if (cityList.IsDeleted)
                {
                    continue;
                }
                city.Add(cityList);
            }

            var cityData = from a in city select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                cityData = city.Where(z => z.Name.Contains(searchString));
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
            return (PagedList<CityModel>)cityData.ToPagedList(pageNumber, pageSize);
        }

        public CityModel prepareCityModelForEditCity(int id)
        {
            var cityModel = new CityModel();
            var getRecord = _cityService.GetCitiesById(id);
            cityModel.Id = getRecord.Id;
            cityModel.Name = getRecord.Name;
            cityModel.StateId = getRecord.StateId;
            cityModel.IsActive = getRecord.IsActive;
            cityModel.IsDeleted = getRecord.IsDeleted;
            cityModel.UpdatedOn = getRecord.UpdatedOn;
            return cityModel;
        }

        public Cities PrepareCitiesForEditPostCity(CityModel model)
        {
            var city = _cityService.GetCitiesById(model.Id);
            city.Id = model.Id;
            city.Name = model.Name;
            city.StateId = model.StateId;
            city.IsActive = model.IsActive;
            city.IsDeleted = model.IsDeleted;
            _cityService.UpdateCities(city);
            return city;
        }

       
    }
}