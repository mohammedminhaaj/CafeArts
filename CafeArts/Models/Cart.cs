using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class Cart
    {
        public int CartID { get; set; }

        public int? Quantity { get; set; } = 1;

        public ProductModels Product { get; set; }  

        public int? ProductID { get; set; }

        public string MemberID { get; set; }
        [ForeignKey("MemberID")]
        public ApplicationUser ApplicationUsers { get; set; }
        public bool IsActive { get; set; }
        public int? OrderID { get; set; }
        [ForeignKey("OrderID")]
        public Order Orders { get; set; }

    }
}