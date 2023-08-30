using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("Screen_Multiplex_Mapping")]
    public class Screen : BaseEntity
    {
        public string Name { get; set; }
        public int MultiplexId { get; set; }
    }
}
