//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class KisiSoruları
    {
        public int ID { get; set; }
        public Nullable<int> KisiID { get; set; }
        public Nullable<int> SoruID { get; set; }
        public string Cevap { get; set; }
        public Nullable<System.DateTime> CevapTarihi { get; set; }
    
        public virtual Kisi Kisi { get; set; }
        public virtual Soru Soru { get; set; }
    }
}
