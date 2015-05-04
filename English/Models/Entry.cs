using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace English.Models
{
    public class Entry
    {
        public int EntryId { get; set; }
        [Display(Name="Słowo")]
        public string EnglishWord { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool Visible { get; set; }

        public virtual ICollection<WordUsage> Usages { get; set; }
    }
}