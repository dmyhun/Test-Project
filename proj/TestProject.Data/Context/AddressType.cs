namespace TestProject.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblAddressType")]
    public partial class AddressType
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

        public Guid? ParentId { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Code1 { get; set; }

        [StringLength(255)]
        public string Code2 { get; set; }

        [StringLength(255)]
        public string Code3 { get; set; }

        [StringLength(255)]
        public string Code4 { get; set; }

        [StringLength(255)]
        public string Code5 { get; set; }

        [StringLength(255)]
        public string Code6 { get; set; }

        public double? Value1 { get; set; }

        public double? Value2 { get; set; }

        public double? Value3 { get; set; }

        public double? Value4 { get; set; }

        public bool? Flag1 { get; set; }

        public bool? Flag2 { get; set; }

        public bool? Flag3 { get; set; }

        public bool? Flag4 { get; set; }

        public DateTime? Timestamp1 { get; set; }

        public DateTime? Timestamp2 { get; set; }

        public DateTime? Timestamp3 { get; set; }

        public DateTime? Timestamp4 { get; set; }
    }
}
