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
    
    public partial class Container
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Container()
        {
            this.Vins = new HashSet<Vin>();
        }
    
        public int Id { get; set; }
        public Nullable<int> OrderItemId { get; set; }
        public string ContainerNum { get; set; }
    
        public virtual OrderItem OrderItem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vin> Vins { get; set; }
    }
}
