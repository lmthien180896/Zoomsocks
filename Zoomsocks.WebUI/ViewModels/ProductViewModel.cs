using System;
using System.ComponentModel.DataAnnotations;

namespace Zoomsocks.WebUI.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Product")]
        public string Name { get; set; }

        public string Alias { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string Image { get; set; }

        public Guid ProductCategoryId { get; set; }
    }
}