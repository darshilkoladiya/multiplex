using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Language_Movie_Model
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}
