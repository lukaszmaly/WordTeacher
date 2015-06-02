using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace English.Models
{
    public class UserWords
    {
        public int UserWordsId { get; set; }
        public virtual ICollection<GameUser> GameUsers { get; set; }
        public int Strike { get; set; }
        public Entry entry { get; set; }
        public DateTime LastUseage { get; set; }
        public Course Course { get; set; }
    }
}