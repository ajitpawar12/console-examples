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
    
    public partial class GetInspectionStatus_Result
    {
        public long tractor_id { get; set; }
        public string storage_location { get; set; }
        public string material_type { get; set; }
        public string model_group { get; set; }
        public string material_code { get; set; }
        public string tractor_sr_no { get; set; }
        public string loader_sr_no { get; set; }
        public string backhoe_sr_no { get; set; }
        public string kitSerialNo { get; set; }
        public string mower_sr_no { get; set; }
        public string cab_sr_no { get; set; }
        public string ROPS { get; set; }
        public string tire_brand { get; set; }
        public string tire_type { get; set; }
        public string def_status { get; set; }
        public string Inspection_status { get; set; }
        public string ins_date { get; set; }
        public string lbm_ins_date { get; set; }
        public string Shipping_ins_date { get; set; }
        public string LBM_Inspection { get; set; }
        public Nullable<bool> IsLBMDone { get; set; }
        public Nullable<System.DateTime> StartInspectionDateTime { get; set; }
        public Nullable<System.DateTime> EndInspectionDateTime { get; set; }
        public int TimeRequiredinMin { get; set; }
    }
}
