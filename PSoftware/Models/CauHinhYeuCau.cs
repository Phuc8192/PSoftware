namespace PSoftware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CauHinhYeuCau")]
    public partial class CauHinhYeuCau
    {
        [Key]
        public Guid MaCHYC { get; set; }

        [StringLength(100)]
        public string CPU { get; set; }

        [StringLength(100)]
        public string RAM { get; set; }

        [StringLength(100)]
        public string GPU { get; set; }

        [StringLength(100)]
        public string Storage { get; set; }

        [StringLength(100)]
        public string OS { get; set; }

        [StringLength(100)]
        public string DirectX { get; set; }

        public Guid MaSP { get; set; }

        public virtual SanPham SanPham { get; set; }
    }
}
