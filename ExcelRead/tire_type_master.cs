//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExcelRead
{
    using System;
    using System.Collections.Generic;
    
    public partial class tire_type_master
    {
        public long tire_type_id { get; set; }
        public string tire_type_name { get; set; }
        public string tire_type_desc { get; set; }
        public Nullable<long> FkTireBrandId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
