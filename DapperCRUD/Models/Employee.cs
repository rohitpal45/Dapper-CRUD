using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DapperCRUD.Models
{
    public class Employee
    {
        public int userid { get; set; }
        [Required(ErrorMessage = "Enter user name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Father name")]
        public string FathName { get; set; }
        [Required(ErrorMessage = "Enter Mobile number")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Enter email")]
        public string email { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string DOB { get; set; }
        [Required(ErrorMessage = "Profile Required")]
        public string UserProfile { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string RoleId { get; set; }
        public string Msg { get; set; }

    }
    public class UserLogin
    {
        public int userid { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }
        public string RoleId { get; set; }
    }
}