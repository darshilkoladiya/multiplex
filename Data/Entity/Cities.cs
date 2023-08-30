
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
   public class Cities : BaseEntity
    {
        
        public int StateId { get; set; }
        public string Name { get; set; }
    }
}
