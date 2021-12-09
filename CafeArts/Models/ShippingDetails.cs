using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class ShippingDetails
    {
        public int? ShippingDetailsID { get; set; }
        public string MemberID { get; set; }
        [ForeignKey("MemberID")]
        public ApplicationUser ApplicationUsers { get; set; }

        [Required(ErrorMessage = "First name field cannot be empty!")]
        [Display(Name ="First name")]
        [MaxLength(20)]
        public string FName { get; set; }

        [Required(ErrorMessage = "Last name field cannot be empty!")]
        [Display(Name = "Last name")]
        [MaxLength(20)]
        public string LName { get; set; }

        [Required(ErrorMessage ="Address field cannot be empty!")]
        [StringLength(300,ErrorMessage ="Please enter a valid address",MinimumLength =10)]
        
        public string Address { get; set; }

        [Required(ErrorMessage = "City field cannot be empty!")]
        [StringLength(20, ErrorMessage = "Please enter a valid city",MinimumLength =3)]
        public string City { get; set; }


        [Required(ErrorMessage = "State field cannot be empty!")]
        [Display(Name = "State")]
        public virtual int StateID { get; set; }

        [ForeignKey("StateID")]
        public virtual States statemodel { get; set; }

        [Required(ErrorMessage = "Zip code field cannot be empty!")]
        [MaxLength(6, ErrorMessage = "Please enter a valid zip code")]
        [MinLength(6, ErrorMessage = "Please enter a valid zip code")]
        public string ZipCode { get; set; }
        public float? Amount { get; set; }

        
        [Required]
        [Phone]
        [Display(Name ="Contact number")]
        [StringLength(10, ErrorMessage = "Please enter valid phone number"), MinLength(10, ErrorMessage ="Invalid Phone number")]
        
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please select a valid shipping type")]
        [Display(Name ="Shipping type")]
        public string ShippingType { get; set; }

    }
}