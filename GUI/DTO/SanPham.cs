﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SanPham
    {
        public string MaSP { get; set; }
        public string MaNhom { get; set; }
        public string MaNCC { get; set; }

        public string TenSP { get; set; }

        public string DonViTinh { get; set; }

        public int SLTon { get; set; }

        public int GiaBan { get; set; }

        public int GiaNhap { get; set; }

        public int action { get; set; }
    }
}
