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
    
    public partial class Siparisler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Siparisler()
        {
            this.Odemeler = new HashSet<Odemeler>();
            this.SiparisDetaylar = new HashSet<SiparisDetaylar>();
        }
    
        public int SiparislerID { get; set; }
        public int UyeID { get; set; }
        public System.DateTime SiparisTarihi { get; set; }
        public Nullable<System.DateTime> SiparisTeslimTarihi { get; set; }
        public decimal Tutar { get; set; }
        public Nullable<decimal> KargoTutari { get; set; }
        public string Aciklama { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Odemeler> Odemeler { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisDetaylar> SiparisDetaylar { get; set; }
        public virtual Uyeler Uyeler { get; set; }
    }
}
