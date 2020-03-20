using IntervalReport.BusinessLayer;
using IntervalReport.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Data;
using Newtonsoft.Json;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Threading.Tasks;

namespace IntervalReport.Controllers
{
    public class IntervalController : Controller
    {
        IHostingEnvironment _hostingEnv;
        /// <summary>
        /// this private filed used to hold the business logic data
        /// </summary>
        IIntervalBusinessLogic _intervalBusinessLogic;
        /// <summary>
        /// this constructor used for DI to business logic interface 
        /// </summary>
        /// <param name="intervalBusinessLogic"></param>
        public IntervalController(IIntervalBusinessLogic intervalBusinessLogic,  IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
            _intervalBusinessLogic = intervalBusinessLogic;
        }
        /// <summary>
        /// this method used   display thed data
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var response = GetIntervalData();
            if (response != null)
            {
               
                return View(response);
            }
            return View(new List<IntervalResponse>());
        }
        /// <summary>
        /// this method is used to redired error page
        /// </summary>
        /// <returns></returns>
        public IActionResult Error()    
        {
            return View();
        }
        /// <summary>
        /// this method will download the interval data in .xlsx format.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> DownloadReportFromTemplate()
        {
            try
            {
                string webRootPath = _hostingEnv.WebRootPath;
                string fileName = @"IntervalDataReportTemplate.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);
                FileInfo file = new FileInfo(Path.Combine(webRootPath, fileName));
                var memoryStream = new MemoryStream();
                var response = GetIntervalData();
                DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(response), (typeof(DataTable)));
                using (var fs = new FileStream(Path.Combine(webRootPath, fileName), FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet excelSheet = workbook.CreateSheet("Sheet1");
                    List<String> columns = new List<string>();
                    IRow row = excelSheet.CreateRow(0);
                    int columnIndex = 0;
                    foreach (DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);
                        row.CreateCell(columnIndex).SetCellValue(column.ColumnName);
                        columnIndex++;
                    }
                    int rowIndex = 1;
                    foreach (DataRow dsrow in table.Rows)
                    {
                        row = excelSheet.CreateRow(rowIndex);
                        int cellIndex = 0;
                        foreach (String col in columns)
                        {
                            row.CreateCell(cellIndex).SetCellValue(dsrow[col].ToString());
                            cellIndex++;
                        }

                        rowIndex++;
                    }
                    workbook.Write(fs);
                }
                using (var fileStream = new FileStream(Path.Combine(webRootPath, fileName), FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
             
            catch (Exception ex)
            {
                ErrorDetails errorDetails = new ErrorDetails();
                errorDetails.methodname += "DownloadReportFromTemplate ";
                errorDetails.errormessage = ex.Message;
                var trace = new System.Diagnostics.StackTrace(ex);
                errorDetails.stacktrace = Convert.ToString(trace);
                //Log the exception here
            }
            return null;
        }
        /// <summary>
        /// this private method used to retreive the interval data from businsess layer and display thed data
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IntervalResponse> GetIntervalData()
        {
            return _intervalBusinessLogic.GetIntervalDataByHourly();
        }
    }
}