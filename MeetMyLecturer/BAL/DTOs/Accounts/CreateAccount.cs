using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class CreateAccount
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Fullname is required.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [FptEmailAddressValidation(ErrorMessage = "Email does not have a valid extension")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        //public List<int> SubjectId { get; set; }
    }
}
