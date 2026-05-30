namespace OnTX2_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Bạn phải nhập Mã sản phẩm")]
        [DisplayName("Mã sản phẩm")]
        public int MaSP { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bạn phải nhập Tên sản phẩm")]
        [DisplayName("Tên sản phẩm")]
        public string TenSP { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập Số lượng")]
        [DisplayName("Số lượng")]

        public int? SoLuong { get; set; }
        [Required(ErrorMessage = "Bạn phải nhập Đơn giá")]
        [DisplayName("Đơn giá")]

        public decimal? DonGia { get; set; }

        [StringLength(50)]
        [DisplayName("Hình Ảnh")]
        public string HinhAnh { get; set; }

        public int? MaHang { get; set; }

        public virtual HangSanXuat HangSanXuat { get; set; }
    }
}
