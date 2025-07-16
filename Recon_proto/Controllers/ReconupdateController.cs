using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Recon_proto.Controllers.ReportsController;
using System.Net.Http.Headers;
using System.Text;
using System.Data;
using DocumentFormat.OpenXml.InkML;
using static Recon_proto.Controllers.ReconController;

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
        public IActionResult Updatecustomreport()
        {
            return View();
        }
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

		//ReconagainstRule
		[HttpPost]
		public JsonResult ReconagainstRule([FromBody] ReconagainstRuleModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("ReconagainstRule", content).Result;
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
				objcom.errorlog(ex.Message, "ReconagainstRule");
				return Json(ex.Message);
			}
		}
		public class ReconagainstRuleModel
		{
			public string? in_rule_name { get; set; }
		}

		//ReconagainstPreProcess

		[HttpPost]
		public JsonResult ReconagainstPreProcess([FromBody] ReconagainstPreProcessModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("ReconagainstPreProcess", content).Result;
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
				objcom.errorlog(ex.Message, "ReconagainstRule");
				return Json(ex.Message);
			}
		}
		public class ReconagainstPreProcessModel
		{
			public string? in_preprocess_name { get; set; }
		}

		//ReconagainstTheme
		[HttpPost]
		public JsonResult ReconagainstTheme([FromBody] ReconagainstThemeModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("ReconagainstTheme", content).Result;
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
				objcom.errorlog(ex.Message, "ReconagainstTheme");
				return Json(ex.Message);
			}
		}
		public class ReconagainstThemeModel
		{
			public string? in_theme_name { get; set; }
		}

        //datasetmap

        [HttpPost]
        public JsonResult datasetmap([FromBody] datasetmapModel context)
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
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try
                    {
                        var response = client.PostAsync("datasetmap", content).Result;
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
                objcom.errorlog(ex.Message, "ReconagainstTheme");
                return Json(ex.Message);
            }
        }
        public class datasetmapModel
        {
            public int? in_datasetmap_gid { get; set; }
            public string? in_recon_code { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_action_by { get; set; }
            public string? in_user_code { get; set; }

        }


		//datasetlistmapped
		[HttpPost]
		public JsonResult datasetlistmapped([FromBody] datasetlistmappedModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					try
					{
						var response = client.PostAsync("datasetlistmapped", content).Result;
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
				objcom.errorlog(ex.Message, "datasetlistmapped");
				return Json(ex.Message);
			}
		}
		public class datasetlistmappedModel
		{
			public string? in_recon_code { get; set; }
			public string? in_target_recon_code { get; set; }

		}

        #region list
        [HttpPost]
        public JsonResult Reconalllistfetch([FromBody] Reconalllistmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            List<Reconlistmodel> objcat_lst = new List<Reconlistmodel>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "UpdateRecon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("http://localhost:4195/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("reconalllist", content).Result;
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
                objcom.errorlog(ex.Message, "Reconlistfetch");
                return Json(ex.Message);
            }
        }

        public class Reconalllistmodel
        {

            public string? in_user_code { get; set; }

        }
        #endregion

        [HttpPost]
        public JsonResult ReconUpdatecusrpt([FromBody] ReconUpdatecusrptModel context)
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
                        var response = client.PostAsync("ReconUpdatecusrpt", content).Result;
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
                objcom.errorlog(ex.Message, "ReconUpdatecusrpt");
                return Json(ex.Message);
            }
        }
        public class ReconUpdatecusrptModel
        {
            public string? in_base_recon_code { get; set; }
            public string? in_base_report_code { get; set; }
            public string? in_update_recon_code { get; set; }
            public string? in_update_report_name { get; set; }
            public string? in_user_code { get; set; }
        }
    }
}
