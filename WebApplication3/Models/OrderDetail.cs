//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int orderid { get; set; }
        public int productid { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<int> quantity { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Order Order1 { get; set; }
        public virtual Product Product { get; set; }
    }
}
