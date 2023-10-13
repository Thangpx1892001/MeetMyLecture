using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.ValidationAttributes
{
    public class EmailAddressAttribute : ValidationAttribute
    {
        public EmailAddressAttribute() { }

        public override bool IsValid(object? value)
        {
            if (value is string emailAddress)
            {
                return emailAddress.EndsWith("@fpt.edu.vn");
            }
            return false;
        }
    }
}
