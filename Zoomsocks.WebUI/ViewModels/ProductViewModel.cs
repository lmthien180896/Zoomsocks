using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Zoomsocks.WebUI.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public SelectListItem[] Categories { get; set; }

        public ProductCategoryViewModel ProductCategory { get; set; }
    }
}