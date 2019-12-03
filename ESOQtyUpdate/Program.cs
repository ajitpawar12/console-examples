using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESOQtyUpdate
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString() + "--------Scheduler started------");
                Logger.LogFileWrite("\n--------ESOQtyUpdate Lookup Start------");
                IOVinTrackingEntities _context = new IOVinTrackingEntities();

                var omList = new List<OrderModel>();
                _context.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


                string ExcerlSheetName = "";
                //string DefectGroupFIlePath = @"E:\ePDI csv files\DefectGroup.xlsx";
                

                string Filepath = @"E:\ESOQtyUpdateFiles\ESOQtyUpdate.xlsx";
               

                DataTable NonUploaded = new DataTable();

                NonUploaded.TableName = "NonuploadedData";
                NonUploaded.Columns.Add("UniqueFeild");
                NonUploaded.Columns.Add("Reason");

                //Defect Group lookup

                Console.WriteLine("\n--------ESOQtyUpdate Lookup Start------");
                Logger.LogFileWrite("\n--------ESOQtyUpdate Lookup Start------");

                DataSet dcgExcel = ExcelToDataSetForESOQty(Filepath, ".xlsx", "EsoQty", ref ExcerlSheetName);
                var contdg = dcgExcel.Tables[0].Rows.Count;

                foreach (DataRow item in dcgExcel.Tables[0].Rows)
                {
                    omList.Add(
                        new OrderModel
                        {
                            Order = Convert.ToString(item["Order"]),
                            Model = Convert.ToString(item["Model"]),
                            ESOQty= !string.IsNullOrWhiteSpace(Convert.ToString(item["ESOQty"])) ? Convert.ToInt32(item["ESOQty"]) : 0,
                            PPQty = !string.IsNullOrWhiteSpace(Convert.ToString(item["PPQty"])) ? Convert.ToInt32(item["PPQty"]) : 0,
                            PPDate = !string.IsNullOrWhiteSpace(Convert.ToString(item["PPDate"])) ? Convert.ToDateTime(Convert.ToString(item["PPDate"])) : DateTime.Now,
                        });
                }
                int updatecounter = 0;

                foreach(var esq in omList)
                {

                    var order = _context.Orders.Where(x => x.SAPNo == esq.Order).FirstOrDefault();
                    if(order!=null)
                    {
                        var orderItem = _context.OrderItems.Where(x => x.OrderId == order.Id && x.Model==esq.Model).FirstOrDefault();
                        if(orderItem!= null)
                        {
                            Console.WriteLine("Order : " + esq.Order + " Model : " + esq.Model + " prev Qty : " + orderItem.ESOQty.ToString() + " new Qty : " + esq.ESOQty + " prev PPDate: " + orderItem.DatePP.ToString() + " new PPDate : " + esq.PPDate.ToString() + "\n");

                            orderItem.ESOQty = esq.ESOQty ?? 0 ;

                            if (esq.PPDate != null && esq.PPQty >= 0 && orderItem.ESODate <= esq.PPDate && orderItem.ESOQty >= esq.PPQty)
                            {
                                orderItem.DatePP = esq.PPDate;
                                orderItem.Feasible = esq.PPQty;
                                orderItem.Gap = orderItem.ESOQty - esq.PPQty;
                                
                                if (orderItem.OrderStatus == 1)
                                {
                                    if (orderItem.PlantId != null)
                                    {
                                        orderItem.OrderStatus = 2;
                                    }
                                }    

                            }

                             _context.SaveChanges();

                            updatecounter++;
                        }
                        else
                        {
                            NonUploaded.Rows.Add(esq.Order.ToString() +"-----" + esq.Model, "Order item not found");
                            continue;
                        }
                        
                    }
                    else
                    {
                        NonUploaded.Rows.Add(esq.Order, "Order not found");
                        continue;
                    }
                    

                }
                Console.WriteLine("\n\n---Total data found---: " + omList.Count);
                Console.WriteLine("\n\n---Total updated record---: " + updatecounter);

                Console.WriteLine("\n--------ESOQtyUpdate Lookup End------");
                Logger.LogFileWrite("\n--------ESOQtyUpdate Lookup End------");

                
                using (var writer = new StringWriter())
                {
                    NonUploaded.WriteXml(writer);
                    Logger.LogFileWrite("\n\n" + writer.ToString());
                    Console.WriteLine("\n"+ DateTime.Now.ToString()+"----Record update Successfully-------");
                    Logger.LogFileWrite(DateTime.Now.ToString() + " Record update Successfully..... \n\n");
                }
            }
            catch (Exception ex)
            {
                var message = Logger.CreateErrorMessage(ex);
                Logger.LogFileWrite("\nInternal server error \n" + message);
                Console.WriteLine("\n----Internal server error-------\n"+ message);

            }
            Console.ReadKey();
        }

        public static DataSet ExcelToDataSetForESOQty(string fileLocation, string fileExtension, string sheetname, ref string ExcelSheetName)
        {

            DataSet dsExcel = new DataSet();

            MemoryStream ms = new MemoryStream();
            FileStream fs = System.IO.File.OpenRead(fileLocation);
            ExcelPackage excelPackage = new ExcelPackage(fs);
            ExcelWorkbook excelWorkBook = excelPackage.Workbook;
            ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets[sheetname];
            ExcelSheetName = excelWorksheet.Name;

            DataTable dt = new DataTable();
            dt.Columns.Add("Order");
            dt.Columns.Add("Model");
            dt.Columns.Add("ESOQty");
            dt.Columns.Add("PPQty");
            dt.Columns.Add("PPDate");
            dsExcel.Tables.Add(dt);

            int col = excelWorksheet.Dimension.Columns;
            int rows = excelWorksheet.Dimension.Rows;
            bool colExist = false;
            for (int j = 0; j < 5; j++)
            {
                var colName = Convert.ToString(((object[,])((excelWorksheet.Cells).Value))[0, j]);
                for (int k = 0; k < 5; k++)
                {
                    if (dsExcel.Tables[0].Columns[k].ToString() == colName)
                    {
                        colExist = true;
                        break;
                    }
                    else
                        colExist = false;
                }
                if (colExist == false)
                {
                    excelPackage.Dispose();
                    return dsExcel;
                }
            }
            if (colExist == true)
            {
                for (int i = 1; i < rows; i++)
                {
                    int columns = 0;
                    DataRow dr = dt.NewRow();
                    while (columns <= 4)
                    {
                        dr[columns] = Convert.ToString(((object[,])((excelWorksheet.Cells).Value))[i, columns]);
                        columns += 1;
                    }
                    dt.Rows.Add(dr);
                }
            }


            excelPackage.Dispose();

            return dsExcel;
        }

        public class OrderModel
        {
            public string Order { get; set; }
            public string Model { get; set; }
            public int? ESOQty { get; set; }
            public int? PPQty { get; set; }
            public DateTime? PPDate { get; set; }
        }
    }
}
