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
    
    public partial class Deleted_sales_order
    {
        public long del_sales_order_id { get; set; }
        public string del_sales_order_no { get; set; }
        public string del_sales_order_date { get; set; }
        public string del_sales_type { get; set; }
        public string del_material_code { get; set; }
        public string del_material_desc { get; set; }
        public string Sold_To_Party_Code { get; set; }
        public string Sold_To_Party_Name { get; set; }
        public string state { get; set; }
        public string Deleted_By { get; set; }
        public string Deletion_Date { get; set; }
        public Nullable<long> FkPlantId { get; set; }
        public Nullable<long> Order_qty { get; set; }
        public string bo_qty { get; set; }
        public string prev_tractor_sr_no { get; set; }
        public string prev_loader_sr_no { get; set; }
        public string prev_backhoe_sr_no { get; set; }
        public string prev_mower_sr_no { get; set; }
    }
}