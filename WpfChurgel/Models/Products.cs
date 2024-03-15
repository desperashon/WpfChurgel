//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfChurgel.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Products
    {
        public Products()
        {
            this.ProductDishRelation = new HashSet<ProductDishRelation>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> UnitId { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Protein { get; set; }
        public Nullable<decimal> Fat { get; set; }
        public Nullable<decimal> Acne { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual ICollection<ProductDishRelation> ProductDishRelation { get; set; }
        public virtual UnitId UnitId1 { get; set; }
    }
}