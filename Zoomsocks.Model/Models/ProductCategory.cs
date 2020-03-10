using SingLife.ULTracker.Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoomsocks.Model.Models
{
    [Table("product-category")]
    public class ProductCategory : Entity
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public int DisplayOrder { get; set; }

        public virtual IEnumerable<Product> Products { get; set; }
    }
}