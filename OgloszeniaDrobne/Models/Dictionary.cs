using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class Dictionary
    {

        public int DictionaryID { get; set; }

        public String Pole { get; set; }

        public int AttributeID { get; set; }

        public virtual Attribute Attribute { get; set; }

    }
}