﻿using CustomerModel;
using DAL;
using ProductCategoriesModel;
using ProductModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;

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
    public class ShopModel
    {

        public List<ProductOrderList> ProductOrderList { get; set; }
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public int OrderDetailsId { get; set; }
        public int CountOrderId { get; set; }
        public int TotalRecord
        {
            get; set;
        }
        public int? PageSize
        {
            get; set;
        } = 10;
        public int? PageNumber
        {
            get; set;
        } = 1;
        public string CustomerName
        {
            get; set;
        }
        public string DateSaleOrders
        {
            get; set;
        }
        public string ProductName
        {
            get; set;
        }
        public string ProductCategory
        {
            get; set;
        }
        public int ProductCategoriesId
        {
            get; set;
        }
        public int Quantity{ get; set;  } 
        public int ProductPrice
        {
            get; set;
        }
        public int ProductId
        {
            get; set;
        }
        public int TotalAmmount
        {
            get; set;
        }
        public List<Product> ProductModelList
        {
            get; set;
        }
        public List<ShopMaster> MasterList
        {
            get; set;
        }
        public List<int> ProductIdList { get; set; }

        //public DataSet MasterList
        //{
        //    get; set;
        //}
        public  string ToDate
        {
            get; set;
        }
        public string FromDate
        {
            get; set;
        }
   
        public List<ProductCategories> ProductCategoryModel
        {
            get; set;
        }    
        public List<Customer>CustomerList
        {
            get; set;
        }
      
        public int CustomerId
        {
            get; set;
        }

        public int TotalCount 
        {
            get; set;
        }
        public int SaleOrdersDetailsId
        {
            get; set;
        }
    }
}
