using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Dropdown
{
   public interface ICityService
    {
        IEnumerable<Cities> GetStateByCity(int id);
        IEnumerable<Cities> GetAllCities();
        Cities GetCitiesById(int id);
        void InsertCities(Cities city);
        void UpdateCities(Cities city);
        void DeleteCities(int id);
    }
}
