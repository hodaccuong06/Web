//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shop_Web.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
    }
}