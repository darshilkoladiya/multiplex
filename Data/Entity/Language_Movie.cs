using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("Language_Movie_Mapping")]
    public class Language_Movie : BaseIdEntity
    {
        public int MovieId { get; set; }
        public int LanguageId { get; set; }
    }
}
