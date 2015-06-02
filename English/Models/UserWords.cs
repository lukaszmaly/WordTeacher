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
        public DateTime LastUsage { get; set; }
        public Course Course { get; set; }

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
            if (step == 1) return 1;
            if (step == 2) return 6;
            return GetDays(step - 1) * 1.4;
        }

        public void LearnResult(bool success)
        {
            LastUsage = DateTime.Now;
            Strike = success ? Strike + 1 : 0;
        }

        public bool IsTimeToLearn()
        {
            if (Strike == 0) return true;
            return LastUsage.AddDays(GetDays(Strike)) >= DateTime.Now;
        }
    }
}