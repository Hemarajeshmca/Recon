using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Recon_proto.Controllers.ReportsController;
using System.Net.Http.Headers;
using System.Text;
using System.Data;
using DocumentFormat.OpenXml.InkML;

namespace Recon_proto.Controllers
{
    public class ReconupdateController : Controller
    {
        private IConfiguration _configuration;
        public ReconupdateController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult ReconRuleUpdate()
        {
            return View();
        }

		public IActionResult UpdateReconPreprocess()
		{
			return View();
		}

		public IActionResult UpdateReconTheme()
		{
			return View();
		}

		//[HttpGet]
		//public JsonResult testing()
		//{
		//	urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
		//	DataTable result = new DataTable();
		//	string post_data = "";
		//	try
		//	{
		//		using (var client = new HttpClient())
		//		{
		//			string Urlcon = "ReconUpdate/";
		//			client.BaseAddress = new Uri(urlstring + Urlcon);
		//			client.DefaultRequestHeaders.Accept.Clear();
		//			client.Timeout = Timeout.InfiniteTimeSpan;
		//			client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
		//			client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
		//			client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
		//			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		//			HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
		//			try
		//			{
		//				var response = client.PostAsync("ReconUpdateRule", content).Result;
		//				Stream data = response.Content.ReadAsStreamAsync().Result;
		//				StreamReader reader = new StreamReader(data);
		//				post_data = reader.ReadToEnd();
		//				string d2 = JsonConvert.DeserializeObject<string>(post_data);
		//				return Json(d2);
		//			}
		//			catch (HttpRequestException ex)
		//			{
		//				return Json(ex);
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		CommonController objcom = new CommonController(_configuration);
		//		objcom.errorlog(ex.Message, "ReconUpdateRule");
		//		return Json(ex.Message);
		//	}
		//}

		//ReconUpdateRule
		[HttpPost]
		public JsonResult ReconUpdateRule([FromBody] ReconUpdateRuleModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UpdateRecon/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("ReconUpdateRule", content).Result;
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
				objcom.errorlog(ex.Message, "ReconUpdateRule");
				return Json(ex.Message);
			}
		}
		public class ReconUpdateRuleModel
		{
			public string? in_base_recon_code { get; set; }
			public string? in_base_rule_code { get; set; }
			public string? in_update_recon_code { get; set; }
			public string? in_update_rule_code { get; set; }
			public string? in_user_code { get; set; }
		}

		//ReconUpdatePreprocess
		[HttpPost]
		public JsonResult ReconUpdatePreprocess([FromBody] ReconUpdatePreprocessModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UpdateRecon/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("ReconUpdatePreprocess", content).Result;
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
				objcom.errorlog(ex.Message, "ReconUpdatePreprocess");
				return Json(ex.Message);
			}
		}
		public class ReconUpdatePreprocessModel
		{
			public string? in_base_recon_code { get; set; }
			public string? in_base_preprocess_code { get; set; }
			public string? in_update_recon_code { get; set; }
			public string? in_update_preprocess_code { get; set; }
			public string? in_user_code { get; set; }
		}

		[HttpPost]
		public JsonResult ReconUpdateTheme([FromBody] ReconUpdateThemeModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UpdateRecon/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("ReconUpdateTheme", content).Result;
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
				objcom.errorlog(ex.Message, "ReconUpdateTheme");
				return Json(ex.Message);
			}
		}
		public class ReconUpdateThemeModel
		{
			public string? in_base_recon_code { get; set; }
			public string? in_base_theme_code { get; set; }
			public string? in_update_recon_code { get; set; }
			public string? in_update_theme_code { get; set; }
			public string? in_user_code { get; set; }
		}



	}
}
