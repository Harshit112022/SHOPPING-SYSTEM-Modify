using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CustomerModel;
namespace DAL
{



    public class TaskMaster
    {
        
        public int OrderId { get; set; }
        public string OrderDate { get; set; }       
        public int CustomerId{    get; set;  }          
        public int CountOrderId { get; set;}
        public string xmlString { get; set; }
        private Database db;
        public TaskMaster()
        {
            this.db = DatabaseFactory.CreateDatabase();
        }
        public int GetCountOrder()
        {
          //  DataSet ds = null;
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

            //XElement Xbooks = new XElement("ProductOrderList");
            //foreach (var Product in this.ProductOrderList)
            //{
            //    XElement bookElement = new XElement("ProductOrder", new XElement("ProductId", Product.ProductId), new XElement("Quantity", Product.Quantity));
            //    Xbooks.Add(bookElement);
            //}
            //string XmlBooksString = Xbooks.ToString().Replace("\r\n", "");

          

            try
            {
                DbCommand com = this.db.GetStoredProcCommand("InsertOrderWithDetails");               
                if (this.CustomerId > 0)
                    db.AddInParameter(com, "CustomerId", DbType.Int32, this.CustomerId);
                else
                    db.AddInParameter(com, "CustomerId", DbType.Int32, DBNull.Value);


                if (!String.IsNullOrEmpty(this.OrderDate))
                         db.AddInParameter(com, "Date", DbType.String, this.OrderDate);
                else
                    db.AddInParameter(com, "Date", DbType.String, DBNull.Value);


                if (xmlString != null)
                {
                    db.AddInParameter(com, "OrderDetails", DbType.Xml,this.xmlString);
                }
                else
                    db.AddInParameter(com, "OrderDetails", DbType.Xml, DBNull.Value);


                this.db.ExecuteNonQuery(com);
              
            }
            catch (Exception ex)
            {
                // To Do: Handle Exception
                return false;
            }
            return true; // Return whether ID was returned
        }
     

    }
}
