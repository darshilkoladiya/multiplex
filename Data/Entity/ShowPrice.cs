using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class ShowPrice : BaseEntity
    {
        public int ShowId { get; set; }
        public int SeatTypeId { get; set; }
        public decimal Price { get; set; }
    }
}
