using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeArts.Models
{
    public class SlideImage
    {
        [Key]
        public int SlideId { get; set; }
        public string SlideTitle { get; set; }

        public byte[] SlidePicture { get; set; }
    }
}