using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Lab2
{
    public class NhanVien
    {
        public Guid MaNV { get; set; }
        public string HoNV { get; set; }
        public string TenNV { get; set; }
        public string TenLot { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string Phai { get; set; }
        public decimal? Luong { get; set; }
        public string Ma_NQL { get; set; }
        public int PHG { get; set; }
        public List<ThanNhan> ThanNhan { get; set; }
    }
    public class ThanNhan
    {
        public Guid Ma_NVien { get; set; }
        public string TenNV { get; set; }
        public string Phai { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string QuanHe { get; set; }
        public NhanVien NhanVien { get; set; }
    }
}
