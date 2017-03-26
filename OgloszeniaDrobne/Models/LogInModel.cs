using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class LogInModel
    {
        [Required(ErrorMessage = "Pole Login jest wymagane!")]
        [Display(Name = "Login:")]
        public String Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Pole Hasło jest wymagane!")]
        [Display(Name = "Hasło:")]
        public String Password { get; set; }
    }
}