using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KCPAdvantageCart.Models.AdvantageBO;
using KCPAdvantageCart.Models.AdvantageBL;
using System.Runtime.Remoting.Messaging;

namespace KCPAdvantageCart.Controllers
{
    public class HomeController : Controller
    {
        UserRepository userRepository = new UserRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult GetCartDetails(string id)
        {
            string email = "chandan.kumar@kcc.com";
            decimal cartTotal = 0;
            decimal msrpTotal = 0;
            int itemTotalQuantity = 0;

            List<CartEntity> objLstCartDetail = userRepository.GetUserCartDetails(email);
            foreach (var item in objLstCartDetail)
            {
                cartTotal = cartTotal + item.UnitTotalCost ?? 0;
                msrpTotal = msrpTotal + item.MSRP ?? 0;
                itemTotalQuantity = itemTotalQuantity + item.Quantity ?? 0;
            }
            ViewBag.CartTotal = cartTotal;
            ViewBag.MsrpTotal = msrpTotal;
            ViewBag.ItemTotalQuantity = itemTotalQuantity;

            return PartialView("~/Views/Home/_UserCartDetails.cshtml",objLstCartDetail);
        }
        public ActionResult RemoveFromCart(string cartId)
        {
            bool status = userRepository.DeleteFromCart(Convert.ToInt64(cartId));

            return Json(new { Status=status }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ClearCart(string userEmail)
        {
            userEmail = "chandan.kumar@kcc.com";
            bool status = userRepository.ClearCart(userEmail);

            return Json(new { Status = status }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateItemQuantity(string Type, string CartId)
        {
            bool status = userRepository.UpdateItemQuantity(Type, Convert.ToInt64(CartId));

            return Json(new { Status = status }, JsonRequestBehavior.AllowGet);
        }
    }
}