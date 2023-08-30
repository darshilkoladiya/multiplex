using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data.Models
{
    public class ScreenListModel
    {
        public ScreenListModel()
        {
            screenModelsList = new List<ScreenModel>();
            seatTypeModelsList = new List<SeatTypeModel>();
            movieMultiplexModelsList = new List<Movie_Multiplex_Model>();
            showModelList = new List<ShowModel>();
            showPriceModelList = new List<ShowPriceModel>();
        }

        public int Id { get; set; }
        public int MultiplexId { get; set; }
        [DisplayName("Screen Name")]
        public string Name { get; set; }
        [DisplayName("Screen Name")]
        public string ScreenName { get; set; }
        public int Seat { get; set; }
        [DisplayName("Multiplex Name")]
        public string MultiplexName { get; set; }
        [DisplayName("Movie Name")]
        public string MovieName { get; set; }
        public DateTime Time { get; set; }
        [DisplayName("Formate Name")]
        public string FormateName { get; set; }
        [DisplayName("Language Name")]
        public string LanguageName { get; set; }

        [DisplayName("Seat Type")]
        public string SeatType { get; set; }
        [DisplayName("Show Time")]
        public DateTime ShowTime { get; set; }
        public decimal Price { get; set; }

        public List<ScreenModel> screenModelsList { get; set; }
        public List<SeatTypeModel> seatTypeModelsList { get; set; }
        public List<Movie_Multiplex_Model> movieMultiplexModelsList { get; set; }
        public List<ShowModel> showModelList { get; set; }
        public List<ShowPriceModel> showPriceModelList { get; set; }


    }
}
