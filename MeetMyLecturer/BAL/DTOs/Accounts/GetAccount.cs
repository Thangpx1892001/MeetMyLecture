using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Accounts
{
    public class GetAccount
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Fullname { get; set; }

        public string Email { get; set; }

        public DateTime Dob { get; set; }

        public string Role { get; set; }

        public List<int> SubjectId{ get; set; }

        public string AccessToken { get; set; }

        public string? Status { get; set; }
    }
}
