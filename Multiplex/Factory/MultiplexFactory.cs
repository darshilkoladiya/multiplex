using Data.Entity;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Factory
{
    public class MultiplexFactory : IMultiplexFactory
    {
        public Multiplexes PrepareMultiplexModelToMultiplex(MultiplexModel modal)
        {
            var multiplex = new Multiplexes()
            {
                Id = modal.Id,
                Name = modal.Name,
                Address = modal.Address,
                IsActive = modal.IsActive,
                IsDeleted = modal.IsDeleted,
                CityId = modal.CityId,
                StateId = modal.StateId
            };
            return multiplex;
        }

        public Movie PrepareMovieModelToMovie(MovieModel modal)
        {
            var movie = new Movie()
            {
                Id = modal.Id,
                Name = modal.Name,
                ReleaseDate = modal.ReleaseDate,
                Image = modal.Image,
                IsActive = modal.IsActive,
                IsDeleted = modal.IsDeleted
            };
            return movie;
        }


        public Customer PrepareCustomerModelToCustomer(CustomerModel modal)
        {

            var customer = new Customer()
            {
                Id = modal.Id,
                Name = modal.Name,
                MobileNo = modal.MobileNo,
                Email = modal.Email,
                Password = modal.Password,
                IsActive = modal.IsActive,
                IsDeleted = modal.IsDeleted,
                CityId = modal.CityId,
                StateId = modal.StateId
            };
            return customer;
        }

        public Cities PrepareCitiesModelToCities(CityModel modal)
        {
            var city = new Cities()
            {
                Id = modal.Id,
                Name = modal.Name,
                StateId = modal.StateId,
                IsActive = modal.IsActive,
                IsDeleted = modal.IsDeleted,

            };
            return city;
        }

        public SeatType PrepareSeatTypeModelToSeatType(SeatTypeModel model)
        {
            var seatType = new SeatType()
            {
                Id = model.Id,
                Name = model.Name,
                ScreenId = model.ScreenId,
                IsDeleted = model.IsDeleted,
                IsActive = model.IsActive,
                Seat = model.Seat
            };
            return seatType;
        }

        public Movie_Multiplex PrepareMovieMultiplexModelToMovieMultiplex(Movie_Multiplex_Model modal)
        {
            var movie = new Movie_Multiplex()
            {
                Id = modal.Id,
                IsActive = modal.IsActive,
                IsDeleted = modal.IsDeleted,
                MovieId = modal.MovieId,
                MultiplexId = modal.MultiplexId
            };
            return movie;
        }


        public Screen PrepareScreenModelToScreen(ScreenModel modal)
        {
            var screen = new Screen()
            {
                Id = modal.Id,
                Name = modal.Name,
                MultiplexId = modal.MultiplexId,
                IsActive = modal.IsActive,
                IsDeleted = modal.IsDeleted
            };
            return screen;
        }

        public Show PrepareShowModelToShow(ShowModel model)
        {
            var show = new Show()
            {
                Id = model.Id,
                MultiplexId = model.MultiplexId,
                MovieId = model.MovieId,
                LanguageId = model.LanguageId,
                FormateId = model.FormateId,
                Time = model.Time,
                ScreenId = model.ScreenId,
                IsDeleted = model.IsDeleted,
                IsActive = model.IsActive
            };
            return show;
        }

        public ShowPrice PrepareShowPriceModelToShowPrice(ShowPriceModel model)
        {
            var showPrice = new ShowPrice()
            {
                Id = model.Id,
                SeatTypeId = model.SeatTypeId,
                ShowId = model.ShowId,
                Price = model.Price,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted
            };
            return showPrice;
        }
    }
}
