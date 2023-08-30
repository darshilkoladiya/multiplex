using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
   public class SeatTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ScreenId { get; set; }
        [DisplayName("Screen Name")]
        public string ScreenName { get; set; }
        public int Seat { get; set; }


        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
