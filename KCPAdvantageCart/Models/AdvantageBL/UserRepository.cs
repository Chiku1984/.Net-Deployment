using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KCPAdvantageModel.Entity;
using KCPAdvantageCart.Models.AdvantageBO;

namespace KCPAdvantageCart.Models.AdvantageBL
{
    public class UserRepository
    {
        private KCPAdvanatgeEntities context;
        private KCPAdvanatgeEntities writeContext;

        public UserRepository()
        {
            this.context = new KCPAdvanatgeEntities();
            this.writeContext = new KCPAdvanatgeEntities();
        }
        public List<CartEntity> GetUserCartDetails(string userEmail)
        {
            List<CartEntity> objUserCartDetails = new List<CartEntity>();
            objUserCartDetails = (from uc in context.UserCarts
                                  where uc.UserEmail == userEmail && uc.Active==true
                                  select new CartEntity { 
                                    Id = uc.Id,
                                    UserEmail=uc.UserEmail,
                                    ProductName=uc.ProductName,
                                    ProductDescription=uc.ProductDescription,
                                    ProductSKU=uc.ProductSKU,
                                    Quantity=uc.Quantity,
                                    UnitRate=uc.UnitRate,
                                    MSRP = uc.Quantity * uc.UnitRate,
                                    UnitTotalCost = (uc.Quantity * uc.UnitRate) - (((uc.Quantity * uc.UnitRate) * uc.Discount)/100),
                                    Discount=uc.Discount,
                                    CreatedDate=uc.CreatedDate
                                  }
                                  ).ToList();
            return objUserCartDetails;
        }
        public bool DeleteFromCart(long id)
        {
            bool status = false;
            try
            {
                UserCart objCart = context.UserCarts.Where(x => x.Id == id).FirstOrDefault();
                if (objCart != null)
                {
                    objCart.Active = false;
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                
            }

            return status;
        }
        public bool ClearCart(string userEmail)
        {
            bool status = false;
            try
            {
                var objCartDetails = context.UserCarts.Where(x => x.UserEmail == userEmail);
                foreach (var item in objCartDetails)
                {
                    UserCart objCart = writeContext.UserCarts.Where(x => x.Id == item.Id).FirstOrDefault();
                    if (objCart != null)
                    {
                        objCart.Active = false;
                        writeContext.SaveChanges();
                        status = true;
                    }
                }
                
            }
            catch (Exception ex)
            {

            }

            return status;
        }
        public bool UpdateItemQuantity(string Type, long id)
        {
            bool status = false;
            try
            {
                UserCart objCart = context.UserCarts.Where(x => x.Id == id).FirstOrDefault();
                if (objCart != null)
                {
                    if(Type.Trim().ToLower() == "inc")
                        objCart.Quantity = objCart.Quantity + 1;
                    if (Type.Trim().ToLower() == "des")
                        objCart.Quantity = objCart.Quantity - 1;
                    
                    context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception ex)
            {
                
            }
            return status;
        }
    }
}