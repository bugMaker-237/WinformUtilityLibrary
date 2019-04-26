using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
//using Excel = Microsoft.Office.Interop.Excel;

namespace WinFormUtilityLibrary.DataUtilities
{
    public class SendToExcel
    {
        private static string GetConnectionString(string FileName)
        {
            try
            {
                Dictionary<string, string> props = new Dictionary<string, string>();

                // XLSX - Excel 2007, 2010, 2012, 2013
                object misvalue = System.Reflection.Missing.Value;
                props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
                props["Extended Properties"] = "Excel 12.0 XML";
                //Excel.Application app = new Excel.Application();
                //Excel.Workbook xlwb = app.Workbooks.Add();
                //xlwb.SaveAs(FileName, Excel.XlFileFormat.xlWorkbookNormal,
                //    misvalue, misvalue, misvalue, misvalue, Excel.XlSaveAsAccessMode.xlExclusive);
                ////xlws.Range.Cells.FormatConditions.
                //props["Data Source"] = xlwb.FullName;
                //xlwb.Close(true);
                //app.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(xlwb);
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(app);

                // XLS - Excel 2003 and Older
                //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
                //props["Extended Properties"] = "Excel 8.0";
                //props["Data Source"] = "C:\\MyExcel.xls";

                StringBuilder sb = new StringBuilder();

                //foreach (KeyValuePair<string, string> prop in props)
                //{
                //    sb.Append(prop.Key);
                //    sb.Append('=');
                //    sb.Append(prop.Value);
                //    sb.Append(';');
                //}

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void WriteExcelFile(string fileName, string[] columns, string query = null)
        {
            System.Windows.Forms.Application.UseWaitCursor = true;
            string connectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "CREATE TABLE [MainSheet] (";
                foreach (string item in columns)
                {
                    if(columns.Last() == item)
                    {
                        cmd.CommandText += item + ");";
                    }
                    else
                    {
                        cmd.CommandText += item + ",";
                    }
                }
                cmd.ExecuteNonQuery();
                cmd.CommandText = query;
                if (query != null)
                {
                    cmd.ExecuteNonQuery();
                }

                conn.Close();

            }
        }
    }
}
