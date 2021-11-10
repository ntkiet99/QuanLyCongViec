using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyCongViec.Models
{
    public class CongViec
    {
        public int CongViecId { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayKetThuc { get; set; } = DateTime.Now;
        public DateTime NgayBatDau { get; set; } = DateTime.Now;
        public int NguoiThucHien { get; set; }
        public int NguoiDuocGiao { get; set; }
        public int? ChaId { get; set; }
        public virtual CongViec Cha { get; set; }
        public virtual ICollection<CongViec> Cons { get; set; }
    }
}