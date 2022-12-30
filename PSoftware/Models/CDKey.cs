namespace PSoftware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CDKey")]
    public partial class CDKey
    {
        [Key]
        [Column(Order = 0)]
        public Guid MaHD { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid MaSP { get; set; }

        [Column("CDKey")]
        [Required]
        [StringLength(40)]
        public string CDKey1 { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
