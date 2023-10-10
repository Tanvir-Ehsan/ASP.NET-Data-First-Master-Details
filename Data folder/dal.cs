using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace finalmvcfirst.Data_folder
{
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }
        public string CutomerName { get; set; }
        public string BillingAddress { get;set; }
        public string ShippingAddress { get; set; }
        public virtual ICollection<Order> Orederlink { get; set; }
    }
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [ForeignKey("Orderslink")]
        public string CustomerID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{o:yyyy-mm-dd}",ApplyFormatInEditMode =true)]
        public DateTime orderDate { get; set; }
        public string OrderStatus { get; set; }
        public virtual Customer Orderslink { get; set; }
    }
}