using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class Attribute
    {
        public int AttributeID { get; set; }

        public String Nazwa { get; set; }

        public String Typ { get; set; }

        public virtual ICollection<Dictionary> Dictionaries { get; set; }

        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }
    }
}