//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EFGUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class DMSinhVien
    {
        public string MaSV { get; set; }
        public string HoTenSV { get; set; }
        public string Phai { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string NoiSinh { get; set; }
        public string MaKhoa { get; set; }
    
        public virtual Khoa Khoa { get; set; }
    }
}