namespace PSoftware.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            BinhLuans = new HashSet<BinhLuan>();
            CauHinhKhuyenNghis = new HashSet<CauHinhKhuyenNghi>();
            CauHinhYeuCaus = new HashSet<CauHinhYeuCau>();
            CDKeys = new HashSet<CDKey>();
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        public Guid MaSP { get; set; }

        [Required]
        [StringLength(50)]
        public string TenSP { get; set; }

        public double Gia { get; set; }

        public DateTime NgayTao { get; set; }

        public DateTime NgayChinhSua { get; set; }

        [Required]
        public string Anh { get; set; }

        [Required]
        public string NoiDung { get; set; }

        public Guid MaLSP { get; set; }

        public Guid MaNPT { get; set; }

        public Guid MaTL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BinhLuan> BinhLuans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHinhKhuyenNghi> CauHinhKhuyenNghis { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHinhYeuCau> CauHinhYeuCaus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CDKey> CDKeys { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }

        public virtual NhaPhatTrien NhaPhatTrien { get; set; }

        public virtual TheLoai TheLoai { get; set; }
    }
}
