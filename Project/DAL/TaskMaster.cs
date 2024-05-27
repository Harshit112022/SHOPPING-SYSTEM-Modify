using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class TaskMaster
    {
        public int OrderId { get; set; }
        public string OrderDate { get; set; }
        public int OrderDetailsId { get; set; }
        public string CustomerName{  get; set;}
        public int CustomerId{    get; set;  }
        public int ProductId{get; set; }
        public string ProductName {get; set;  }
        public int ProductPrice {get; set; }
        public string ProductCategory{ get; set;}
        public int ProductCategoriesId { get; set;}
        public int Quantity{get; set;}
        public int CountOrderId { get; set;}
        private Database db;
        public TaskMaster()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public int GetCountOrder()
        {
            DataSet ds = null;
            try
            {
                DbCommand com = db.GetStoredProcCommand("CountOrderId");
                db.AddOutParameter(com, "Count", DbType.Int32, 1024);
                this.db.ExecuteNonQuery(com);
                int Count = (int)db.GetParameterValue(com, "Count");
                return Count+1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }
        public bool Save()
        {
            if(this.OrderId==0)
            {
                 return   OrdersInsert();

            }
            else
            {
                return false;
            }
        } 
        private bool OrdersInsert()
        {
            //this this = new this();
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("OrdersInsert");               
                if (this.CustomerId > 0)
                    db.AddInParameter(com, "CustomerId", DbType.Int32, this.CustomerId);

                if (!String.IsNullOrEmpty(this.OrderDate))
                         db.AddInParameter(com, "Date", DbType.String, this.OrderDate);
                this.db.ExecuteNonQuery(com);
              
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
            return true; // Return whether ID was returned
        }
        public bool OrderDetailsInsert()
        {
            try
            {
                DbCommand com = this.db.GetStoredProcCommand("OrderDetailInsert");
                //    this.db.AddOutParameter(com, "TagId", DbType.Int32, 1024); 
                if (this.ProductId > 0)
                    db.AddInParameter(com, "ProductId", DbType.Int32, this.ProductId);
                if (this.Quantity > 0)
                    db.AddInParameter(com, "Quantity", DbType.Int32, this.Quantity);                
                this.db.ExecuteNonQuery(com);

            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
            return true; // Return whet
        }

    }
}
