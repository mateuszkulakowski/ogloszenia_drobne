using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class Category
    {

        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Pole nazwa jest obowiązkowe!")]
        public String Nazwa { get; set; }

        public int? ParentCategoryID { get; set; }
        
        public virtual Category ParentCategory { get; set; }

        public virtual ICollection<Category> Children { get; set; }

        public virtual ICollection<Attribute> Attributies { get; set; }

    }
}