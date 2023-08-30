using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("Movie_Multiplex_Mapping")]
    public class Movie_Multiplex : BaseEntity
    {
        public int MultiplexId { get; set; }
        public int MovieId { get; set; }

        //public virtual Movie Movie { get; set; }

    }
}
