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
    
    public partial class task_master
    {
        public task_master()
        {
            this.performby_test = new HashSet<performby_test>();
        }
    
        public int task_id { get; set; }
        public string task_name { get; set; }
        public string task_desc { get; set; }
        public Nullable<int> createdById { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public Nullable<int> modifiedById { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
    
        public virtual ICollection<performby_test> performby_test { get; set; }
    }
}