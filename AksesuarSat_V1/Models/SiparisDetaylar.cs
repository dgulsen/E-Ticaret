//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AksesuarSat_V1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SiparisDetaylar
    {
        public int SiparisID { get; set; }
        public int UrunID { get; set; }
        public decimal BirimFiyat { get; set; }
        public int Miktar { get; set; }
        public int indirimID { get; set; }
    
        public virtual indirimler indirimler { get; set; }
        public virtual Siparisler Siparisler { get; set; }
        public virtual Urunler Urunler { get; set; }
    }
}