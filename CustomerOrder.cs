using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Xunit;

namespace finalmvcfirst
{
    public class CustomerOrder
    {
        [Required(ErrorMessage = "Please enter CustomerID")]
        public string CustomerID { get; set; }
        [Required(ErrorMessage = "Please enter CutomerName")]
        public string CutomerName { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        public int OrderID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime orderDate { get; set; }
        public string OrderStatus { get; set; }
        
    }

}
