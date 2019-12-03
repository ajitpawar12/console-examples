using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelRead
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(DateTime.Now.ToString()+"--------Scheduler started------");
                Logger.LogFileWrite("\n--------Defect Group Lookup Start------");
                musaEntities _context = new musaEntities();
                var dglist = new List<DefectGroup>();
                var dclist = new List<DefectCode>();

                _context.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");


                string ExcerlSheetName = "";
                //string DefectGroupFIlePath = @"E:\ePDI csv files\DefectGroup.xlsx";
                //string DefectCodeFIlePath = @"E:\ePDI csv files\DefectCode.xlsx";

                string DefectGroupFIlePath = @"F:\DefectFiles\DefectGroup.xlsx";
                string DefectCodeFIlePath = @"F:\DefectFiles\DefectCode.xlsx";

                DataTable NonUploaded = new DataTable();

                NonUploaded.TableName = "NonuploadedData";
                NonUploaded.Columns.Add("UniqueFeild");
                NonUploaded.Columns.Add("Reason");

                //Defect Group lookup

                Console.WriteLine("\n--------Defect Group Lookup Start------");
                Logger.LogFileWrite("\n--------Defect Group Lookup Start------");

                DataSet dcgExcel = ExcelToDataSetForDefectCodeGroup(DefectGroupFIlePath, ".xlsx", "DefectGroup", ref ExcerlSheetName);
                var contdg = dcgExcel.Tables[0].Rows.Count;

                foreach (DataRow item in dcgExcel.Tables[0].Rows)
                {
                    dglist.Add(
                        new DefectGroup
                        {
                            Name = Convert.ToString(item["Inspection Criteria"]),
                            Code = Convert.ToString(item["Defect Code Group"])
                        });
                }
                Console.WriteLine("\n--------Defect Group Lookup End------");
                Logger.LogFileWrite("\n--------Defect Group Lookup End------");

                //Defect Code lookup

                Console.WriteLine("\n--------Defect Code Lookup Start------");
                Logger.LogFileWrite("\n--------Defect Code Lookup Start------");
                DataSet dcExcel = ExcelToDataSetForDefectCode(DefectCodeFIlePath, ".xlsx", "DefectCode", ref ExcerlSheetName);
                var contdc = dcExcel.Tables[0].Rows.Count;

                foreach (DataRow item in dcExcel.Tables[0].Rows)
                {

                    dclist.Add(
                       new DefectCode
                       {
                           Name = Convert.ToString(item["Defect Title"]),
                           Code = Convert.ToString(item["Defect Code"])
                       });

                }
                Console.WriteLine("\n--------Defect Group Lookup End------");
                Logger.LogFileWrite("\n--------Defect Group Lookup End------");

                Console.WriteLine("\n----updating recoed start-------");
                Logger.LogFileWrite("\n----updating recoed start-------");

                var coderesult = _context.defects.Where(d => d.DefectCode == null || d.DefectCode == "" || d.DefectGroup == null || d.DefectGroup == "").Distinct().ToList();
                int counter = 0;
                if (coderesult.Count > 0)
                {

                    Console.WriteLine("\n----Total empty record found = " + coderesult.Count.ToString());
                    Logger.LogFileWrite("\n----Total empty record found = " + coderesult.Count.ToString());
                   

                    foreach (var cr in coderesult)
                    {

                        if (cr != null)
                        {
                            var defect = _context.defects.FirstOrDefault(c => c.defect_id == cr.defect_id);

                            if (defect != null)
                            {

                                if (string.IsNullOrWhiteSpace(defect.DefectGroup))
                                {
                                    var inscritera = _context.inspection_parameter_master.FirstOrDefault(i => i.inspection_parameter_id == defect.FKinspection_parameter_id);

                                    if (!string.IsNullOrWhiteSpace(inscritera.inspection_parameter_name))
                                    {
                                        var defectgroup = dglist.FirstOrDefault(x => x.Name.Trim() == inscritera.inspection_parameter_name.Trim());

                                        if (defectgroup != null)
                                        {
                                            defect.DefectGroup = defectgroup.Code;
                                        }
                                        else
                                        {
                                            NonUploaded.Rows.Add(inscritera.inspection_parameter_name, "inspection parameter not found in Defect Group List");
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        NonUploaded.Rows.Add(defect.FKinspection_parameter_id.ToString(), "inspection parameter not found");
                                        continue;
                                    }
                                }
                                if (string.IsNullOrWhiteSpace(defect.DefectCode))
                                {
                                    var deftitle = defect.title;

                                    if (!string.IsNullOrWhiteSpace(deftitle))
                                    {
                                        var defectcode = dclist.FirstOrDefault(x => x.Name.Trim() == deftitle.Trim());

                                        if (defectcode != null)
                                        {
                                            defect.DefectCode = defectcode.Code;
                                        }
                                        else
                                        {
                                            NonUploaded.Rows.Add(defect.title.ToString(), "defect title not found in Defect Code List");
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        NonUploaded.Rows.Add(defect.title.ToString(), "defect title not found in Defect");
                                        continue;
                                    }
                                    _context.SaveChanges();
                                    counter++;
                                }
                                
                            }
                            else
                            {
                                NonUploaded.Rows.Add(cr.defect_id.ToString(), "defect not found");
                                continue;
                            }

                        }
                        else
                        {
                            NonUploaded.Rows.Add(string.Empty, "coderesult not found");
                            continue;
                        }
                    }
                    Console.WriteLine("\n----updating record end-------");
                    Logger.LogFileWrite("\n----updating record end-------");

                    Console.WriteLine("\n----Total update record = " + counter);                   
                }
                else
                {
                    NonUploaded.Rows.Add(string.Empty, "Empty Data not found");
                    
                }
                using (var writer = new StringWriter())
                {
                    NonUploaded.WriteXml(writer);
                    Logger.LogFileWrite("\n\n" + writer.ToString());
                    Console.WriteLine("\n----Record update Successfully-------");
                    Logger.LogFileWrite(DateTime.Now.ToString() + " Record update Successfully..... \n\n");
                }
            }
            catch (Exception ex)
            {
                var message = Logger.CreateErrorMessage(ex);
                Logger.LogFileWrite("\nInternal server error \n" + message);
                Console.WriteLine("\n----Internal server error-------");

            }
            Console.ReadKey();
        }

        public static DataSet ExcelToDataSetForDefectCodeGroup(string fileLocation, string fileExtension, string sheetname, ref string ExcelSheetName)
        {
            DataSet dsExcel = new DataSet();

            MemoryStream ms = new MemoryStream();
            FileStream fs = System.IO.File.OpenRead(fileLocation);
            ExcelPackage excelPackage = new ExcelPackage(fs);
            ExcelWorkbook excelWorkBook = excelPackage.Workbook;
            ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets[sheetname];
            ExcelSheetName = excelWorksheet.Name;

            DataTable dt = new DataTable();
            dt.Columns.Add("Inspection Criteria");
            dt.Columns.Add("Defect Code Group");
            dsExcel.Tables.Add(dt);

            int col = excelWorksheet.Dimension.Columns;
            int rows = excelWorksheet.Dimension.Rows;
            bool colExist = false;
            for (int j = 0; j < 2; j++)
            {
                var colName = Convert.ToString(((object[,])((excelWorksheet.Cells).Value))[0, j]);
                for (int k = 0; k < 2; k++)
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
                    while (columns <= 2)
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

        public static DataSet ExcelToDataSetForDefectCode(string fileLocation, string fileExtension, string sheetname, ref string ExcelSheetName)
        {
            DataSet dsExcel = new DataSet();

            MemoryStream ms = new MemoryStream();
            FileStream fs = System.IO.File.OpenRead(fileLocation);
            ExcelPackage excelPackage = new ExcelPackage(fs);
            ExcelWorkbook excelWorkBook = excelPackage.Workbook;
            ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets[sheetname];
            ExcelSheetName = excelWorksheet.Name;

            DataTable dt = new DataTable();
            dt.Columns.Add("Defect Title");
            dt.Columns.Add("Defect Code");
            dsExcel.Tables.Add(dt);

            int col = excelWorksheet.Dimension.Columns;
            int rows = excelWorksheet.Dimension.Rows;
            bool colExist = false;
            for (int j = 0; j < 2; j++)
            {
                var colName = Convert.ToString(((object[,])((excelWorksheet.Cells).Value))[0, j]);
                for (int k = 0; k < 2; k++)
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
                    while (columns <= 1)
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


        public class DefectGroup
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }

        public class DefectCode
        {
            public string Name { get; set; }
            public string Code { get; set; }
        }
    }
}
