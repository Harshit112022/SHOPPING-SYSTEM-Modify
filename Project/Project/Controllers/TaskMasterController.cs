using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using Model;
using CustomerModel;
using DataAccessLayeProduct;
using System.Text;

namespace Project.Controllers
{
    public class TaskMasterController : Controller
    {
            TaskMaster taskMaster = new TaskMaster();
            TaskMasterModel taskMasterModel = new TaskMasterModel();
            Customer customer = new Customer();
            CustomerDAL customerDAL = new CustomerDAL();
            DataLaye DataLayeProduct = new DataLaye();
            StringBuilder xmlString = new StringBuilder();


        // GET: TaskMaster
        public ActionResult Index()
        {
            taskMasterModel.CountOrderId = taskMaster.GetCountOrder();
            taskMasterModel.CustomerList = customerDAL.GetList(customer);
         
            return View(taskMasterModel);
        }




        public ActionResult GetProductList()
        {
            taskMasterModel.ProductModelList = DataLayeProduct.GetList();
            return PartialView("_GetProductList", taskMasterModel);

        }
        [HttpPost]
        public ActionResult InsertOdrder(TaskMasterModel taskMasterModel)
        {
            try
            {
                    taskMaster.CustomerId = taskMasterModel.CustomerId;
                    taskMaster.OrderDate = taskMasterModel.OrderDate;  
                    if (ConvertToXml(taskMasterModel.ProductOrderList))
                    {  
                         taskMaster.xmlString = xmlString.ToString();
                         taskMaster.Save();
                         return Json(true);
                    }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return Json(false );

        }

        public bool ConvertToXml(List<ProductOrderList> productOrderList)
        {
            if (productOrderList == null || productOrderList.Count == 0)
                return false;

            
           
            xmlString.AppendLine("<?xml version=\"1.0\"?>");
            xmlString.AppendLine("<ProductOrderList>");
            foreach (var productOrder in productOrderList)
            {
                 xmlString.AppendLine("<ProductOrder>");
                 xmlString.AppendLine($"    <ProductId>{productOrder.ProductId}</ProductId>");
                 xmlString.AppendLine($"    <Quantity>{productOrder.Quantity}</Quantity>");
                 xmlString.AppendLine("  </ProductOrder>");
            }

                xmlString.AppendLine("</ProductOrderList>");    
                return true;
        }




    }
}