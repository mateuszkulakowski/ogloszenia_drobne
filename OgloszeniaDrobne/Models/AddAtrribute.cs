using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class AddAtrribute
    {
        [Key]
        public int AddAttributeID { get; set; }

        public int AddID { get; set; }

        public virtual Add Add { get; set; }

        public int AttributeID { get; set; }

        public virtual Attribute Attribute { get; set; }

        public String Value { get; set; }
    }
}