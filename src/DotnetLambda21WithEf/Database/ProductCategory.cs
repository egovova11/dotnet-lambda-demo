using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetLambda21WithEf.Database
{
    [Table("ProductCategory", Schema = "SalesLT")]
    public class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
            ProductCategory1 = new HashSet<ProductCategory>();
        }

        public int ProductCategoryID { get; set; }

        public int? ParentProductCategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<ProductCategory> ProductCategory1 { get; set; }

        public virtual ProductCategory ProductCategory2 { get; set; }
    }
}
