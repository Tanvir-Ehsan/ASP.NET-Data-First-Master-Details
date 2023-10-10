using CrystalDecisions.CrystalReports.Engine;
using finalmvcfirst.Data_folder;
using finalmvcfirst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace finalmvcfirst.Controllers
{
    public class CustomerOrderController : Controller
    {
        // GET: CustomerOrder
        private ApplicationDbContext db = new ApplicationDbContext();
        List<Customer> mst = new List<Customer>();
        List<Order> sdt = new List<Order>();
        public ActionResult Index(string CustomerID = "")
        {
            CustomerOrder a = null;
            if(CustomerID !="") //Edit
            {
                sdt = (from d in db.Orders where d.CustomerID == CustomerID select d).ToList();
                Customer m = db.Customers.Find(CustomerID);
                a = new CustomerOrder { CustomerID = m.CustomerID};
                
            }
            ViewBag.Records = sdt;
            TempData["sdt"] = sdt;
            return View(a);
        }
        public void AddLine(CustomerOrder d)
        {
            sdt = TempData["sdt"] as List<Order>;
            if (sdt == null)
                sdt = new List<Order>();
            sdt.Add(new Order() { OrderID = d.OrderID, orderDate = d.orderDate, OrderStatus = d.OrderStatus });
            
            ViewBag.records = sdt;
            TempData["sdt"] = sdt;
        }
        public void SaveMe(CustomerOrder m2)
        {
            DeleteMe(m2.CustomerID);
            Customer md = new Customer() { CustomerID = m2.CustomerID, CutomerName = m2.CutomerName, BillingAddress = m2.BillingAddress, ShippingAddress = m2.ShippingAddress, };
            db.Customers.Add(md);
            db.SaveChanges();
            sdt = TempData["sdt"] as List<Order>;
            foreach (Order d in sdt)
            {
                Order r = new Order() { OrderID = d.OrderID,CustomerID=m2.CustomerID, orderDate = d.orderDate, OrderStatus = d.OrderStatus };
                db.Orders.Add(r);
                db.SaveChanges();
            }

            TempData["sdt"] = "";
            Session["sdt"] = "";

        }
        public void DeleteMe(string CustomerID)
        {
            db.Database.ExecuteSqlCommand($"delete Orders where CustomerID='{CustomerID}'");
            db.Database.ExecuteSqlCommand($"delete Customers where CustomerId='{CustomerID}'");
            db.SaveChanges();
        }
        [HttpPost]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult index(CustomerOrder m2, string ButtonType)    
        {
            if (ButtonType == "Add")
            {
                AddLine(m2);
                return PartialView("_PartialPage1");
            }
            else if (ButtonType == "Save")
                SaveMe(m2);
            else
                DeleteMe(m2.CustomerID);
            return Json(new { url = Url.Action("list") });
        }
        [Route("Main")]
        public ActionResult List()
        {
            TempData["sdt"] = "";
            List<CustomerOrder> c = new List<CustomerOrder>();
            var a = db.Customers.ToList();
            foreach (var m2 in a)
            {
                c.Add(new CustomerOrder { CustomerID = m2.CustomerID, CutomerName = m2.CutomerName, BillingAddress = m2.BillingAddress, ShippingAddress = m2.ShippingAddress,  });
            }
            return View(c);
        }
    }
}