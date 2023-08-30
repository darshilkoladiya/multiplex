using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Show : BaseEntity
    {
        public DateTime Time { get; set; }
        public int MultiplexId { get; set; }
        public int MovieId { get; set; }
        public int FormateId { get; set; }
        public int ScreenId { get; set; }
        public int LanguageId { get; set; }

    }
}
