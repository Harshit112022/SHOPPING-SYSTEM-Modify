using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Model;
using CustomerModel;
using DataAccessLayeProduct;

namespace Project.Controllers
{
    public class TaskMasterController : Controller
    {
            TaskMaster taskMaster = new TaskMaster();
            ShopModel shopModel = new ShopModel();
            Customer customer = new Customer();
            CustomerDAL customerDAL = new CustomerDAL();
            DataLaye DataLayeProduct = new DataLaye();


        // GET: TaskMaster
        public ActionResult Index()
        {
            shopModel.CountOrderId = taskMaster.GetCountOrder();
            shopModel.CustomerList = customerDAL.GetList(customer);
            return View(shopModel);
        }




        public ActionResult GetProductList()
        {
            shopModel.ProductModelList = DataLayeProduct.GetList();
            return PartialView("_GetProductList", shopModel);

        }
        [HttpPost]
        public ActionResult InsertOdrder(ShopModel shopModel)
        {
            taskMaster.CustomerId = shopModel.CustomerId;
            taskMaster.OrderDate = shopModel.OrderDate;
            taskMaster.Save();
            foreach (var pair in shopModel.ProductOrderList)
            {
                taskMaster.ProductId = pair.ProductId;
                taskMaster.Quantity = pair.Quantity;
               bool var = taskMaster.OrderDetailsInsert();

            }
            return Json(true);
        }




    }
}