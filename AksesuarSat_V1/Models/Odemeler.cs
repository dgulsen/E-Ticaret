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
    
    public partial class Odemeler
    {
        public int OdemelerID { get; set; }
        public int SiparisID { get; set; }
        public decimal ToplamTutari { get; set; }
        public decimal KargoTutari { get; set; }
        public System.DateTime SiparisTarihi { get; set; }
    
        public virtual Siparisler Siparisler { get; set; }
    }
}
