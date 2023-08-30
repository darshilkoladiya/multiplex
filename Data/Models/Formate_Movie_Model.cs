using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Formate_Movie_Model
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int FormateId { get; set; }
        public string FormateName { get; set; }
    }
}
