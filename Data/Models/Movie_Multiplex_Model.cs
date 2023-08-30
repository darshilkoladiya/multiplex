using Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data.Models
{
    public class Movie_Multiplex_Model
    {

      
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int MultiplexId { get; set; }
        [DisplayName("Multiplex Name")]
        public string MultiplexName { get; set; }
        public int MovieId { get; set; }
        [DisplayName("Movie Name")]
        public string MovieName { get; set; }



    }
}
