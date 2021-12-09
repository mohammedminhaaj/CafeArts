using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class Category
    {
        public int CategoryID { get; set; }

        [Display(Name ="Category Name")]
        [Required(ErrorMessage ="Category name cannot be empty!")]
        [StringLength(10,ErrorMessage ="Cannot be more than {1} characters and less than {2} characters!",MinimumLength =3)]
        public string CategoryName { get; set; }


    }
}