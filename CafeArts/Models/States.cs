using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class States
    {
        [Key]
        public int StateKey { get; set; }

        public string StateName { get; set; }
    }
}