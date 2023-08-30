using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.Web.Mvc.CompareAttribute;

namespace Data.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string MobileNo { get; set; }


        [Required]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string Confirmpassword { get; set; }


        [Required]
        [DisplayName("City")]
        public int CityId { get; set; }


        [Required]
        [DisplayName("State")]
        public int StateId { get; set; }


        [DisplayName("City")]
        public string CityName { get; set; }


        [DisplayName("State")]
        public string StateName { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}