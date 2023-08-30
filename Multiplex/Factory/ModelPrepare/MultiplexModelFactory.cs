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
    public class MultiplexModelFactory : IMultiplexModelFactory
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateSevice;
        private readonly IMultiplexService _multiplexService;

        public MultiplexModelFactory(IMultiplexService multiplexService, IStateService stateSevice, ICityService cityService)
        {
            _cityService = cityService;
            _stateSevice = stateSevice;
            _multiplexService = multiplexService;
        }

        #region Multiplex Model

        public PagedList<MultiplexModel> PrepareMultiplexModelForIndex(string searchString, int? page, string searchState, string searchCity)
        {
            var Customerlist = _multiplexService.GetMultiplex();
            var multiplex = new List<MultiplexModel>();
            foreach (var item in Customerlist)
            {
                var stateList = _stateSevice.GetStateById(item.StateId);
                var cityList = _cityService.GetCitiesById(item.CityId);
                var multiplexModel = new MultiplexModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    StateId = item.StateId,
                    CityId = item.CityId,
                    CityName = cityList.Name,
                    StateName = stateList.Name,
                    IsDeleted = item.IsDeleted,
                    IsActive = item.IsActive

                };
                if (multiplexModel.IsDeleted)
                {
                    continue;
                }
                multiplex.Add(multiplexModel);
            }
            var multiplexlist = from a in multiplex select a;

            if (!string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(searchState)&& string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            if ( string.IsNullOrEmpty(searchString)&& !string.IsNullOrEmpty(searchState) && string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.StateId.ToString().Contains(searchState));
            }

            if (string.IsNullOrEmpty(searchState) && string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.CityId.ToString().Contains(searchCity));
            }

            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchState) && string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.Name.ToUpper().Contains(searchString.ToUpper()) && z.StateId.ToString().Contains(searchState));
            }

            if (!string.IsNullOrEmpty(searchString) && string.IsNullOrEmpty(searchState) && !string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.Name.ToUpper().Contains(searchString.ToUpper()) && z.CityId.ToString().Contains(searchCity));
            }

            if (string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchState) && !string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.StateId.ToString().Contains(searchState) && z.CityId.ToString().Contains(searchCity));
            }
            
            if (!string.IsNullOrEmpty(searchString) && !string.IsNullOrEmpty(searchState) && !string.IsNullOrEmpty(searchCity))
            {
                multiplexlist = multiplex.Where(z => z.Name.ToUpper().Contains(searchString.ToUpper()) && z.StateId.ToString().Contains(searchState) && z.CityId.ToString().Contains(searchCity));
            }
            
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return (PagedList<MultiplexModel>)multiplexlist.ToPagedList(pageNumber, pageSize);
        }


        public MultiplexModel PrepareMultiplexModelForEdit(int multiplexId)
        {
            var getrecord = _multiplexService.GetMultiplexById(multiplexId);
            var multiplexModal = new MultiplexModel()
            {
                Id = getrecord.Id,
                Name = getrecord.Name,
                Address = getrecord.Address,
                UpdatedOn = getrecord.UpdatedOn,
                IsActive = getrecord.IsActive,
                IsDeleted = getrecord.IsDeleted,
                CityId = getrecord.CityId,
                StateId = getrecord.StateId
            };
            return multiplexModal;
        }

        public Multiplexes PrepareMultiplexModelForEditPost(MultiplexModel model)
        {
            var multiplex = _multiplexService.GetMultiplexById(model.Id);
            multiplex.Name = model.Name;
            multiplex.Address = model.Address;
            multiplex.IsActive = model.IsActive;
            multiplex.IsDeleted = model.IsDeleted;
            multiplex.UpdatedOn = System.DateTime.Now;
            multiplex.CityId = model.CityId;
            multiplex.StateId = model.StateId;
            _multiplexService.UpdateMultiplex(multiplex);

            return multiplex;
        }
        #endregion


    }
}