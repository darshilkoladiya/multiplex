using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class CityModel
    {
        public int Id { get; set; }
        [DisplayName("State Name")]
        public int StateId { get; set; }
        public string Name { get; set; }
        [DisplayName("State Name")]
        public string StateName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
