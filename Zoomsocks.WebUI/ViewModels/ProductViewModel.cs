using System;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Alias is required.")]
        public string Alias { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required.")]
        public string Image { get; set; }

        [Display(Name = "More Images")]
        public string MoreImages { get; set; }

        public string[] MoreImagesList { get; set; }

        public SelectListItem[] Categories { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        public Guid ProductCategoryId { get; set; }

        public string Category { get; set; }

        public bool IsCreateWizard { get; set; }

        public string CallBackMethod => IsCreateWizard ? "Create" : "Edit";
    }
}