using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Recon_proto.Controllers.ReconController;
using System.Data;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.CommonController;
using ClosedXML.Excel;
using System.IO;
using System.Dynamic;
using System.Text.RegularExpressions;
using Recon_proto.Models;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Bibliography;
using Irony.Parsing;
using System.Net;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Configuration;
using Recon_proto.Controllers;
using System.Data.SqlTypes;
using static Recon_proto.Controllers.UtilityController;
using System.Net.Mime;
using DocumentFormat.OpenXml.ExtendedProperties;

namespace Recon_proto.Controllers
{
    public class ReportsController : Controller
    {
        private IConfiguration _configuration;
        public ReportsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult WithinAccounts()
        {
            return View();
        }
        public IActionResult BetweenAccounts()
        {
            return View();
        }
        public IActionResult KnockoffMIS()
        {
            return View();
        }
        public IActionResult ReportGeneration()
        {
            return View();
        }
        public IActionResult PaginationReport()
        {
            return View();
        }

        public IActionResult ReconHistory()
        {
            return View();
        }

        public IActionResult ReportTemplete()
        {
            return View();
        }
        public IActionResult ReportTempletedetail()
        {
            return View();
        }

        public IActionResult ReportGeneration_newlist()
        {
            return View();
        }

        public IActionResult ReportGeneration_newdetail()
        {
            return View();
        }

        #region Reportlist

        public JsonResult ReportList()

        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            List<getreportlist> objcat_lst = new List<getreportlist>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
                    try
                    {
                        var response = client.PostAsync("getreportlist", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result1 = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result1.Rows.Count; i++)
                    {
                        getreportlist objcat = new getreportlist();
                        objcat.report_gid = Convert.ToInt16(result1.Rows[i]["report_gid"]);
                        objcat.report_code = result1.Rows[i]["report_code"].ToString();
                        objcat.report_desc = result1.Rows[i]["report_desc"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "ReportList");
                return Json(ex.Message);
            }
        }


        public class getreportlist
        {
            public Int32? report_gid { get; set; }
            public String? report_code { get; set; }
            public String? report_desc { get; set; }
        }
        #endregion

        #region getreportparam
        public JsonResult getreportparam([FromBody] Reconparamlistmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            List<getreportparamlist> objcat_lst = new List<getreportparamlist>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try
                    {
                     var response = client.PostAsync("getreportparamlist", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result1 = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result1.Rows.Count; i++)
                    {
                        getreportparamlist objcat = new getreportparamlist();
                        //objcat.report_code = Convert.ToInt16(result1.Rows[i]["report_code"]);
                        objcat.reportparam_code = result1.Rows[i]["reportparam_code"].ToString();
                        objcat.reportparam_value = result1.Rows[i]["reportparam_value"].ToString();
                        objcat.report_code = result1.Rows[i]["report_code"].ToString();

                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getreportparam");
                return Json(ex.Message);
            }
        }

        public class getreportparamlist
        {
            public String? report_code { get; set; }
            public String? reportparam_code { get; set; }
            public String? reportparam_value { get; set; }

        }

        public class Reconparamlistmodel
        {
            public String? in_report_code { get; set; }
        }
        #endregion

        #region QCDMaster

        [HttpPost]
        public JsonResult Qcdmaster([FromBody] Qcdgridread context)
        {
            DataTable result = new DataTable();
            List<mainQCDMaster> objcat_lst = new List<mainQCDMaster>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44348/api/Qcdmaster/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try
                    {
                        var response = client.PostAsync("QcdMasterGridRead", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        mainQCDMaster objcat = new mainQCDMaster();
                        objcat.master_gid = Convert.ToInt32(result.Rows[i]["master_gid"]);
                        objcat.masterCode = result.Rows[i]["master_code"].ToString();
                        objcat.masterName = result.Rows[i]["master_name"].ToString();
                        objcat.masterShortCode = result.Rows[i]["master_short_code"].ToString();
                        objcat.masterSyscode = result.Rows[i]["master_syscode"].ToString();
                        objcat.ParentMasterSyscode = result.Rows[i]["parent_master_syscode"].ToString();
                        objcat.mastermutiplename = result.Rows[i]["master_multiple_name"].ToString();
                        objcat.active_status = result.Rows[i]["active_status"].ToString();
                        objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
                        objcat_lst.Add(objcat);

                        ViewBag.constraints = objcat_lst;


                    }
                    return Json(ViewBag.constraints);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Qcdmaster");
                return Json(ex.Message);
            }
        }
        #endregion
        #region generateReport
        [HttpPost]
        public JsonResult generateReport([FromBody] generateReportmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("generatereport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                    //DataTable newdt = new DataTable();
                    //DownloadExcel();
                    //return Json("");

                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "generateReport");
                return Json(ex.Message);
            }
        }


        public ActionResult generateReportXLSX(string in_recon_code, string in_report_code, string in_report_param, string reportcondition, string in_ip_addr, Boolean in_outputfile_flag, string in_user_code)
        {

            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            DataTable Table1 = new DataTable();
            string post_data = "";
            string filename = in_recon_code + ".xlsx";
            try
            {
                using (var client = new HttpClient())
                {
                    generateReportmodel report = new generateReportmodel();
                    report.in_recon_code = in_recon_code;
                    report.in_report_code = in_report_code;
                    report.in_report_param = in_report_param;
                    report.reportcondition = reportcondition;
                    report.in_ip_addr = in_ip_addr;
                    report.in_outputfile_flag = in_outputfile_flag;
                    report.in_user_code = in_user_code;
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(report), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("generatereport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    Table1 = JsonConvert.DeserializeObject<DataTable>(d2);
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var worksheet = wb.Worksheets.Add(Table1, "Sheet1");
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                        }
                    }
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

         public class generateReportmodel
        {
            public String? in_recon_code { get; set; }
            public String? in_report_code { get; set; }
            public String? in_report_param { get; set; }
            public String? reportcondition { get; set; }
            public String? in_ip_addr { get; set; }
            public Boolean? in_outputfile_flag { get; set; }
            public string? in_user_code { get; set; }
        }

        #endregion

        #region reconwithinacc
        [HttpPost]
        public JsonResult reconwithinacc([FromBody] reconwithinaccmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reconwithinacc", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "reconwithinacc");
                return Json(ex.Message);
            }
        }

        public class reconwithinaccmodel
        {
            public String? in_recon_code { get; set; }
            public String? in_tran_date { get; set; }

        }
        #endregion

        #region reconbetweenacc
        [HttpPost]
        public JsonResult reconbetweenacc([FromBody] reconbetweenaccmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reconbetweenacc", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "reconbetweenacc");
                return Json(ex.Message);
            }
        }

        public class reconbetweenaccmodel
        {
            public String? in_recon_code { get; set; }
            public String? in_tran_date { get; set; }

        }
        #endregion
        #region version history
        [HttpPost]
        public JsonResult Reconversionhistoryfetch([FromBody] ReconversionListfetchmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            string post_data = "";
            using (var client = new HttpClient())
            {
                string Urlcon = "ReconVersion/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = Timeout.InfiniteTimeSpan;
                client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                try { 
                var response = client.PostAsync("ReconVersionhistory", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                string d2 = JsonConvert.DeserializeObject<string>(post_data);
                return Json(d2);
                }
                catch (HttpRequestException ex)
                {
                    return Json(ex);
                }
            }
        }
        public class ReconversionListfetchmodel
        {
            public string in_recon_code { get; set; }
            public string in_version_code { get; set; }
        }
        #endregion

        #region

        public ActionResult kendogrid_download(string in_tran_date, string in_recon_code, string in_recon_name)
        {

            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            DataTable Table1 = new DataTable();
            string post_data = "";
            string filename = "Recon-between A/cs.xlsx";
            try
            {
                using (var client = new HttpClient())
                {
                    reconbetweenaccmodel report = new reconbetweenaccmodel();
                    report.in_tran_date = in_tran_date;
                    report.in_recon_code = in_recon_code;
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(report), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reconbetweenacc", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    Table1 = JsonConvert.DeserializeObject<DataTable>(d2);
                    Table1.Columns.RemoveAt(0);
                    Table1.Columns[0].ColumnName = "Particulars";
                    Table1.Columns[1].ColumnName = "Amount";
                    Table1.Columns[2].ColumnName = "Account Mode";
                    Table1.Columns[3].ColumnName = "Balance Amount";
                    using (XLWorkbook wb = new XLWorkbook())
                    {

                        var ws = wb.AddWorksheet(in_recon_name);
                        ws.Cell("A4").InsertTable(Table1);
                        wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                        wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                        ws.Range("A4:D4").Style.Font.Bold = true;
                        ws.Range("A4:D4").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        ws.Range("A17:D17").Style.Font.Bold = true;
                        ws.Range("A17:D17").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                        ws.Cell("A11").Style.Font.Bold = true; ws.Cell("A13").Style.Font.Bold = true;
                        ws.Range("A19:D19").Style.Font.Bold = true;
                        ws.Range("A20:D20").Style.Font.Bold = true;
                        ws.Range("A21:D21").Style.Font.Bold = true;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                        }
                    }
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        #endregion

        #region monthendreport
        //monthendreport
        public ActionResult monthendreport(string in_tran_date, string in_recon_code, string in_recon_name)
        {
            string post_data = "";
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            try
            {
                //List<TransactionRpt_model> objcat_3st = new List<TransactionRpt_model>();
                DataSet result2 = new DataSet();
                DataSet dt = new DataSet();
                DataTable Table = new DataTable();
                DataTable Table1 = new DataTable();
                DataTable Table2 = new DataTable();
                DataTable Table3 = new DataTable();
                DataTable Table4 = new DataTable();
                DataTable Table6 = new DataTable();
                DataTable Table7 = new DataTable();

                long row = 0;
                long col = 0;
                string cellTxt = "";
                string cellTxt1 = "";
                string formulaTxt = "";
                string calcCellTxt = "";
                var recon_name1 = "";
                if (DateTime.TryParse(in_tran_date, out DateTime parsedDate))
                {
                    monthendreportModel contect = new monthendreportModel();
                    contect.in_tran_date = in_tran_date;
                    contect.in_recon_code = in_recon_code;
                    using (var client = new HttpClient())
                    {
                        string Urlcon = "Report/";
                        client.BaseAddress = new Uri(urlstring + Urlcon);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.Timeout = Timeout.InfiniteTimeSpan;
                        client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                        client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                        client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                        client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(contect), UTF8Encoding.UTF8, "application/json");
                        try { 
                        var response = client.PostAsync("MonthendReport", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        string d2 = JsonConvert.DeserializeObject<string>(post_data);
                        result2 = (DataSet)JsonConvert.DeserializeObject(d2, result2.GetType());
                        Table = result2.Tables[0].Copy();
                        Table1 = result2.Tables[1].Copy();
                        Table2 = result2.Tables[2].Copy();
                        Table3 = result2.Tables[3].Copy();
                        Table4 = result2.Tables[4].Copy();
                        Table6 = result2.Tables[6].Copy();
                        Table7 = result2.Tables[7].Copy();

                        var entity_name = Table.Rows[0]["entity"].ToString();
                        // var recon_name = Table.Rows[0]["recon_name"].ToString();
                        var recon_name = in_recon_name;
                        int i = recon_name.Length;
                        if (i >= 30)
                        {
                            recon_name1 = recon_name.Substring(0, 30);
                        }
                        else
                        {
                            recon_name1 = recon_name;
                        }
                        var acc_no1 = Table.Rows[0]["source_dataset_name"].ToString();
                        var acc_no2 = Table.Rows[0]["target_dataset_name"].ToString();
                        string Acc1date = Table.Rows[0]["source_bal_date"].ToString();
                        string Acc2date = Table.Rows[0]["target_bal_date"].ToString();
                        Decimal acc1Bal_amount = Convert.ToDecimal(Table.Rows[0]["source_bal_value"].ToString());
                        Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["target_bal_value"].ToString());
                        Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["source_dr_total"].ToString());
                        Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["target_dr_total"].ToString());
                        Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["source_cr_total"].ToString());
                        Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["target_cr_total"].ToString());
						Decimal roundoff_count = Convert.ToDecimal(Table.Rows[0]["roundoff_count"].ToString());
						Decimal roundoff_total = Convert.ToDecimal(Table.Rows[0]["roundoff_count"].ToString());

						dt = result2.Copy();
                        dt.Tables.Remove("Table");

                        string fileName = acc_no1 + ".xlsx";

                        using (XLWorkbook wb = new XLWorkbook())// wb.Worksheets.Add(dt);
                        {
                            ///Setting Table Data static & Dynamic;
                            var sheetname = recon_name1.Replace(" ", "_");
                            var ws = wb.AddWorksheet(sheetname);
                            ws.FirstCell().SetValue(entity_name); ws.Cell("A1").Style.Font.Bold = true;
                            ws.Cell("A1").Style.Font.Underline.ToString(); ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                            ws.Range("A1:D1").Row(1).Merge();

                            ws.Cell("E1").SetValue("GL CODE").SetActive(); ws.Cell("E1").Style.Font.Bold = true;
                            ws.Cell("F1").SetValue(acc_no2).SetActive(); ws.Cell("F1").Style.Font.Bold = true;

                            ws.FirstCell().CellBelow().SetValue(recon_name);
                            ws.Cell("A2").Style.Font.Bold = true;
                            ws.Range("A2:F2").Row(1).Merge();

                            ws.Cell("A3").SetValue("CC A/c No.").SetActive(); ws.Cell("A3").Style.Font.Bold = true;
                            ws.Range("A3:B3").Row(1).Merge();

                            ws.Cell("C3").SetValue(acc_no1).SetActive(); ws.Cell("C3").Style.Font.Bold = true;
                            ws.Range("C3:F3").Row(1).Merge();

                            ws.Cell("A4").SetValue("Bank Reconciliation Statement as on date " + Acc1date + "").SetActive();
                            ws.Cell("A4").Style.Font.Bold = true;
                            ws.Range("A4:F4").Row(1).Merge();

                            ws.Cell("A6").SetValue("S U M M A R Y").SetActive(); ws.Cell("A6").Style.Font.Bold = true;
                            ws.Cell("A6").Style.Font.Underline = XLFontUnderlineValues.Single;
                            ws.Cell("A6").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            ws.Range("A6:F6").Row(1).Merge();

                            ws.Cell("A7").SetValue("Closing balance  as per our bank book as on " + Acc2date + "").SetActive();
                            ws.Cell("A7").Style.Font.Bold = true;
                            ws.Range("A7:E7").Row(1).Merge();
                            ws.Cell("I7").SetValue(acc2Bal_amount).SetActive(); ws.Cell("I7").Style.NumberFormat.Format = "###0.00";

                            ws.Cell("A9").SetValue("Less: Cheques deposited but not credited in bank passbook").SetActive();
                            ws.Range("A9:E9").Row(1).Merge();
                            ws.Cell("I9").SetValue(accno2_drtotal).SetActive(); ws.Cell("FI9").Style.Font.Bold = true; ws.Cell("I9").Style.NumberFormat.Format = "###0.00";

                            ws.Cell("A11").SetValue("Add: Cheques issued but not presented in the bank").SetActive();
                            ws.Range("A11:E11").Row(1).Merge();
                            ws.Cell("I11").SetValue(accno2_crtotal).SetActive(); ws.Cell("I11").Style.NumberFormat.Format = "###0.00";

                            ws.Cell("A13").SetValue("Less: Amount debited in bank but not accounted").SetActive();
                            ws.Range("A13:E13").Row(1).Merge();
                            ws.Cell("I13").SetValue(accno1_drtotal).SetActive(); ws.Cell("I13").Style.NumberFormat.Format = "###0.00";

                            ws.Cell("A15").SetValue("Add: Amount credited in bank but not accounted").SetActive();
                            ws.Range("A15:E15").Row(1).Merge();
                            ws.Cell("I15").SetValue(accno1_crtotal).SetActive(); ws.Cell("I15").Style.NumberFormat.Format = "###0.00";

                                if (roundoff_count > 0)
                                {
                                    ws.Cell("A16").SetValue("Roundoff Difference (" + roundoff_count + ")").SetActive();
								ws.Range("A16:E16").Row(1).Merge(); 
                                ws.Cell("I16").SetValue(roundoff_total).SetActive(); ws.Cell("I16").Style.NumberFormat.Format = "###0.00";
                                }

                            ws.Cell("A17").SetValue("Closing balance as per bank passbook").SetActive();
                            ws.Cell("A17").Style.Font.Bold = true;
                            ws.Range("A17:E17").Row(1).Merge();

                            ws.Cell("I17").SetValue(acc1Bal_amount).SetActive(); ws.Cell("I17").Style.Font.Bold = true; ws.Cell("I17").Style.NumberFormat.Format = "###0.00";
                            ws.Cell("I17").Style.Font.Bold = true; //ws.Cell("F17").Style.Font.Underline = XLFontUnderlineValues.Double;
                            ws.Cell("I17").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            ws.Cell("I17").Style.Border.TopBorderColor = XLColor.Black;
                            ws.Cell("I17").Style.Border.BottomBorder = XLBorderStyleValues.Double;
                            ws.Cell("I17").Style.Border.BottomBorderColor = XLColor.Black;

                            ws.Cell("A19").SetValue("Less: Cheques deposited but not credited").SetActive(); ws.Cell("A19").Style.Font.Bold = true;
                            ws.Cell("A19").Style.Font.Underline = XLFontUnderlineValues.Single;
                            ws.Range("A19:F19").Row(1).Merge();

                            //Inserting  Table1 Data
                            wb.Worksheet(1).Cell("A20").InsertTable(Table1);
                            ws.Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                            ws.Table(0).Theme = XLTableTheme.None;
                            ws.Range("A20:H20").Style.Font.Bold = true;
                            ws.Range("A20:H20").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                            //Inserting Table1 Total
                            formulaTxt = "=sum(D21:D" + (20 + Table1.Rows.Count).ToString() + ")";
                            row = 20 + Table1.Rows.Count + 1;
                            cellTxt = "I" + row.ToString() + "";
                            ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;

                            var cellFormulaTableSum = ws.Cell(cellTxt);

                            cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                            //Inserting calculation
                            calcCellTxt = "I7";
                            formulaTxt = "=+(" + calcCellTxt + "*-1)-" + cellTxt;

                            row = row + 1;
                            cellTxt = "I" + row.ToString() + "";
                            calcCellTxt = cellTxt;

                            var cellFormulaCalc = ws.Cell(cellTxt);
                            cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                            //Inserting  Table2 Data
                            row = row + 2;
                            cellTxt = "A" + row.ToString() + "";

                            ws.Cell(cellTxt).SetValue("Add: Cheques issued but not presented").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                            ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                            cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                            ws.Range(cellTxt).Row(1).Merge();

                            row = row + 1;
                            cellTxt = "A" + row.ToString() + "";

                            wb.Worksheet(1).Cell(cellTxt).InsertTable(Table2);
                            ws.Table(1).ShowAutoFilter = false;// Disable AutoFilter.
                            ws.Table(1).Theme = XLTableTheme.None;
                            cellTxt1 = "A" + row.ToString() + ":H" + row.ToString();
                            ws.Range(cellTxt1).Style.Font.Bold = true;
                            ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);


                            //Inserting  Table2 Total
                            formulaTxt = "=sum(D" + (row + 1).ToString() + ":D" + (row + Table2.Rows.Count).ToString() + ")";
                            row = row + Table2.Rows.Count + 1;
                            cellTxt = "I" + row.ToString() + "";
                            ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                            cellFormulaTableSum = ws.Cell(cellTxt);
                            cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                            formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                            row = row + 1;
                            cellTxt = "I" + row.ToString() + "";
                            calcCellTxt = cellTxt;

                            cellFormulaCalc = ws.Cell(cellTxt);
                            cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                            row = row + 2;
                            cellTxt = "A" + row.ToString() + "";

                            ws.Cell(cellTxt).SetValue("Less: Amount  debited  in pass Book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                            ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;
                            cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                            ws.Range(cellTxt).Row(1).Merge();

                            //Inserting  Table3 Data
                            row = row + 1;
                            cellTxt = "A" + row.ToString() + "";

                            wb.Worksheet(1).Cell(cellTxt).InsertTable(Table3);
                            ws.Table(2).ShowAutoFilter = false;// Disable AutoFilter.
                            ws.Table(2).Theme = XLTableTheme.None;
                            cellTxt1 = "A" + row.ToString() + ":H" + row.ToString();
                            ws.Range(cellTxt1).Style.Font.Bold = true;
                            ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                            //Inserting  Table3 Total
                            formulaTxt = "=sum(D" + (row + 1).ToString() + ":D" + (row + Table3.Rows.Count).ToString() + ")";
                            row = row + Table3.Rows.Count + 1;
                            cellTxt = "I" + row.ToString() + "";
                            ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                            cellFormulaTableSum = ws.Cell(cellTxt);
                            cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                            formulaTxt = "=+" + calcCellTxt + "-" + cellTxt;

                            row = row + 1;
                            cellTxt = "I" + row.ToString() + "";
                            calcCellTxt = cellTxt;

                            cellFormulaCalc = ws.Cell(cellTxt);
                            cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";

                            row = row + 2;
                            cellTxt = "A" + row.ToString() + "";

                            ws.Cell(cellTxt).SetValue("Add: Amount credited in pass Book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;
                            ws.Cell(cellTxt).Style.Font.Underline = XLFontUnderlineValues.Single;

                            cellTxt = "A" + row.ToString() + ":F" + row.ToString();
                            ws.Range(cellTxt).Row(1).Merge();

                            row = row + 1;
                            cellTxt = "A" + row.ToString() + "";

                            wb.Worksheet(1).Cell(cellTxt).InsertTable(Table4);
                            ws.Table(3).ShowAutoFilter = false;// Disable AutoFilter.
                            ws.Table(3).Theme = XLTableTheme.None;
                            cellTxt1 = "A" + row.ToString() + ":H" + row.ToString();
                            ws.Range(cellTxt1).Style.Font.Bold = true;
                            ws.Range(cellTxt1).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                            //Inserting  Table4 Total
                            formulaTxt = "=sum(D" + (row + 1).ToString() + ":D" + (row + Table4.Rows.Count).ToString() + ")";
                            row = row + Table4.Rows.Count + 1;
                            cellTxt = "I" + row.ToString() + "";
                            ws.Cell(cellTxt).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            ws.Cell(cellTxt).Style.Border.BottomBorderColor = XLColor.Black;
                            cellFormulaTableSum = ws.Cell(cellTxt);
                            cellFormulaTableSum.FormulaA1 = formulaTxt; cellFormulaTableSum.Style.NumberFormat.Format = "###0.00";

                            formulaTxt = "=+" + calcCellTxt + "+" + cellTxt;

                            row = row + 1;
                            cellTxt = "I" + row.ToString() + "";
                            calcCellTxt = cellTxt;

                            cellFormulaCalc = ws.Cell(cellTxt);
                            cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                            cellFormulaCalc.Style.Font.Bold = true;
                            // cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            // cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                            //cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                            //cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                            //cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            //Total Balance & Difference
                            cellTxt = "B" + row.ToString() + "";
                            ws.Cell(cellTxt).SetValue("Balance as per bank pass book").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                            row = row + 2;
                            cellTxt = "B" + row.ToString() + "";
                            ws.Cell(cellTxt).SetValue("Difference").SetActive(); ws.Cell(cellTxt).Style.Font.Bold = true;

                            formulaTxt = "=+" + calcCellTxt + "-I17";

                            cellTxt = "I" + row.ToString() + "";

                            cellFormulaCalc = ws.Cell(cellTxt);
                            cellFormulaCalc.FormulaA1 = formulaTxt; cellFormulaCalc.Style.NumberFormat.Format = "###0.00";
                            cellFormulaCalc.Style.Font.Bold = true;
                            cellFormulaCalc.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                            cellFormulaCalc.Style.Border.TopBorderColor = XLColor.Black;
                            cellFormulaCalc.Style.Border.BottomBorder = XLBorderStyleValues.Double;
                            cellFormulaCalc.Style.Border.BottomBorderColor = XLColor.Black;
                            cellFormulaCalc.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                            ws.Columns("A").AdjustToContents();
                            ws.Columns("F").AdjustToContents();

                            //Insert Transaction
                            ws = wb.AddWorksheet("Transaction");
                            //ws.Cell("A1").InsertTable(Table6).Theme = XLTableTheme.None;
                            // ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                            foreach (var table in ws.Tables)
                            {
                                table.ShowAutoFilter = false;
                            }
                            ws.Cell("A1").Style.Font.Bold = true;
                            ws.Range("A1:Z1").Style.Font.Bold = true;
                            ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                            // ws.Table(6).Theme = XLTableTheme.None;

                            if (Table7.Rows.Count > 0)
                            {
                                //Insert Supporting File
                                ws = wb.AddWorksheet("SupportingFile");
                                ws.Cell("A1").InsertTable(Table7).Theme = XLTableTheme.None;
                                //ws.Tables.ForEach(x => x.ShowAutoFilter = false);
                                foreach (var table in ws.Tables)
                                {
                                    table.ShowAutoFilter = false;
                                }
                                ws.Range("A1:Z1").Style.Font.Bold = true;
                                ws.Range("A1:Z1").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                                // ws.Table(7).Theme = XLTableTheme.None;
                            }

                            using (MemoryStream stream = new MemoryStream())
                            {
                                wb.SaveAs(stream);
                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                            }

                        }
                        }
                        catch (HttpRequestException ex)
                        {
                            return Json(ex);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public class monthendreportModel
        {
            public string in_recon_code { get; set; }
            public string in_tran_date { get; set; }

        }

        #endregion

        #region reportTemplete
        [HttpPost]
        public JsonResult reportTempletelist([FromBody] getreportlistModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("getReportTemplateList", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getReportTemplateList");
                return Json(ex.Message);
            }
        }

        public class getreportlistModel
        {
            public string? in_recon_code { get; set; }
            public Boolean? in_custom_flag { get; set; }

        }

        #endregion
        //reporttemplate
        [HttpPost]
        public JsonResult reporttemplate([FromBody] reporttemplateModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reportTemplate", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "reportTemplate");
                return Json(ex.Message);
            }
        }
        public class reporttemplateModel
        {
            public Int32? in_reporttemplate_gid { get; set; }
            public String? in_reporttemplate_code { get; set; }
            public String? in_reporttemplate_name { get; set; }
            public String? in_report_code { get; set; }

            public String? in_sortby_code { get; set; }
            public String? in_recon_code { get; set; }
            public String? in_action { get; set; }
            public String? in_active_status { get; set; }

            public String? in_system_flag { get; set; }

        }
        //Report Templete fetch
        [HttpPost]
        public JsonResult reporttemplatefetch([FromBody] reporttemplatefetchModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("fetchReportTemplate", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "fetchReportTemplate");
                return Json(ex.Message);
            }
        }
        public class reporttemplatefetchModel
        {
            public String? in_reporttemplate_code { get; set; }
            public string? in_recon_code { get; set; }
            public string? in_report_code { get; set; }

        }
        #region reportfilter
        public JsonResult reporttemplatefilter([FromBody] reporttemplatefilterModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try
                    {
                        var response = client.PostAsync("reporttemplatefilter", content).Result;
                        response.EnsureSuccessStatusCode();
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        string d2 = JsonConvert.DeserializeObject<string>(post_data);
                        return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }

                    }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "reporttemplatefilter");
                return Json(ex.Message);
            }
        }
        public class reporttemplatefilterModel
        {
            public Int32? in_reporttemplatefilter_gid { get; set; }
            public String? in_reporttemplate_code { get; set; }
            public Decimal? in_filter_seqno { get; set; }
            public string? in_report_field { get; set; }
            public string? in_filter_criteria { get; set; }
            public string? in_filter_value { get; set; }
            public string? in_open_parentheses_flag { get; set; }
            public string? in_close_parentheses_flag { get; set; }
            public string in_join_condition { get; set; }
            public string? in_last_system_flag { get; set; }
            public string? in_action { get; set; }
            public String? in_active_status { get; set; }
        }
        #endregion

        #region download withinaccount


        public ActionResult kendogrid_download_withinaccount(string in_tran_date, string in_recon_code, string in_recon_name)
        {

            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            DataTable Table1 = new DataTable();
            string post_data = "";
            string filename = "Recon-within A/cs.xlsx";
            try
            {
                using (var client = new HttpClient())
                {
                    reconbetweenaccmodel report = new reconbetweenaccmodel();
                    report.in_tran_date = in_tran_date;
                    report.in_recon_code = in_recon_code;
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(report), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reconwithinacc", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    Table1 = JsonConvert.DeserializeObject<DataTable>(d2);
                    Table1.Columns.RemoveAt(0);
                    Table1.Columns[0].ColumnName = "Particulars";
                    Table1.Columns[1].ColumnName = "Amount";
                    Table1.Columns[2].ColumnName = "Account Mode";
                    Table1.Columns[3].ColumnName = "Balance Amount";



                    using (XLWorkbook wb = new XLWorkbook())
                    {

                        var ws = wb.AddWorksheet(in_recon_name);
                        ws.Cell("A4").InsertTable(Table1);
                        wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                        wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                        ws.Range("A4:D4").Style.Font.Bold = true;
                        ws.Range("A4:D4").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        ws.Range("A17:D17").Style.Font.Bold = true;
                        ws.Range("A17:D17").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                        ws.Cell("A11").Style.Font.Bold = true; ws.Cell("A13").Style.Font.Bold = true;
                        ws.Range("A19:D19").Style.Font.Bold = true;
                        ws.Range("A20:D20").Style.Font.Bold = true;
                        ws.Range("A21:D21").Style.Font.Bold = true;

                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                        }
                    }
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }


        #endregion


        #region Pagination Report

        //ReportmatchoffGridRead

        public ActionResult ReportmatchoffGridRead(paginationModel context)   // ProgressView  Read
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    
                    try{
                        var response = client.PostAsync("Manualmatchoffgridload", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Manualmatchoffgridload");
                return Json(ex.Message);
            }
        }


        public class paginationModel
        {
            public int _count { get; set; }
            public int _pageno { get; set; } = 1;
            public int _pagesize { get; set; } = 100;
            public string? table_name { get; set; }
            public string? condition { get; set; }
            public Boolean radio_checked { get; set; } = false;
            public int recon_id { get; set; } = 0;
        }

        #endregion


        #region Report Run Pagination

        public ActionResult Report_Runpagereport([FromBody] runPageReportModel context)
        {   //first sp call
            try
            {
                urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
                DataTable result = new DataTable();
                string post_data = "";
                try
                {
                    using (var client = new HttpClient())
                    {
                        string Urlcon = "Report/";
                        client.BaseAddress = new Uri(urlstring + Urlcon);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.Timeout = Timeout.InfiniteTimeSpan;
                        client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                        client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                        client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                        client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                        try { 
                        var response = client.PostAsync("runPageReport", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        string d2 = JsonConvert.DeserializeObject<string>(post_data);
                        return Json(d2);
                        }
                        catch (HttpRequestException ex)
                        {
                            return Json(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    CommonController objcom = new CommonController(_configuration);
                    objcom.errorlog(ex.Message, "runPageReport");
                    return Json(ex.Message);
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
            }
            return View();
        }

        public class runPageReportModel
        {
            public string? in_reporttemplate_code { get; set; }
            public string? in_report_condition { get; set; }
            public string? in_recon_code { get; set; }
            public string? in_report_code { get; set; }
        }


        #endregion



        #region RemarkReson
        public ActionResult RemarkReason(remarkResonModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("RemarkReason", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "RemarkReason");
                return Json(ex.Message);
            }
        }

        public class remarkResonModel
        {
            public string remark1 { get; set; }
            public int tran_gid { get; set; }
        }

        #endregion

        #region  ReportExpenceGrid_Read
        public ActionResult ReportExpenceGrid_Read(ReportExpenceGridModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("ExectionReport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "ExectionReport");
                return Json(ex.Message);
            }
        }

        public class ReportExpenceGridModel
        {
            public string? table_name { get; set; }
            public string? condition { get; set; }
            public Boolean radio_checked { get; set; }
            public int recon_id { get; set; }
            public int recon_gid { get; set; }
        }
        #endregion


        #region getPageNoReport
        public ActionResult getPageNoReport([FromBody] getPageNoReportModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("getPageNoReport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "ExectionReport");
                return Json(ex.Message);
            }
        }

        public class getPageNoReportModel
        {
            public string? in_reporttemplate_code { get; set; }
            public int? in_rptsession_gid { get; set; }
            public int in_page_no { get; set; }
            public int in_page_size { get; set; }
            public string in_condition { get; set; }
			public int in_tot_records { get; set; }
            public string in_recon_code { get; set; }
            public string? in_report_code { get; set; }
        }

        #endregion


        #region viewreportGenerate
        [HttpPost]
        public JsonResult viewReportGenerate([FromBody] viewReportGenerateModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("fetchReportTemplate", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "fetchReportTemplate");
                return Json(ex.Message);
            }
        }
        public class viewReportGenerateModel
        {
            public String? jobid { get; set; }
        }
        #endregion


        #region getreportparamwithrecon
        public JsonResult getreportparamrecon([FromBody] Reconparamreconlistmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            List<getreportparamlist> objcat_lst = new List<getreportparamlist>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("getreportparamlistrecon", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getreportparamlistrecon");
                return Json(ex.Message);
            }
        }

        public class Reconparamreconlistmodel
        {
            public String? in_report_code { get; set; }
            public String? in_recon_code { get; set; }
        }
        #endregion

        #region clonereporttemplate

        public JsonResult clonereporttemplate([FromBody] clonereporttemplateModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            List<getreportparamlist> objcat_lst = new List<getreportparamlist>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("cloneReportTemplate", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "cloneReportTemplate");
                return Json(ex.Message);
            }
        }

        public class clonereporttemplateModel
        {
            public int? in_reporttemplate_gid { get; set; }
            public string? in_clone_reporttemplate_code { get; set; }
            public string? in_reporttemplate_name { get; set; }
            public string? in_report_code { get; set; }
            public string? in_action { get; set; }
            public string? in_active_status { get; set; }
        }
        #endregion

        #region reporttemplatefield
        public JsonResult reporttemplatefield([FromBody] reporttemplatefieldModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reporttemplatefield", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "cloneReportTemplate");
                return Json(ex.Message);
            }
        }

        public class reporttemplatefieldModel
        {
            public string? templateJSON { get; set; }
            public string? in_reporttemplate_code { get; set; }
        }

        #endregion

        #region reporttemplatesorting
        public JsonResult reporttemplatesorting([FromBody] reporttemplatesortingModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("reporttemplatesorting", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "reporttemplatesorting");
                return Json(ex.Message);
            }
        }

        public class reporttemplatesortingModel
        {
            public int? in_reporttemplatesorting_gid { get; set; }
            public string? in_reporttemplate_code { get; set; }
            public string? in_report_field { get; set; }
            public decimal? in_sorting_order { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_action_by { get; set; }
            public string? in_delete_flag { get; set; }
        }

        #endregion


        #region uploadreporttempletefile

        public async Task<JsonResult> uploadreporttempletefile(string in_reporttemplate_code, IFormFile file, string in_action_by, string in_file_name)
        {
            var out_result = getuploadfolderpath();
            string folder_path = "";
            List<fileconfigmodel> myObjects = JsonConvert.DeserializeObject<List<fileconfigmodel>>(out_result.Value.ToString());
            if (myObjects.Count > 0)
            {
                folder_path = myObjects[0].out_config_value;
            }
            uploadreporttempletefileModel objcontent = new uploadreporttempletefileModel();
            objcontent.in_reporttemplate_code = in_reporttemplate_code;
            objcontent.file = file;
            objcontent.in_action_by = in_action_by;
            objcontent.in_file_name = in_file_name;
            objcontent.in_file_path = folder_path;

            if (file != null && file.Length > 0)
            {
                if (!Directory.Exists(folder_path))
                {
                    Directory.CreateDirectory(folder_path);
                }
                string[] parts = file.FileName.Split('.');
                string extension = "." + parts.Last();
                string dummy_file_name = in_reporttemplate_code + extension;
                HttpContext.Session.SetString("session_folderpath", folder_path);
                HttpContext.Session.SetString("session_filename", file.FileName);
                HttpContext.Session.SetString("session_dummy_filename", dummy_file_name);
                string uniqueFileName = Path.Combine(folder_path, dummy_file_name);
                using (var fileStream = new FileStream(uniqueFileName, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                HttpContext.Session.SetString("session_uniqueFileName", uniqueFileName);
                var result = setfilePath(objcontent);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return Json(new { success = false, message = "No file selected." });
            }
          }

        public JsonResult setfilePath([FromBody] uploadreporttempletefileModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("uploadreporttempletefile", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "uploadreporttempletefile");
                return Json(ex.Message);
            }
        }

        public class uploadreporttempletefileModel
        {
            public string? in_reporttemplate_code { get; set; }
            public string? in_file_name { get; set; }
            public string? in_file_path { get; set; }
            public string? in_action_by { get; set; }
            public IFormFile? file { get; set; }
        }

        #endregion


        public JsonResult getuploadfolderpath()
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            fileconfigmodel FileDownload = new fileconfigmodel();

            var context = _configuration.GetSection("Appsettings")["Uploadpath"].ToString();
            FileDownload.in_config_name = context;
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Common/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownload), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("configvalue", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    return Json(d2);

                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Datasetdetail");
                return Json(ex.Message);
            }
        }

        public class fileconfigmodel
        {
            public string? in_config_name { get; set; }
            public string? out_config_value { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }

        }

        #region generatedynamicreport


        [HttpPost]
        public JsonResult generatedynamicreport([FromBody] generatedynamicReportmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("generatedynamicreport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "generatedynamicreport");
                return Json(ex.Message);
            }
        }

        public class generatedynamicReportmodel
        {

            public String? in_reporttemplate_code { get; set; }
            public String? in_recon_code { get; set; }
            public String? in_report_code { get; set; }
            public String? in_report_param { get; set; }
            public String? in_report_condition { get; set; }
            public String? in_ip_addr { get; set; }
            public Boolean? in_outputfile_flag { get; set; }
            public String? in_outputfile_type { get; set; }
            public string? in_user_code { get; set; }
            public string? file_name { get; set; }
            public string? in_report_name { get; set; }

        }

        #endregion



        #region generatedynamicXLSX
        public ActionResult generateReportXLSXdynamic([FromBody] generatexlsxdynamicReportmodel context)
        {

            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            DataTable Table1 = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {

                    string Urlcon = "Report/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("generatedynamicreport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    Table1 = JsonConvert.DeserializeObject<DataTable>(d2);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }


        public class generatexlsxdynamicReportmodel
        {

            public String? in_reporttemplate_code { get; set; }
            public String? in_report_code { get; set; }
            public String? in_recon_code { get; set; }
            public String? in_report_param { get; set; }
            public String? in_report_condition { get; set; }
            public String? in_ip_addr { get; set; }
            public Boolean? in_outputfile_flag { get; set; }
            public String? in_outputfile_type { get; set; }
            public string? in_user_code { get; set; }
            public string? file_name { get; set; }
            public string? in_report_name { get; set; }
        }


        public JsonResult getfilepath()
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            fileconfigmodel FileDownload = new fileconfigmodel();

            var context = _configuration.GetSection("Appsettings")["fileconfig_value"].ToString();
            FileDownload.in_config_name = context;
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Common/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownload), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("configvalue", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }

                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Datasetdetail");
                return Json(ex.Message);
            }
        }


        #endregion

        #region clone files

        public string clonefiles([FromBody] filepaths context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            fileconfigmodel FileDownload = new fileconfigmodel();
            var getfolderpath = roleconfig_db();
            try
            {
                var source_file = getfolderpath + context.source_code + ".xlsx";
                var destination_file = getfolderpath + context.destination_code + ".xlsx";
                System.IO.File.Copy(source_file, destination_file, true);
                return "Success";
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Datasetdetail");
                return "Error";
            }
        }

        public class filepaths
        {
            public String? source_code { get; set; }
            public String? destination_code { get; set; }

        }

        #endregion

        public string roleconfig_db()
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            fileconfigmodel FileDownload = new fileconfigmodel();

            var context = _configuration.GetSection("Appsettings")["Uploadpath"].ToString();
            FileDownload.in_config_name = context;
            DataTable result = new DataTable();
            string post_data = "";
            List<configmodel> objcat_lst = new List<configmodel>();
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Common/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownload), UTF8Encoding.UTF8, "application/json");
                    try { 
                    var response = client.PostAsync("configvalue", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    configmodel objcat = new configmodel();
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        objcat.out_config_value = result.Rows[i]["out_config_value"].ToString();
                        objcat.out_msg = result.Rows[i]["out_msg"].ToString();
                        objcat.out_result = result.Rows[i]["out_result"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return objcat.out_config_value;

                    }
                    catch (HttpRequestException ex)
                    {
                        return "";
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "configvalue");
                return "Error";
            }
        }

        public class configmodel
        {
            public string? out_config_value { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }

    }

}