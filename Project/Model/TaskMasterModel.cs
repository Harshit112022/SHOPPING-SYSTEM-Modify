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
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public int OrderDetailsId { get; set; }
        public int CountOrderId { get; set; }
        public List<Customer> CustomerList {  get; set;   }
        public List<Product> ProductModelList   {     get; set;    }
        public int CustomerId { get; set;  }

    }
}
