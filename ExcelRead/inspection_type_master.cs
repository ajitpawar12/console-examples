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
    
    public partial class inspection_type_master
    {
        public inspection_type_master()
        {
            this.inspection_parameter_master = new HashSet<inspection_parameter_master>();
        }
    
        public long inspection_type_id { get; set; }
        public string inspection_type_name { get; set; }
        public string inspection_type_desc { get; set; }
        public Nullable<int> createdById { get; set; }
        public string createdDate { get; set; }
        public Nullable<int> modifiedById { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
    
        public virtual ICollection<inspection_parameter_master> inspection_parameter_master { get; set; }
    }
}
