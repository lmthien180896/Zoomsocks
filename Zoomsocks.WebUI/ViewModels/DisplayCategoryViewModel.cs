using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoomsocks.WebUI.ViewModels
{
    public class DisplayCategoryViewModel
    {
        public ProductCategoryViewModel Category { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }
    }
}