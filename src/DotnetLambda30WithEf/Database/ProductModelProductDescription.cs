using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetLambda30WithEf.Database
{
    [Table("ProductModelProductDescription", Schema = "SalesLT")]
    public class ProductModelProductDescription
    {
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductModelID { get; set; }

        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductDescriptionID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(6)]
        public string Culture { get; set; }

        public Guid rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ProductDescription ProductDescription { get; set; }

        public virtual ProductModel ProductModel { get; set; }
    }
}
