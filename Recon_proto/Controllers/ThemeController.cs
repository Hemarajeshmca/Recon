using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.KnockOffController;

namespace Recon_proto.Controllers
{
	public class ThemeController : Controller
	{
		private IConfiguration _configuration;
		public ThemeController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string urlstring = "";
		public IActionResult Themelist()
		{
			return View();
		}
		public IActionResult Themeview()
		{
			return View();
		}

		#region Themelistfetch

		[HttpPost]
		public JsonResult Themelistfetch([FromBody] themelistmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();			
			DataTable result = new DataTable();		
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("themeRead", content).Result;
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
				objcom.errorlog(ex.Message, "Themelistfetch");
				return Json(ex.Message);
			}

		}
		public class themelistmodel
		{
			public string? recon_code { get; set; }
		}
			#endregion

		#region themeheader
			[HttpPost]
		public JsonResult themeheader([FromBody] themeheadermodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			themeheadermodel objList = new themeheadermodel();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					//client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("themeHeader", content).Result;
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
				objcom.errorlog(ex.Message, "themeheader");
				return Json(ex.Message);
			}
		}
		public class themeheadermodel
		{
			public string theme_name { get; set; }
			public string theme_Code { get; set; }
			public string theme_order { get; set; }
			public Int32 theme_gid { get; set; }
			public string recon_code { get; set; }
			public string active_status { get; set; }
			public string in_action { get; set; }
			public string in_action_by { get; set; }
			public string out_msg { get; set; }
			public string out_result { get; set; }
			public string theme_type { get; set; }
			public string source_dataset { get; set; }
			public string comparison_dataset { get; set; }
			public string inactive_reason { get; set; }
			
		}
		#endregion

		#region themedetail
		[HttpPost]
		public JsonResult themedetail([FromBody] themedetailmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			themedetailmodel objList = new themedetailmodel();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					//client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("themedetail", content).Result;
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
				objcom.errorlog(ex.Message, "Datasetheader");
				return Json(ex.Message);
			}
		}
		public class themedetailmodel
		{
			public Int32 themefilter_gid { get; set; }
			public decimal themefilter_seqno { get; set; }
			public string theme_code { get; set; }
			public string filter_applied_on { get; set; }
			public string filter_field { get; set; }
			public string filter_criteria { get; set; }
			public string filter_value { get; set; }
			public string filter_value_flag { get; set; }
			public string open_flag { get; set; }
			public string close_flag { get; set; }
			public string join_condition { get; set; }
			public string? active_status { get; set; }
			public string? action { get; set; }			
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		#endregion

		#region Themedetailfetch

		[HttpPost]
		public JsonResult Themedetailfetch([FromBody] themeDetailmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("themefield", content).Result;
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
				objcom.errorlog(ex.Message, "Themelistfetch");
				return Json(ex.Message);
			}
		}
		public class themeDetailmodel
		{
			public string theme_Code { get; set; }
		}
		#endregion

		#region Themeclonefetch

		[HttpPost]
		public JsonResult Themeclonefetch()
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Themeclonefetch", content).Result;
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
				objcom.errorlog(ex.Message, "Themeclonefetch");
				return Json(ex.Message);
			}

		}

		#endregion

		#region  theme condition
		public class themecondition
		{
			public int? in_themecondition_gid { get; set; }
			public string? in_theme_code { get; set; }
			public Decimal? in_themecondition_seqno { get; set; }
			public string? in_source_field { get; set; }
			public string? in_comparison_field { get; set; }
			public string? in_extraction_criteria { get; set; }
			public string? in_comparison_criteria { get; set; }
			public string? in_open_flag { get; set; }
			public string? in_close_flag { get; set; }
			public string? in_join_condition { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? in_user_code { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}

		[HttpPost]
		public JsonResult themeconditionsave([FromBody] themecondition context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			themecondition objList = new themecondition();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "theme/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("themecondition", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_themecondition_gid = Convert.ToInt32(result.Rows[i]["in_themecondition_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion

		#region grouping
		public class themegrouping
		{
			public int? in_themegrpfield_gid { get; set; }
			public Decimal? themegrpfield_seqno { get; set; }
			public string? grpfield_applied_on_code { get; set; }
			public string? in_grp_field { get; set; }
			public string? in_theme_code { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }			
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		[HttpPost]
		public JsonResult themegroupingsave([FromBody] themegrouping context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			themegrouping objList = new themegrouping();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "theme/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("themegrouping", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_themegrpfield_gid = Convert.ToInt32(result.Rows[i]["in_themegrpfield_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion
		#region Aggfunction
		public class Aggfunction
		{
			public int? themeaggfield_gid { get; set; }
			public Decimal? themeaggfield_seqno { get; set; }
			public string? recon_field { get; set; }
			public string? themeaggfield_applied_on { get; set; }
			public string? themeaggfield_name { get; set; }
			public string? themeagg_function { get; set; }
			public string? in_theme_code { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		[HttpPost]
		public JsonResult Aggfunctionsave([FromBody] Aggfunction context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			Aggfunction objList = new Aggfunction();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "theme/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("themeaggfun", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.themeaggfield_gid = Convert.ToInt32(result.Rows[i]["in_themeaggfield_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion
		#region fetch condition
		public class getConditiontheme
		{
			public String? in_condition_type { get; set; }
			public String? in_field_type { get; set; }
			public String? in_theme_code { get; set; }
		}
		[HttpPost]
		public JsonResult getconditionfetch([FromBody] getConditiontheme context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "theme/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getConditioncriteria", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}
		#endregion

		#region Aggcondition
		public class Aggcondition
		{
			public int? themeaggcondition_gid { get; set; }
			public Decimal? themeaggcondition_seqno { get; set; }
			public string? themeagg_applied_on { get; set; }
			public string? themeagg_field { get; set; }
			public string? themeagg_criteria { get; set; }
			public string? themeagg_value_flag { get; set; }
			public string? themeagg_value { get; set; }
			public string? in_theme_code { get; set; }
			public string? in_open_flag { get; set; }
			public string? in_close_flag { get; set; }
			public string? in_join_condition { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		[HttpPost]
		public JsonResult Aggconditionsave([FromBody] Aggcondition context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			Aggcondition objList = new Aggcondition();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "theme/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("themeaggcondition", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.themeaggcondition_gid = Convert.ToInt32(result.Rows[i]["in_themeaggcondition_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion

		#region fetch dataset
		public class getdataagainsRecon
		{
			public String? in_recon_code { get; set; }
		}
		[HttpPost]
		public JsonResult themerecondatasetfetch([FromBody] getdataagainsRecon context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "theme/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getdataagainsRecontheme", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}
		#endregion

		#region Themelistfetchclone

		[HttpPost]
		public JsonResult Themelistfetchclone([FromBody] themelistmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("themelistclone", content).Result;
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
				objcom.errorlog(ex.Message, "Themelistfetchclone");
				return Json(ex.Message);
			}
		}
		#endregion

		#region Themeclone save
		public class clonethemeModel
		{
			public String? in_theme_name { get; set; }
			public String? in_clone_theme_code { get; set; }

		}
		[HttpPost]
		public JsonResult Themeclone([FromBody] clonethemeModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("clonetheme", content).Result;
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
				objcom.errorlog(ex.Message, "Themeclone");
				return Json(ex.Message);
			}

		}
		#endregion

		#region runtheme
		[HttpPost]
		public JsonResult runtheme([FromBody] runthemeModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataSet result = new DataSet();
			DataTable result1 = new DataTable();
			string post_data = "";
			string d2 = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "theme/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("runrecontheme", content).Result;
					Stream data = response.Content.ReadAsStreamAsync().Result;
					StreamReader reader = new StreamReader(data);
					post_data = reader.ReadToEnd();
					d2 = JsonConvert.DeserializeObject<string>(post_data);
					return Json(d2);
				}
			}
			catch (Exception ex)
			{
				CommonController objcom = new CommonController(_configuration);
				objcom.errorlog(ex.Message, "runrecontheme");
				return Json(ex.Message);
			}
		}

		public class runthemeModel
		{
			public String? in_recon_code { get; set; }
			public int in_job_gid { get; set; }
			public String? in_period_from { get; set; }
			public String? in_period_to { get; set; }
			public String? in_automatch_flag { get; set; }
		}

		#endregion

	}
}
