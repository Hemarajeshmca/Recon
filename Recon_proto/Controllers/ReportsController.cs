using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Recon_proto.Controllers.ReconController;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.CommonController;
using ClosedXML.Excel;
using System.Dynamic;
using System.Text.RegularExpressions;
using Recon_proto.Models;
using DocumentFormat.OpenXml.InkML;

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
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
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
            } catch (Exception ex)
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
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
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
            } catch (Exception ex)
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
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
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
            }  catch (Exception ex)
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
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("generatereport", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "generateReport");
                return Json(ex.Message);
            }
        }
	
		public class generateReportmodel
		{
			public String? in_recon_code { get; set;}
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
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("reconwithinacc", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
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
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Report/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("reconbetweenacc", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
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
				client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
				client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
				client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
				client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("ReconVersionhistory", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
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
                    client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                    client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                    client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                    client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(report), UTF8Encoding.UTF8, "application/json");
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
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        #endregion
    }


}
