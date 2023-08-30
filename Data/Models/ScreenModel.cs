using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data.Models
{
   public class ScreenModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Screen Name")]
        public string Name { get; set; }
        public int MultiplexId { get; set; }
        [DisplayName("Multiplex Name")]
        public string MultiplexName { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
