namespace TestProject.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblCountry")]
    public partial class Country
    {
        [Key]
        [Column(Order = 0)]
        public Guid Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime CreatedAt { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool IsDisabled { get; set; }

        [Key]
        [Column(Order = 4)]
        public bool IsDefault { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(2)]
        public string ISO3166_2 { get; set; }

        [StringLength(3)]
        public string ISO3166_3 { get; set; }

        public Guid? DefaultVatId { get; set; }

        [StringLength(255)]
        public string PostalCodeFormat { get; set; }
    }
}
