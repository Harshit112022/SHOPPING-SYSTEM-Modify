using CustomerModel;
//using Helper;
using ProductModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductOrderList
    {
        public int ProductId
        {
            get; set;
        }
        public int Quantity
        {
            get; set;
        }
    }
    public class TaskMasterModel
    {
        public List<ProductOrderList> ProductOrderList { get; set; }
        public List<TaskMasterModel> ListOrderDetails {get; set;}
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }
        public string OrderDate { get; set; }
        public int OrderDetailsId { get; set; }
        public int CountOrderId { get; set; }
        public List<Customer> CustomerList {  get; set;   }
        public List<Product> ProductModelList   {     get; set;    }
        public int CustomerId { get; set;  }
        public int PageNumber
        {
            get; set;
        } = 1;
        public int PageSize
        {
            get; set;
        } = 3;
        public int TotalRows
        {
            get; set;
        }
        public int TotalCount
        {
            get; set;
        }

    }
}
