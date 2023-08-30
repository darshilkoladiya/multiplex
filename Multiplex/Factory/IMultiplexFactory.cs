using Data.Entity;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Factory
{
   public interface IMultiplexFactory
    {
        Multiplexes PrepareMultiplexModelToMultiplex(MultiplexModel modal);

        Movie PrepareMovieModelToMovie(MovieModel modal);

        Customer PrepareCustomerModelToCustomer(CustomerModel modal);

        Cities PrepareCitiesModelToCities(CityModel modal);


        SeatType PrepareSeatTypeModelToSeatType(SeatTypeModel modal);

        Screen PrepareScreenModelToScreen(ScreenModel modal);

        Movie_Multiplex PrepareMovieMultiplexModelToMovieMultiplex(Movie_Multiplex_Model modal);

        Show PrepareShowModelToShow(ShowModel model);
        ShowPrice PrepareShowPriceModelToShowPrice(ShowPriceModel model);
    }
}
