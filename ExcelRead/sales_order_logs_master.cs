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
    
    public partial class sales_order_logs_master
    {
        public int sales_orderlog_id { get; set; }
        public string sales_order_no { get; set; }
        public string approval_status { get; set; }
        public string carrier { get; set; }
        public string invoice { get; set; }
        public string ship_date { get; set; }
        public string invoice_date { get; set; }
        public string actual_ship_date { get; set; }
        public Nullable<long> FkDespatch_closure_id { get; set; }
        public string tractor_sr_no { get; set; }
        public string loader_sr_no { get; set; }
        public string backhoe_sr_no { get; set; }
        public string mower_sr_no { get; set; }
        public string cab_sr_no { get; set; }
        public string kit_sr_no { get; set; }
        public string Sent_To_Inspection_date { get; set; }
        public string EOM { get; set; }
        public Nullable<long> FKSales_order_id { get; set; }
        public Nullable<long> FKUser_id { get; set; }
        public Nullable<long> FKUsertype_id { get; set; }
        public Nullable<long> FKPlant_id { get; set; }
        public Nullable<System.DateTime> LastModified_date { get; set; }
    }
}