﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class DanhSachDuKhachDTO
    {
        public int MaTaiKhoan { get; set; }
        public string Ten { get; set; }
        public string SDT { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public DateTime NgayBatDau { get; set; }
        public string CCCD { get; set; }
    }
}
