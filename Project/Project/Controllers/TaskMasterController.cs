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
            TaskMasterModel taskMasterModel = new TaskMasterModel();
            TaskMaster taskMaster = new TaskMaster();
            Customer customer = new Customer();
            CustomerDAL customerDAL = new CustomerDAL();
            DataLaye DataLayeProduct = new DataLaye();
            StringBuilder xmlString = new StringBuilder();


        // GET: TaskMaster
        public ActionResult Index(TaskMasterModel taskMasterModel)
        {
            try
            {

                taskMasterModel.CountOrderId = taskMaster.GetCountOrder();
                taskMasterModel.CustomerList = customerDAL.GetList(customer);
                    if(taskMasterModel.CustomerId>0)
                    {
                        taskMaster.CustomerId = taskMasterModel.CustomerId;
                        taskMasterModel.ListOrderDetails = taskMaster.GetOrderDetails();
                        
                    }
                return View(taskMasterModel);

            }
            catch (Exception ex)
            {
                return Json(new { message = "Error occeer"});
            }
        }
       
        public ActionResult GetProductListView()
        {
            try
            {                            
              return PartialView("_GetProductList", taskMasterModel);
            }
            catch (Exception ex)
            {
                return Json(new { success=false, message = "Error occeer" });
            }

        }
      
        public ActionResult GetProductList(TaskMasterModel taskMasterModel)
        {    
            try
            {
                DataLayeProduct.PageNumber = taskMasterModel.PageNumber;
                DataLayeProduct.PageSize = taskMasterModel.PageSize;
                taskMasterModel.ProductModelList = DataLayeProduct.GetList();
                taskMasterModel.TotalCount = DataLayeProduct.TotalCount;
                taskMasterModel.TotalRows = DataLayeProduct.TotalRows;
                return Json(taskMasterModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occeer" });
            }

        }

        [HttpPost]
        public ActionResult InsertOdrder(TaskMasterModel taskMasterModel)
        {
            try
            {
                    if (ConvertToXml(taskMasterModel.ProductOrderList))
                    {  
                         taskMaster.CustomerId = taskMasterModel.CustomerId;
                         taskMaster.OrderDate = taskMasterModel.OrderDate;  
                         taskMaster.xmlString = xmlString.ToString();                       
                         return Json(taskMaster.Save());
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