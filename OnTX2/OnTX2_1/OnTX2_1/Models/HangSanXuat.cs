namespace OnTX2_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HangSanXuat")]
    public partial class HangSanXuat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HangSanXuat()
        {
            SanPhams = new HashSet<SanPham>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "Bạn phải nhập Mã hãng")]
        [DisplayName("Mã hãng")]
        public int MaHang { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Bạn phải nhập Tên hãng")]
        [DisplayName("Tên hãng")]
        public string TenHang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
}
