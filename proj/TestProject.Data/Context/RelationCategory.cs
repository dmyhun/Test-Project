namespace TestProject.Data.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblRelationCategory")]
    public partial class RelationCategory
    {
        [Key]
        [Column(Order = 0)]
        public Guid RelationId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid CategoryId { get; set; }
    }
}
