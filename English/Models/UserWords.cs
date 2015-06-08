using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
namespace English.Models
{
    public class UserWords
    {
        public int UserWordsId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [ScriptIgnore]
        public virtual ICollection<GameUser> GameUsers { get; set; }
        public int Strike { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [ScriptIgnore]
        public virtual Entry entry { get; set; }
        public DateTime LastUsage { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [ScriptIgnore]
        public virtual  Course Course { get; set; }
        public bool IsTimeToLearn { get; set; }

        public bool ContainWord(string word)
        {
            foreach (var e in entry.Usages)
            {
                if (e.Usage == word)
                    return true;
            }
            return false;
        }

        public int GetDays(int step)
        {
            if (step == 0) return 0;
            if (step == 1) return 1;
            if (step == 2) return 6;
            return (int)(GetDays(step - 1) * 1.4);
        }

        public void LearnResult(bool success)
        {
            LastUsage = DateTime.Now;
            Strike = success ? Strike + 1 : 0;
        }

        public bool IsTime()
        {
            if (Strike == 0) return true;
            var nextData = LastUsage.AddDays(GetDays(Strike));
            var result= DateTime.Compare( DateTime.Now,nextData );
            return result>0;
        }
        public void Update()
        {
            this.IsTimeToLearn = IsTime();
        }
    }
}