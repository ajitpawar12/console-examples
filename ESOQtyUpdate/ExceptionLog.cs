//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESOQtyUpdate
{
    using System;
    using System.Collections.Generic;
    
    public partial class ExceptionLog
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public System.DateTime LoggedDateTime { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
