using SingLife.ULTracker.Model.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoomsocks.Model.Models
{
    [Table("product")]
    public class Product : Entity
    {
        public string Name { get; set; }

        public string Alias { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public Guid ProductCategoryId { get; set; }

        [ForeignKey("ProductCategoryId")]
        public virtual ProductCategory ProductCategory { get; set; }
    }
}