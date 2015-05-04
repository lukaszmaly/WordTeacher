using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace English.Models
{
    public class EnglishContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EnglishContext() : base("name=EnglishContext")
        {
            Database.SetInitializer<EnglishContext>(new DropCreateDatabaseAlways<EnglishContext>());
        }

        public System.Data.Entity.DbSet<English.Models.Entry> Entries { get; set; }

        public System.Data.Entity.DbSet<English.Models.WordUsage> WordUsages { get; set; }
        public System.Data.Entity.DbSet<English.Models.GameUser> GameUsers { get; set; }

        public System.Data.Entity.DbSet<English.Models.Course> Courses { get; set; }
    }
}
