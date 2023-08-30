using Data.Entity;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Factory.ModelPrepare
{
    public interface IStateCityModelFactory
    {
        PagedList<States> PrepareStateForList(string searchString, string currentFilter, int? page);
        States PrepareStateForEdit(int id);
        States PrepareStateForEditPost(States model);
        PagedList<CityModel> PrepareCityModelForCityList(string searchString, string currentFilter, int? page);
        CityModel prepareCityModelForEditCity(int id);
        Cities PrepareCitiesForEditPostCity(CityModel model);
    }
}
