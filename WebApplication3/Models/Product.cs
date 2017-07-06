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
    using System.ComponentModel.DataAnnotations;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
    
        public int id { get; set; }
        [Required,Display(Name="Tên sản phẩm")]
        public string name { get; set; }
        [Required,Display(Name="Tên loại sản phẩm")]
        public Nullable<int> categoryid { get; set; }
        [Required,Display(Name="Tên nhà cung cấp")]
        public Nullable<int> supplierid { get; set; }
        [Required,Display(Name="Ảnh sản phẩm")]
        public string image { get; set; }
        [Display(Name="Giá của sản phẩm")]
        public Nullable<double> price { get; set; }
        [Display(Name="Số lượng sản phẩm")]
        public Nullable<int> quantity { get; set; }
        [Display(Name="Thông tin sản phẩm")]
        public string information { get; set; }
        [Display(Name="Ngày tạo sản phẩm")]
        public Nullable<System.DateTime> datecreate { get; set; }
        [Display(Name="Chi tiết sản phẩm")]
        public string description { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
