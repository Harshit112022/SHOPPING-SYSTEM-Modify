using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Helper;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
   
 

    public class TaskMaster
    {
        
        public List<ProductOrderList> ProductOrderList { get; set; }
        public int OrderId { get; set; }
        public string OrderDate { get; set; }       
        public int CustomerId{    get; set;  }          
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

        public string ConvertToXml(List<ProductOrderList> productOrderList)
        {
            if (productOrderList == null || productOrderList.Count == 0)
                return null;

            StringBuilder xmlBuilder = new StringBuilder();
           
            xmlBuilder.AppendLine("<?xml version=\"1.0\"?>");
            xmlBuilder.AppendLine("<ProductOrderList>");

            foreach (var productOrder in productOrderList)
            {
                xmlBuilder.AppendLine("<ProductOrder>");
                xmlBuilder.AppendLine($"    <ProductId>{productOrder.ProductId}</ProductId>");
                xmlBuilder.AppendLine($"    <Quantity>{productOrder.Quantity}</Quantity>");
                xmlBuilder.AppendLine("  </ProductOrder>");
            }

            xmlBuilder.AppendLine("</ProductOrderList>");

            return xmlBuilder.ToString();
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

            string xmlBuilder = ConvertToXml(this.ProductOrderList);

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


                if (xmlBuilder != null)
                {
                    db.AddInParameter(com, "OrderDetails", DbType.Xml, xmlBuilder);
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
