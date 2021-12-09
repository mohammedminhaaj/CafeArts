using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class ProductModels
    {
        public  int Id { get; set; }

        [Required]
        public  string ProductName { get; set; }

        public Category Category { get; set; }

        [Required]
        [Display(Name ="Product Category")]
        public int CategoryID { get; set; }


        [Required]
        public bool IsActive { get; set; }

        public string RibbonStatus { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string ProductShortDescription { get; set; }

        [Required]
        [MaxLength(500)]
        public string ProductDescription { get; set; }

        //[Required]
        [Display(Name ="Product Image 1 (Required)")]
        public byte[] ProductImage1 { get; set; }

        //[Required]
        [Display(Name = "Product Image 2 (Optional)")]
        public byte[] ProductImage2 { get; set; }

        //[Required]
        [Display(Name = "Product Image 3 (Optional)")]
        public byte[] ProductImage3 { get; set; }

        //[Required]
        [Display(Name = "Product Image 4 (Optional)")]
        public byte[] ProductImage4 { get; set; }

        [Required]
        public bool IsFeatured { get; set; }

        [Required]
        [Range(1,10)]
        public int ProductQuantity { get; set; }

        [Required]
        public float ProductPrice { get; set; }


    }
}