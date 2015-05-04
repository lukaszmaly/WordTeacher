using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using English.Models;

namespace English.ViewModels
{
    public class WordForUser
    {
        virtual public GameUser GameUser { get; set; }
        virtual public ICollection<Entry> WordCollection { get; set; }
    }
}