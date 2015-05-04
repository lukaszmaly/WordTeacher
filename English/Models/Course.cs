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
        [Required]
        [Display(Name="Course Name")]
        public string CourseName { get; set; }

        public bool Visible { get; set; }

        public virtual ICollection<Entry> Entries { get; set; }

    }
}