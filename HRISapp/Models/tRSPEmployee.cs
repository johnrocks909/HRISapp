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
    
    public partial class tRSPEmployee
    {
        public tRSPEmployee()
        {
            this.tRSPPlantillaPersonnels = new HashSet<tRSPPlantillaPersonnel>();
            this.tRSPServiceRecords = new HashSet<tRSPServiceRecord>();
            this.tRSPServiceRecordRemarks = new HashSet<tRSPServiceRecordRemark>();
        }
    
        public int recNo { get; set; }
        public string EIC { get; set; }
        public string idNo { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string extName { get; set; }
        public Nullable<System.DateTime> birthDate { get; set; }
        public string birthPlace { get; set; }
    
        public virtual ICollection<tRSPPlantillaPersonnel> tRSPPlantillaPersonnels { get; set; }
        public virtual ICollection<tRSPServiceRecord> tRSPServiceRecords { get; set; }
        public virtual ICollection<tRSPServiceRecordRemark> tRSPServiceRecordRemarks { get; set; }
    }
}