using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OgloszeniaDrobne.Models
{
    public class Add
    {

        public int AddID { get; set; }

        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        public virtual User User { get; set; }
        
        [Required(ErrorMessage = "Pole jest wymagane!")]
        [Display(Name = "Tytuł:")]
        public String Title { get; set; }
        
        
        [Required(ErrorMessage = "Pole jest wymagane!")]
        [Display(Name = "Treść Ogłoszenia:")]
        [DataType(DataType.MultilineText)]
        public String Content { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane!")]
        [Phone]
        [RegularExpression("[0-9]{9}", ErrorMessage = "Nie prawidłowy wzór")]
        [Display(Name ="Telefon:")]
        public String TelephoneNumber { get; set; }
        
        [Required(ErrorMessage = "Pole jest wymagane!")]
        [RegularExpression("[1-9]*[0-9]+([,]{1}[0-9]{1}[0-9]{1}){0,1}", ErrorMessage = "Wartość nie może być uznana jako cena. Wzór (23) (23,23)")]
        [Display(Name = "Cena:")]
        public Double Price { get; set; }

        [ScaffoldColumn(false)]
        public DateTime Data { get; set; }

        [ScaffoldColumn(false)]
        public int LiczbaOdsłon { get; set; }


        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<AddAtrribute> ListaDodatkowychAtr { get; set; }


    }
}