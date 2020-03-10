using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zoomsocks.WebUI.ViewModels
{
    public class ProductCategoryViewModel
    {
        public ProductCategoryViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        [Display(Name = "Category")]
        public string Name { get; set; }

        public string Alias { get; set; }

        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}