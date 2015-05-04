using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace English.Models
{
    public class WordUsage
    {
        public int WordUsageId { get; set; }
        public int EntryId { get; set; }
        public string Usage { get; set; }

        public WordUsage(string name)
        {
            this.Usage = name;
        }

        public WordUsage()
        {
            
        }
        [Newtonsoft.Json.JsonIgnore]
        [ScriptIgnore]
        public virtual Entry Entry { get; set; }

    }
}
