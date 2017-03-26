using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Pole imię jest wymagane!")]
        [Display(Name = "Imię:")]
        public String Imie { get; set; }

        [Required(ErrorMessage = "Pole nazwisko jest wymagane!")]
        [Display(Name = "Nazwisko:")]
        public String Nazwisko { get; set; }


        [ScaffoldColumn(false)]
        public String Rola { get; set; }

        [Required(ErrorMessage = "Pole e-mail jest wymagane!")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail:")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Pole login jest wymagane!")]
        [Display(Name = "Login:")]
        public String Login { get; set; }

        [Required(ErrorMessage = "Pole hasło jest wymagane!")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło:")]
        public String Haslo { get; set; }


        public int? IloscOgloszenStrona { get; set; }

    }


    
}