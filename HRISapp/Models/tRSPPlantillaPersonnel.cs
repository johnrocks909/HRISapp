//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HRISapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tRSPPlantillaPersonnel
    {
        public int recNo { get; set; }
        public string plantillaCode { get; set; }
        public string EIC { get; set; }
        public Nullable<bool> isActive { get; set; }
    
        public virtual tRSPEmployee tRSPEmployee { get; set; }
        public virtual tRSPPlantilla tRSPPlantilla { get; set; }
    }
}
