using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ShowPriceModel
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int ShowId { get; set; }
        [DisplayName("Show")]
        public DateTime ShowTime { get; set; }
        public int SeatTypeId { get; set; }
        [DisplayName("Seat")]
        public string SeatType { get; set; }
        public decimal Price { get; set; }
    }
}
