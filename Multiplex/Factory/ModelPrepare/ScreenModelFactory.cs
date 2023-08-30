using Data;
using Data.Entity;
using Data.Models;
using ServiceLayer;
using ServiceLayer.Dropdown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Multiplex.Factory.ModelPrepare
{
    public class ScreenModelFactory : IScreenModelFactory
    {

        private readonly IShowPriceService _showPriceService;
        private readonly IFormateService _formateService;
        private readonly IMovie_Multiplex_Serviece _movieMultiplexServiece;
        private readonly IMultiplexService _multiplexService;
        private readonly IScreenService _screenService;
        private readonly ISeatTypeService _seatTypeService;
        private readonly IMovieService _movieService;
        private readonly IShowService _showService;
        private readonly ILanguageService _languageService;
        private readonly IMovieLanguageService _movieLanguageService;


        public ScreenModelFactory(IShowPriceService showPriceService, IMovieLanguageService movieLanguageService, ILanguageService languageService, IFormateService formateService, IShowService showService, IMovieService movieService, IMovie_Multiplex_Serviece movieMultiplexServiece, IScreenService screenService, ISeatTypeService seatTypeService, IMultiplexService multiplexService)
        {
            _showPriceService = showPriceService;
            _movieLanguageService = movieLanguageService;
            _languageService = languageService;
            _formateService = formateService;
            _showService = showService;
            _movieService = movieService;
            _movieMultiplexServiece = movieMultiplexServiece;
            _multiplexService = multiplexService;
            _seatTypeService = seatTypeService;
            _screenService = screenService;
        }

        #region Screen

        public ScreenListModel PrepareScreenModelForList(int multiplexId)
        {
            var screen = _screenService.GetScreenByMultiplexId(multiplexId);
            var multiplex = _multiplexService.GetMultiplexById(multiplexId);
            var screenListModel = new ScreenListModel();

            var screenList = new List<ScreenModel>();

            foreach (var item in screen)
            {
                var model = new ScreenModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    MultiplexId = item.MultiplexId,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted
                };
                if (model.IsDeleted)
                {
                    continue;
                }
                screenList.Add(model);
            }

            screenListModel.screenModelsList = screenList;
            if (screen != null)
            {
                var firstReco = screen.FirstOrDefault();

                if (firstReco != null)
                {
                    screenListModel.Id = firstReco.MultiplexId;
                }
                else
                {
                    screenListModel.Id = multiplexId;
                }
                screenListModel.Name = multiplex.Name;
            }
            return screenListModel;

        }
        public ScreenModel PrepareScreenModelForCreateScreen(int id)
        {
            var multiplex = _multiplexService.GetMultiplexById(id);
            var screenModel = new ScreenModel();

            if (multiplex != null)
            {
                screenModel.MultiplexId = multiplex.Id;
                screenModel.MultiplexName = multiplex.Name;
            }
            return screenModel;
        }

        public ScreenModel PrepareScreenModelForEditScreen(int id)
        {
            var screenModel = new ScreenModel();
            var screen = _screenService.GetScreenById(id);
            var multiplex = _multiplexService.GetMultiplexById(screen.MultiplexId);

            if (screen != null)
            {
                screenModel.Name = screen.Name;
                screenModel.MultiplexId = screen.MultiplexId;
                screenModel.MultiplexName = multiplex.Name;
                screenModel.UpdatedOn = multiplex.UpdatedOn;
                screenModel.IsActive = multiplex.IsActive;
                screenModel.IsDeleted = multiplex.IsDeleted;
            }
            return screenModel;
        }

        public Screen PrepareScreenForEditPost(ScreenModel model)
        {
            var screen = _screenService.GetScreenById(model.Id);
            screen.Id = model.Id;
            screen.Name = model.Name;
            screen.MultiplexId = model.MultiplexId;
            screen.IsDeleted = model.IsDeleted;
            screen.IsActive = model.IsActive;
            _screenService.UpdateScreen(screen);
            return screen;
        }


        #endregion

        #region SeatType
        public ScreenListModel PrepareScreenListModelForList(int ScreenId)
        {
            var seatTypes = _seatTypeService.GetSeatByScreenId(ScreenId);
            var screen = _screenService.GetScreenById(ScreenId);
            var multiplex = _multiplexService.GetMultiplexById(screen.MultiplexId);
            var screenListModel = new ScreenListModel();
            var list = new List<SeatTypeModel>();

            foreach (var item in seatTypes)
            {
                var model = new SeatTypeModel()
                {
                    Id = item.Id,
                    ScreenId = item.ScreenId,
                    ScreenName = screen.Name,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted,
                    Seat = item.Seat,
                    Name = item.Name
                };
                if (model.IsDeleted)
                {
                    continue;
                }
                list.Add(model);
            }
            screenListModel.seatTypeModelsList = list;
            if (screen != null)
            {
                var firstReco = screen;

                if (firstReco != null)
                {
                    screenListModel.Id = firstReco.Id;
                    screenListModel.MultiplexId = screen.MultiplexId;

                }
                screenListModel.Name = multiplex.Name;
                screenListModel.ScreenName = screen.Name;
            }
            return screenListModel;
        }

        public SeatTypeModel PrepareSeatTypeModelForCreateSeatType(int ScreenId)
        {
            var screen = _screenService.GetScreenById(ScreenId);
            var seatTypeModel = new SeatTypeModel();
            if (screen != null)
            {
                seatTypeModel.ScreenId = screen.Id;
                seatTypeModel.ScreenName = screen.Name;
            }
            return seatTypeModel;
        }

        public SeatTypeModel PrepareSeatTypeModelForEditEditSeatType(int id)
        {
            var seatTypeModel = new SeatTypeModel();
            var seatType = _seatTypeService.GetSeatTypeById(id);
            var screen = _screenService.GetScreenById(seatType.ScreenId);

            if (seatType != null)
            {
                seatTypeModel.Id = seatType.Id;
                seatTypeModel.Name = seatType.Name;
                seatTypeModel.ScreenId = seatType.ScreenId;
                seatTypeModel.ScreenName = screen.Name;
                seatTypeModel.Seat = seatType.Seat;
            }
            return seatTypeModel;

        }

        public SeatType PrepareSeatTypeForEditPost(SeatTypeModel model)
        {
            var screen = _seatTypeService.GetSeatTypeById(model.Id);
            screen.Id = model.Id;
            screen.Name = model.Name;
            screen.ScreenId = model.ScreenId;
            screen.Seat = model.Seat;
            _seatTypeService.UpdateSeatType(screen);
            return screen;
        }


        #endregion

        #region Multiplex Base Movie
        public ScreenListModel PrepareMovieMultiplexModelForList(int multiplexId)
        {
            var movie = _movieMultiplexServiece.GetMovieByMultiplexId(multiplexId);
            var multiplex = _multiplexService.GetMultiplexById(multiplexId);
            var screenListModel = new ScreenListModel();
            var movieMultiplexList = new List<Movie_Multiplex_Model>();

            foreach (var item in movie)
            {
                var moviename = _movieService.GetMovieById(item.MovieId);
                var model = new Movie_Multiplex_Model()
                {
                    Id = item.Id,
                    MovieId = item.MovieId,
                    MovieName = moviename.Name,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted
                };
                if (model.IsDeleted)
                {
                    continue;
                }
                movieMultiplexList.Add(model);
            }

            screenListModel.movieMultiplexModelsList = movieMultiplexList;
            if (movie != null)
            {
                var firstReco = movie.FirstOrDefault();

                if (firstReco != null)
                {
                    screenListModel.Id = firstReco.MultiplexId;
                }
                else
                {
                    screenListModel.Id = multiplexId;
                }
                screenListModel.Name = multiplex.Name;
            }
            return screenListModel;
        }

        public Movie_Multiplex_Model PrepareMovieMultiplexModelForAddMovie(int id)
        {
            var multiplex = _multiplexService.GetMultiplexById(id);
            var movie_Multiplex_Model = new Movie_Multiplex_Model();

            if (multiplex != null)
            {
                movie_Multiplex_Model.MultiplexId = multiplex.Id;
                movie_Multiplex_Model.MultiplexName = multiplex.Name;
            }


            return movie_Multiplex_Model;
        }

        public Movie_Multiplex_Model PrepareMovieMultiplexModelForEditMovieMultiplex(int id)
        {
            var model = new Movie_Multiplex_Model();
            var movie = _movieMultiplexServiece.GetMovieMultiplexById(id);
            var multiplex = _multiplexService.GetMultiplexById(movie.MultiplexId);

            if (movie != null)
            {
                model.MovieId = movie.MovieId;
                model.MultiplexId = movie.MultiplexId;
                model.MultiplexName = multiplex.Name;
            }
            return model;
        }

        public Movie_Multiplex PrepareMovieMultiplexForEditPost(Movie_Multiplex_Model model)
        {

            var movie = _movieMultiplexServiece.GetMovieMultiplexById(model.Id);
            movie.Id = model.Id;
            movie.MovieId = model.MovieId;
            movie.MultiplexId = model.MultiplexId;
            _movieMultiplexServiece.UpdateMovieMultiplex(movie);
            return movie;
        }
        #endregion

        #region Show
        public ScreenListModel PrepareShowModelForList(int multiplexId)
        {
            var show = _showService.GetMultiplexIdByShow(multiplexId);
            var multiplex = _multiplexService.GetMultiplexById(multiplexId);
            var screenListModel = new ScreenListModel();
            var showList = new List<ShowModel>();
            var EnumData = from MovieEnum.Formate e in Enum.GetValues(typeof(MovieEnum.Formate))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };
            foreach (var item in show)
            {
                var screen = _screenService.GetScreenById(item.ScreenId);
                var formate = _formateService.GetFormateMovieById(item.FormateId);
                var formateEnum = EnumData.Where(s => s.Id == formate.FormateId).FirstOrDefault();
                var movie = _movieService.GetMovieById(item.MovieId);
                var lan = _movieLanguageService.GetLanguageMovieById(item.LanguageId);
                var language = _languageService.GetLanguageById(lan.LanguageId);

                var model = new ShowModel()
                {
                    Id = item.Id,
                    Time = item.Time,
                    MovieName = movie.Name,
                    ScreenName = screen.Name,
                    FormateName = formateEnum.Name,
                    LanguageName = language.Name,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted
                };
                if (model.IsDeleted)
                {
                    continue;
                }
                showList.Add(model);
            }

            screenListModel.showModelList = showList;
            if (show != null)
            {
                var firstReco = show.FirstOrDefault();

                if (firstReco != null)
                {
                    screenListModel.Id = firstReco.MultiplexId;
                }
                else
                {
                    screenListModel.Id = multiplexId;
                }
                screenListModel.Name = multiplex.Name;
            }
            return screenListModel;
        }

        public ShowModel PrepareShowModelForCreateShow(int id)
        {
            var multiplex = _multiplexService.GetMultiplexById(id);
            var showModel = new ShowModel();

            if (multiplex != null)
            {
                showModel.MultiplexId = multiplex.Id;
                showModel.MultiplexName = multiplex.Name;
            }
            return showModel;

        }

        public ShowModel PrepareShowModelForEditShow(int id)
        {
            var showModel = new ShowModel();
            var show = _showService.GetShowById(id);
            var screen = _screenService.GetScreenById(show.ScreenId);
            var multiplex = _multiplexService.GetMultiplexById(show.MultiplexId);
            var movie = _movieService.GetMovieById(show.MovieId);
            var lan = _movieLanguageService.GetLanguageMovieById(show.LanguageId);
            var language = _languageService.GetLanguageById(lan.LanguageId);
            var EnumData = from MovieEnum.Formate e in Enum.GetValues(typeof(MovieEnum.Formate))
                           select new
                           {
                               Id = (int)e,
                               Name = e.ToString()
                           };

            var formate = _formateService.GetFormateMovieById(show.FormateId);
            var formateEnum = EnumData.Where(s => s.Id == formate.FormateId).FirstOrDefault();
            if (show != null)
            {
                showModel.Id = show.Id;
                showModel.Time = show.Time;
                showModel.MultiplexId = show.MultiplexId;
                showModel.ScreenId = show.ScreenId;
                showModel.ScreenName = screen.Name;
                showModel.MovieId = show.MovieId;
                showModel.LanguageId = show.LanguageId;
                showModel.FormateId = show.FormateId;
                showModel.MultiplexName = multiplex.Name;
                showModel.MovieName = movie.Name;
                showModel.LanguageName = language.Name;
                showModel.FormateName = formateEnum.Name;
            }
            return showModel;
        }

        public Show PrepareShowForEditPost(ShowModel model)
        {
            var show = _showService.GetShowById(model.Id);
            show.Id = model.Id;
            show.Time = model.Time;
            show.MultiplexId = model.MultiplexId;
            show.ScreenId = model.ScreenId;
            show.MovieId = model.MovieId;
            show.LanguageId = model.LanguageId;
            show.FormateId = model.FormateId;
            show.IsActive = model.IsActive;
            show.IsDeleted = model.IsDeleted;
            _showService.UpdateShow(show);
            return show;
        }

        #endregion

        public ScreenListModel PrepareShowPriceModelForList(int showId)
        {
            var show = _showService.GetShowById(showId);
            var screen = _screenService.GetScreenById(show.ScreenId);
            var multiplex = _multiplexService.GetMultiplexById(show.MultiplexId);
            var seatType = _seatTypeService.GetSeatByScreenId(screen.Id).Where(x => x.IsDeleted == false);
            var screenListModel = new ScreenListModel();
            var showPriceList = new List<ShowPriceModel>();

            foreach (var item in seatType)
            {
                decimal price = 0;
                var prices = _showPriceService.GetAllShowPrice().Where(x => x.SeatTypeId == item.Id && x.IsDeleted == false).ToList();
                if (prices.Count != 0)
                    price = prices.FirstOrDefault().Price;

                var model = new ShowPriceModel()
                {
                    Id = item.Id,
                    SeatType = item.Name,
                    ShowId = showId,
                    IsActive = item.IsActive,
                    IsDeleted = item.IsDeleted
                };
                if (price >= 0)
                    model.Price = price;

                showPriceList.Add(model);
            }
            screenListModel.showPriceModelList = showPriceList;
            if (show != null)
            {
                var firstReco = show;

                if (firstReco != null)
                {
                    screenListModel.Id = firstReco.Id;
                }
                else
                {
                    screenListModel.Id = showId;
                }
                screenListModel.ShowTime = show.Time;
                screenListModel.MultiplexId = multiplex.Id;
                screenListModel.MultiplexName = multiplex.Name;
            }
            return screenListModel;
        }

        public ShowPriceModel PrepareShowPriceModelForSetPrice(int seatId, int showId)
        {

            var showpricelist = _showPriceService.GetAllShowPrice().Where(x => x.SeatTypeId == seatId).FirstOrDefault();
            if (showpricelist == null)
            {
                var showprice = new ShowPrice();
                showprice.SeatTypeId = seatId;
                showprice.ShowId = showId;
                _showPriceService.InsertShowPrice(showprice);
            }

            var showPrice = _showPriceService.GetAllShowPrice().Where(x => x.SeatTypeId == seatId && x.ShowId == showId).FirstOrDefault();
            var show = _showService.GetShowById(showId);
            var showPriceModel = new ShowPriceModel();
            var seat = _seatTypeService.GetSeatTypeById(seatId);
            if (seat != null)
            {
                showPriceModel.Id = showPrice.Id;
                showPriceModel.ShowId = show.Id;
                showPriceModel.ShowTime = show.Time;
                showPriceModel.SeatTypeId = seat.Id;
                showPriceModel.SeatType = seat.Name;
                showPriceModel.Price = showPrice.Price;
            }
            return showPriceModel;
        }


    }
}