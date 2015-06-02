using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace English.Models
{
    public class GameUser
    {
        public int GameUserId { get; set; }
        public string UserName { get; set; }
        public int Points { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int PremiumPoints { get; set; }
        virtual public ICollection<Course> Courses { get; set; }
        public virtual ICollection<UserWords> UserWords { get; set; }

        public GameUser()
        {
            
        }

        public GameUser(string userName)
        {
            this.UserName = userName;
            this.Points = 0;
            this.Health = this.MaxHealth = 100;
            this.PremiumPoints = 100;
        }

    }
}