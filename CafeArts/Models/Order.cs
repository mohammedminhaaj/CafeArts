using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class Order
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? OrderID { get; set; }
        public string RazorPayKey { get; set; }
        public string MemberID { get; set; }

        [ForeignKey("MemberID")]
        public ApplicationUser ApplicationUsers { get; set; }
        public string TransactionID { get; set; }
        public string RPUniquePaymentID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerAddress { get; set; }
        public float TotalAmount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string OrderType { get; set; }
        public string OrderStatus { get; set; }

        public string WayBill { get; set; }

        [Display(Name ="Is this a customized order?")]
        public bool IsCustomized { get; set; }

    }
}