using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class CreateAccount
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        [EmailAddress(ErrorMessage = "Email does not have a valid extension")]
        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string Role { get; set; }

        public List<int> SubjectId { get; set; }
    }
}
