using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class BannedWord
    {
        public int BannedWordID { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane !")]
        [Display(Name = "Słowo:")]
        public String Word { get; set; }
    }
}