using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }

        public string Reason { get; set; }

        [Required(ErrorMessage ="Please enter a valid name")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        [Display(Name = "Email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a valid contact")]
        [Display(Name = "Contact")]
        [Phone]
        [StringLength(10, ErrorMessage = "Please enter valid phone number"), MinLength(10, ErrorMessage = "Invalid Phone number")]

        public string Contact { get; set; }

        [Required(ErrorMessage = "Please enter a valid message")]
        [Display(Name = "Message")]

        
        public string Message { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}