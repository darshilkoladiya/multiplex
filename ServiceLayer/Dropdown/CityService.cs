using Data.Context;
using Data.Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
    public class CityService : ICityService
    {
        private readonly MultiplexContext db = new MultiplexContext();
        private IMultiRepository<Cities> cityRepository;

        public CityService(IMultiRepository<Cities> cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        public IEnumerable<Cities> GetAllCities()
        {
            return cityRepository.GetAll();
        }

        public Cities GetCitiesById(int id)
        {
            return cityRepository.Get(id);
        }

        public void InsertCities(Cities city)
        {
            cityRepository.Insert(city);
        }
        public void UpdateCities(Cities city)
        {
            cityRepository.Update(city);
        }

        public void DeleteCities(int id)
        {

            Cities city = GetCitiesById(id);
            city.IsDeleted = true;
            UpdateCities(city);
            //multiplexRepository.Remove(multiplex);
            //multiplexRepository.SaveChanges();
        }

        //public IEnumerable<Cities> GetAllCity()
        //{
        //    var city = db.cities.ToList();
        //    return city;
        //}


        public IEnumerable<Cities> GetStateByCity(int id)
        {
            var cityId = db.cities.Where(z => z.StateId == id).ToList();
            return cityId;
        }

        //public Cities GetList(int id)
        //{
        //    var cityList = db.cities.Where(x => x.Id ==id).FirstOrDefault();
        //    return cityList;
        //}
    }
}
