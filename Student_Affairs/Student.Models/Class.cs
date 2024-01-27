using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Models
{
    public class Class
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [StringLength(255, ErrorMessage = "Class name cannot exceed 255 characters.")]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
