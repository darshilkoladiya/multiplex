using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Data.Models
{
    public class ShowModel
    {
        public ShowModel()
        {
            MovieList = new List<SelectListItem>();
            FormateList = new List<SelectListItem>();
            LanguageList = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public DateTime Time { get; set; }
        public int MultiplexId { get; set; }
        [DisplayName("Multiplex Name")]
        public string MultiplexName { get; set; }
        public int MovieId { get; set; }
        [DisplayName("Movie Name")]
        public string MovieName { get; set; }
        public int FormateId { get; set; }
        [DisplayName("Formate Type")]
        public string FormateName { get; set; }
        public int ScreenId { get; set; }
        [DisplayName("Screen Name")]
        public string ScreenName { get; set; }
        public int LanguageId { get; set; }
        [DisplayName("Language")]
        public string LanguageName { get; set; }

        [NotMapped]
        public List<SelectListItem> MovieList { get; set; }
        [NotMapped]
        public List<SelectListItem> FormateList { get; set; }
        [NotMapped]
        public List<SelectListItem> LanguageList { get; set; }
       
    }
}
