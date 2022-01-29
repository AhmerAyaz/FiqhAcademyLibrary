using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class ImportExcelController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: ImportExcel
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ImportExcel importExcel)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("~/Content/Upload/" + importExcel.file.FileName);
                importExcel.file.SaveAs(path);

                string excelConnectionString = @"Provider='Microsoft.ACE.OLEDB.12.0';Data Source='" + path + "';Extended Properties='Excel 12.0 Xml;IMEX=1'";
                OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);

                //Sheet Name
                excelConnection.Open();
                string tableName = excelConnection.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();
                excelConnection.Close();
                //End

                OleDbCommand cmd = new OleDbCommand("Select * from [" + tableName + "]", excelConnection);

                excelConnection.Open();

                OleDbDataReader dReader;
                dReader = cmd.ExecuteReader();
                SqlBulkCopy sqlBulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

                //Give your Destination table name
                sqlBulk.DestinationTableName = "Books";

                //Mappings
                //sqlBulk.ColumnMappings.Add("Id", "Id");
                sqlBulk.ColumnMappings.Add("Name", "Name");
                sqlBulk.ColumnMappings.Add("Author", "Author");
                //sqlBulk.ColumnMappings.Add("Translator", "Translator");
                sqlBulk.ColumnMappings.Add("Topic", "GenreId");
                sqlBulk.ColumnMappings.Add("Publisher", "Publisher");
                //sqlBulk.ColumnMappings.Add("Set", "Set");
                //sqlBulk.ColumnMappings.Add("Number", "NumberInStock");
                //sqlBulk.ColumnMappings.Add("Edition", "Edition");
                //sqlBulk.ColumnMappings.Add("Date", "DateAdded");
                //sqlBulk.ColumnMappings.Add("Price", "Price");
                //sqlBulk.ColumnMappings.Add("Column", "Column");
                //sqlBulk.ColumnMappings.Add("Rack", "Rack");
                sqlBulk.ColumnMappings.Add("ReferenceNo", "ReferenceNo");
                sqlBulk.ColumnMappings.Add("SetNo", "SetNo");



                sqlBulk.WriteToServer(dReader);
                excelConnection.Close();

                ViewBag.Result = "Successfully Imported";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Reset()
        {
            Session["ExcelData"] = null;
            return RedirectToAction("Index");
        }
    }
}