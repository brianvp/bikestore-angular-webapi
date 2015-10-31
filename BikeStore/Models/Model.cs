namespace BikeStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product.Model")]
    public partial class Model
    {
        public int ModelId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(25)]
        public string ManufacturerCode { get; set; }

        public int? CategoryId { get; set; }

        public string Description { get; set; }

        public string Features { get; set; }

        public int? StatusId { get; set; }

        public int? ManufacturerId { get; set; }

        [Column(TypeName = "money")]
        public decimal? ListPrice { get; set; }

        public string ImageCollection { get; set; }

        public string CategoryCustomData { get; set; }

        public string ManufacturerCustomData { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateModified { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DateCreated { get; set; }

        public  Category Category { get; set; }

        public  Manufacturer Manufacturer { get; set; }

        public  Status Status { get; set; }
    }
}
