using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Data.Models
{
    public class DropDownModel
    {
        public DropDownModel()
        {
            NameList = new List<SelectListItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<SelectListItem> NameList { get; set; }
    }
}
