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
    
    public partial class Orders
    {
        public Orders()
        {
            this.DishOrderRelation = new HashSet<DishOrderRelation>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual ICollection<DishOrderRelation> DishOrderRelation { get; set; }
        public virtual Users Users { get; set; }
    }
}