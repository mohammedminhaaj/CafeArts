using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class Reviews
    {
        [Key]
        public int ReviewId { get; set; }

        
        [StringLength(256)]
        [Display(Name ="Write something about this product")]
        public string ReviewData { get; set; }

        public bool IsValid { get; set; }

        public string Rating { get; set; }

        public int? ProdID { get; set; }

        public DateTime? ReviewCreatedDate { get; set; }

        
        public ProductModels prod { get; set; }

        [Display(Name ="The user should login first to post a comment!")]
        public string MemberID { get; set; }
        [ForeignKey("MemberID")]
        public ApplicationUser ApplicationUsers { get; set; }
    }
}