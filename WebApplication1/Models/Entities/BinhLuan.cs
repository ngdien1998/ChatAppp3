namespace WebApplication1.Models.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public int IDBinhLuan { get; set; }

        public int IDBaiDang { get; set; }

        [Required]
        [StringLength(50)]
        public string TenNguoiBinhLuan { get; set; }

        [Required]
        [StringLength(200)]
        public string NoiDung { get; set; }

        public DateTime ThoiGian { get; set; }

        public virtual BaiDang BaiDang { get; set; }

        public virtual NguoiDung NguoiDung { get; set; }
    }
}
