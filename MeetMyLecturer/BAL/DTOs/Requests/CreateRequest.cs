using BAL.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DTOs.Requests
{
    public class CreateRequest
    {
        public int StudentId { get; set; }

        public int LecturerId { get; set; }

        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [ComparationDateValidation(ErrorMessage = "Date must be more than or equals today.")]
        public DateTime Date {  get; set; }

        [Required(ErrorMessage = "Start Time is required.")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Time is required.")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
