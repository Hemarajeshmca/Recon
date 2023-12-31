﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Recon_proto.Controllers.ReconController;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.CommonController;

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
	}
}
