using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("SeatType_Screen_Mapping")]
    public class SeatType : BaseEntity
    {
        public string Name { get; set; }
        public int ScreenId { get; set; }
        public int Seat { get; set; }
    }
}
