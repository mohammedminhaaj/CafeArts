using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class Customize
    {
        [Key]
        public int CustomizeID { get; set; }

        [Required(ErrorMessage ="Please select a category")]
        [Display(Name ="Category")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }

        public IEnumerable<Category> CategoriesForCustom { get; set; }

        [Display(Name = "Color 1")]
        [Required(ErrorMessage ="Please select atleast one color before submitting")]
        public string Color1 { get; set; }

        [Display(Name = "Color 2")]
        public string Color2 { get; set; }

        [Display(Name = "Color 3")]
        public string Color3 { get; set; }

        [Display(Name = "Color 4")]
        public string Color4 { get; set; }

        [Display(Name = "Full name")]
        [Required(ErrorMessage ="Enter a valid name")]
        public string FullName { get; set; }

        [Display(Name = "Contact number")]
        [Required(ErrorMessage = "Enter a valid contact number")]
        [Phone]
        [StringLength(10, ErrorMessage = "Please enter valid phone number"), MinLength(10, ErrorMessage = "Invalid Phone number")]

        public string ContactNumber { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Enter a valid email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Notes")]
        public string CustomizeDescription { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}