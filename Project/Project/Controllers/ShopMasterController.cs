using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Model;
using DataAccessLayeProduct;
using DataAccessLayeProductCategories;
using CustomerModel;
namespace Project.Controllers
{
    public class ShopMasterController : Controller
    {

        #region
        ShopModel shopModel = new ShopModel();
        ShopMaster shopMaster = new ShopMaster();
        DataLaye DataLayeProduct = new DataLaye();
        DataLayeCategories dataLayeCategories = new DataLayeCategories();
        CustomerDAL customerDAL = new CustomerDAL();
        Customer customer = new Customer();
        #endregion
        // GET: ShopMaster
        public ActionResult Index()
        {
            ShopModel shopModel = new ShopModel();          
            shopModel.ProductModelList = DataLayeProduct.GetList();
            shopModel.ProductCategoryModel = dataLayeCategories.GetList();
            return View(shopModel);
        }
       
        public ActionResult GetList(ShopModel shopModel)
        {
            //int? pageNumber = Session["PageNumber"] as int?;
            shopMaster.CustomerName = shopModel.CustomerName;
            shopMaster.CustomerId = shopModel.CustomerId;
            shopMaster.ProductId = shopModel.ProductId;
            shopMaster.ProductName = shopModel.ProductName;
            shopMaster.ProductCategoriesId = shopModel.ProductCategoriesId;
            shopMaster.ProductCategory = shopModel.ProductCategory;
            shopMaster.ProductPrice = shopModel.ProductPrice;
            shopMaster.TotalAmmount = shopModel.TotalAmmount;
            shopMaster.PageNumber = shopModel.PageNumber;
            
            //if (pageNumber.HasValue)
            //{
            //    shopMaster.PageNumber = pageNumber.Value;
            //}
            //else
            //{
            //    shopMaster.PageNumber = shopModel.PageNumber;
            //}
            shopMaster.PageSize = shopModel.PageSize;
            shopMaster.FromDate = shopModel.FromDate;
            shopMaster.ToDate = shopModel.ToDate;
            shopModel.MasterList = shopMaster.GetList();
            shopModel.TotalCount = shopMaster.TotalCount;
            shopModel.TotalRecord = shopMaster.TotalRecord;
            shopModel.ProductModelList = DataLayeProduct.GetList();
            shopModel.ProductCategoryModel = dataLayeCategories.GetList();
            //Session["PageNumber"] = null;
            return Json(shopModel, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Insert()
        {
            shopModel.CustomerList = customerDAL.GetList(customer);
            shopModel.ProductModelList = DataLayeProduct.GetList();
            shopModel.ProductCategoryModel = dataLayeCategories.GetList();
            return View("_Insert", shopModel);
            // return View(shopModel);
        }
        [HttpPost]
        public ActionResult Insert(ShopModel shopModel)
        {
            //shopModel.CustomerList = customerDAL.GetList(customer);
            //shopModel.ProductModelList = DataLayeProduct.GetList();
            //shopModel.ProductCategoryModel = dataLayeCategories.GetList();         
            shopMaster.CustomerId = shopModel.CustomerId;
            shopMaster.ProductId = shopModel.ProductId;
            shopMaster.Quantity = shopModel.Quantity;
            shopMaster.DateSaleOrders = shopModel.DateSaleOrders;
            shopMaster.Save();
            return Redirect("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (shopMaster.Delete(id))
            {
                return Json(new { Success = true, Message = "Item deleted successfully" });
            }
            else
            {
                return Json(new { Success = false, Message = "Item is not deleted" });
            }
        }

        [HttpGet]
        public ActionResult Edit(ShopModel shopModel)
        {
            //Session["PageNumber"] = shopModel.PageNumber as int?;
            shopMaster.SaleOrdersDetailsId = shopModel.SaleOrdersDetailsId;
            shopMaster.Load();
            shopMaster.PageNumber = shopModel.PageNumber;
            shopModel.CustomerName = shopMaster.CustomerName;
            shopModel.CustomerId = shopMaster.CustomerId;
            shopModel.DateSaleOrders = shopMaster.DateSaleOrders;
            shopModel.ProductName = shopMaster.ProductName;
            shopModel.ProductId = shopMaster.ProductId;
            shopModel.Quantity = shopMaster.Quantity;
            shopModel.CustomerList = customerDAL.GetList(customer);
            shopModel.ProductModelList = DataLayeProduct.GetList();
            shopModel.ProductCategoryModel = dataLayeCategories.GetList();
            //return View(shopModel);
            return View("_Edit", shopModel);
        }
        [HttpPost]
        //JsonResult
        public JsonResult Edit(ShopModel shopModel, ShopMaster shopMaster)
        {
            shopMaster.SaleOrdersDetailsId = shopModel.SaleOrdersDetailsId;
            shopMaster.CustomerId = shopModel.CustomerId;
            shopMaster.ProductId = shopModel.ProductId;
            shopMaster.DateSaleOrders = shopModel.DateSaleOrders;
            shopModel.Quantity = shopMaster.Quantity;
            shopModel.PageNumber = shopMaster.PageNumber;
            //if (pageNumber.HasValue)
            //{
            //    shopModel.PageNumber = pageNumber.Value;
            //}
            shopModel.CustomerList = customerDAL.GetList(customer);
            shopModel.ProductModelList = DataLayeProduct.GetList();
            shopModel.ProductCategoryModel = dataLayeCategories.GetList();
            shopMaster.Save();
            //return Redirect("Index");
            return Json(new { success = true, message = "edit successfull " });
        }
    }
}