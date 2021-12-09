using CafeArts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeArts.ViewModels
{
    public class ProductCategories
    {
        public IEnumerable<Category> Categories { get; set; }

        public ProductModels Products { get; set; }

     
    }
}