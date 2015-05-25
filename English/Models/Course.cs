using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace English.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public int Owner { get; set; }
        [Required]
        [Display(Name="Course Name")]
        public string CourseName { get; set; }
        public bool Visible { get; set; }
         [Display(Name = "Is this course paid?")]
        public bool Paid { get; set; }
         [Display(Name = "Course Cost" )]
        [Range(1,999,ErrorMessage = "[1-999]")]
        public int?  Cost { get; set; }
        public virtual ICollection<Entry> Entries { get; set; }
        public virtual ICollection<GameUser> Users { get; set; } 
    }
}