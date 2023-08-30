using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
   public class Login
    {
        [Required(ErrorMessage = "Please Enter UserName !")]
        public string Email { get; set; }

        public bool RememberMe { get; set; }


        [Required(ErrorMessage = "Please Enter Password !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
