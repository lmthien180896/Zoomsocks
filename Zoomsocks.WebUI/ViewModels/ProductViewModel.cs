using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zoomsocks.WebUI.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        [Display(Name ="More Images")]
        public string MoreImages { get; set; }

        public string[] MoreImagesList { get; set; }

        public SelectListItem[] Categories { get; set; }

        public Guid ProductCategoryId { get; set; }

        public string Category { get; set; }
    }
}