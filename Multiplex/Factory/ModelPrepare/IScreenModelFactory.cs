using Data.Entity;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Multiplex.Factory.ModelPrepare
{
    public interface IScreenModelFactory
    {
        #region Screen
        ScreenListModel PrepareScreenModelForList(int multiplexId);
        ScreenModel PrepareScreenModelForCreateScreen(int id);
        ScreenModel PrepareScreenModelForEditScreen(int id);
        Screen PrepareScreenForEditPost(ScreenModel model);
        #endregion

        #region SeatType
        ScreenListModel PrepareScreenListModelForList(int ScreenId);
        SeatTypeModel PrepareSeatTypeModelForCreateSeatType(int ScreenId);
        SeatTypeModel PrepareSeatTypeModelForEditEditSeatType(int id);
        SeatType PrepareSeatTypeForEditPost(SeatTypeModel model);
        #endregion

        #region Multiplex Base Movie
        ScreenListModel PrepareMovieMultiplexModelForList(int multiplexId);
        Movie_Multiplex_Model PrepareMovieMultiplexModelForAddMovie(int id);
        Movie_Multiplex_Model PrepareMovieMultiplexModelForEditMovieMultiplex(int id);
        Movie_Multiplex PrepareMovieMultiplexForEditPost(Movie_Multiplex_Model model);
        #endregion

        #region Show
        ScreenListModel PrepareShowModelForList(int multiplexId);
        ShowModel PrepareShowModelForCreateShow(int id);
        ShowModel PrepareShowModelForEditShow(int id);
        Show PrepareShowForEditPost(ShowModel model);
        #endregion

        ScreenListModel PrepareShowPriceModelForList(int showId);
        ShowPriceModel PrepareShowPriceModelForSetPrice(int seatId, int showId);
    }
}