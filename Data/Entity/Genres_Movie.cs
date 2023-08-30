using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    [Table("Genres_Movie_Mapping")]
    public class Genres_Movie : BaseIdEntity
    {
        public int MovieId { get; set; }
        public int GenresId { get; set; }
    }
}
