using Data.Entity;
using Data.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Factory.ModelPrepare
{
    public interface IMultiplexModelFactory
    {
        PagedList<MultiplexModel> PrepareMultiplexModelForIndex(string searchString, int? page, string searchState, string searchCity);
        MultiplexModel PrepareMultiplexModelForEdit(int multiplexId);
        Multiplexes PrepareMultiplexModelForEditPost(MultiplexModel model);

    }
}