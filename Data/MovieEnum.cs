using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class MovieEnum
    {

        public enum Genres
        {
            Drama = 10,
            Action = 20,
            Comedy = 30,
            Adventure = 40,
            Thriller = 50,
            Period = 60,
            Romantic = 70,
            Animation = 80,
            Biography = 90,
            Family = 100,
            Fantasy = 110,
            Historical = 120,
            Horror = 130,
            Political = 140,
            Sci_Fi = 150
        };

        public enum Formate
        {
            Two_D = 10,
            Three_D = 20,
            Four_DX_Three_D = 30,
            Four_DX = 40,
            MX_Four_D_Three_D = 50,
            MX_Four_D = 60,
            Two_D_Screen_X = 70,
            Three_D_Screen_X = 80,
            IMAX_Two_D = 90,
            IMAX_Three_D = 100,
        }
    }
}
