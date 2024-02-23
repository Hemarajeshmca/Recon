using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Recon_proto.Controllers.ReconController;
using System.Data;
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

		public IActionResult ReportGenerationlist()
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
                        client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                        client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                        client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                        client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(contect), UTF8Encoding.UTF8, "application/json");
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
                        //  string s2 = Regex.Replace(recon_name, @"[^A-Z]+", String.Empty);
                        var acc_no2 = Table.Rows[0]["target_dataset_name"].ToString();
                        string Acc1date = Table.Rows[0]["source_bal_date"].ToString();
                        string Acc2date = Table.Rows[0]["target_bal_date"].ToString();
                        Decimal acc1Bal_amount = Convert.ToDecimal(Table.Rows[0]["source_bal_value"].ToString());
                        Decimal acc2Bal_amount = Convert.ToDecimal(Table.Rows[0]["target_bal_value"].ToString());
                        Decimal accno1_drtotal = Convert.ToDecimal(Table.Rows[0]["source_dr_total"].ToString());
                        Decimal accno2_drtotal = Convert.ToDecimal(Table.Rows[0]["target_dr_total"].ToString());
                        Decimal accno1_crtotal = Convert.ToDecimal(Table.Rows[0]["source_cr_total"].ToString());
                        Decimal accno2_crtotal = Convert.ToDecimal(Table.Rows[0]["target_cr_total"].ToString());

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
                            formulaTxt = "=+(" + calcCellTxt + "*1)-" + cellTxt;

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
                            ws.Cell("A1").InsertTable(Table6).Theme = XLTableTheme.None;
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
        [HttpGet]
        public JsonResult reportTempletelist()
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
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
                    var response = client.GetAsync("getReportTemplateList").Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getReportTemplateList");
                return Json(ex.Message);
            }
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
                    client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                    client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                    client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                    client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("reportTemplate", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
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
            public String? in_action { get; set; }
            public String? in_active_status { get; set; }
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
                    client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                    client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                    client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                    client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("fetchReportTemplate", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
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
                    client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                    client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                    client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                    client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("reporttemplatefilter", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
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
            public Int32? in_filter_seqno { get; set; }
            public string? in_report_field { get; set; }
            public string? in_filter_criteria { get; set; }
            public string? in_filter_value { get; set; }
            public string? in_open_parentheses_flag { get; set; }
            public string? in_close_parentheses_flag { get; set; }
            public string? in_join_condition { get; set; }
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
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(report), UTF8Encoding.UTF8, "application/json");
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
			}
			catch (Exception e)
			{
				return Json(e.Message);
			}
		}


		#endregion

	}
}