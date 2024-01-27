using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(255, ErrorMessage = "Student name cannot exceed 255 characters.")]
        public string Name { get; set; }
		[Required(ErrorMessage = "Student Gender is required.")]
		public char Gender { get; set; }

      //  [DataType(DataType.Date)]
        public string? BirthDate { get; set; }

        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters.")]
        public string? Address { get; set; }

		//[Required(ErrorMessage = "Student Email Address is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string? EmailAddress { get; set; }

		[Required(ErrorMessage = "Student Phone Number is required.")]
		public string PhoneNumber { get; set; }

		public string? Photo { get; set; }
     
		[Required(ErrorMessage = "Student Class is required.")]
		public int ClassID { get; set; }

        [ForeignKey("ClassID")]
        [ValidateNever]
        public Class Class { get; set; }
    }
}
