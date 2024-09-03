using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.ThemeController;

namespace Recon_proto.Controllers
{
	public class PreprocessController : Controller
	{
		private IConfiguration _configuration;
		public PreprocessController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string urlstring = "";
		public IActionResult Preprocesslist()
		{
			return View();
		}
		public IActionResult PreprocessForm()
		{
			return View();
		}
		#region Preprocesslistfetch

		[HttpPost]
		public JsonResult Preprocesslistfetch([FromBody] Preprocesslistmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Preprocess/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code",  context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Preprocesslist", content).Result;
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
				objcom.errorlog(ex.Message, "Preprocesslistfetch");
				return Json(ex.Message);
			}

		}
		public class Preprocesslistmodel
		{
			public string? recon_code { get; set; }
			public string? in_user_code { get; set; }
		}
		#endregion

		#region preprocess header
		[HttpPost]
		public JsonResult preprocessheader([FromBody] preprocessHeadermodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			preprocessHeadermodel objList = new preprocessHeadermodel();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Preprocess/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					//client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code",  context.in_action_by);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("preprocessHeader", content).Result;
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
				objcom.errorlog(ex.Message, "preprocessheader");
				return Json(ex.Message);
			}
		}
		public class preprocessHeadermodel
		{
			public string? preprocess_name { get; set; }
			public string? preprocess_Code { get; set; }
			public Int32 preprocess_gid { get; set; }
			public string? recon_code { get; set; }
			public string? get_recon_field { get; set; }
			public string? set_recon_field { get; set; }
			public string? process_method { get; set; }
			public string? process_query { get; set; }
			public string? process_function { get; set; }
			public string? preprocess_order { get; set; }
			public string? lookup_dataset_code { get; set; }
			public string? lookup_return_field { get; set; }
			public string? lookup_multi_return_flag { get; set; }
			public string? in_returnflag { get; set; }
			public string? active_status { get; set; }
			public string? in_action { get; set; }
			public string? in_action_by { get; set; }
			public string? hold_flag { get; set; }
			public string? clone_process { get; set; }
		}
		#endregion

		#region Filter
		public class filterdata
		{
			public int? in_preprocessfilter_gid { get; set; }
			public string? in_preprocess_code { get; set; }
			public string? in_recon_code { get; set; }
			public Double in_filter_seqno { get; set; }
			public string? in_filter_field { get; set; }
			public string? in_filter_criteria { get; set; }
			public string? in_ident_value_flag { get; set; }
			public string? in_ident_value { get; set; }
			public string? in_open_flag { get; set; }
			public string? in_close_flag { get; set; }
			public string? in_join_condition { get; set; }
			public string? in_filter_applied_on { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? in_user_code { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		[HttpPost]
		public JsonResult filterdatasave([FromBody] filterdata context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			filterdata objList = new filterdata();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "Preprocess/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("preprocessfilter", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_preprocessfilter_gid = Convert.ToInt32(result.Rows[i]["in_preprocessfilter_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion

		#region Preprocessfetch

		[HttpPost]
		public JsonResult Preprocessfetch([FromBody] Preprocessfetchmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Preprocess/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Preprocessfetch", content).Result;
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
				objcom.errorlog(ex.Message, "Preprocessfetch");
				return Json(ex.Message);
			}

		}
		public class Preprocessfetchmodel
		{
			public string? preprocess_code { get; set; }
			public string? in_user_code { get; set; }
		}
		#endregion

		#region Condition
		public class conditiondata
		{
			public int? in_preprocesscondition_gid { get; set; }
			public string? in_preprocess_code { get; set; }
			public string? in_Ldataset_code { get; set; }
			public string? in_Lreturn_field { get; set; }
			public string? in_setrecon_field { get; set; }
			public Double in_condition_seqno { get; set; }
			public string? in_recon_field { get; set; }
			public string? in_extraction_criteria { get; set; }
			public string? in_extraction_filter { get; set; }
			public string? in_lookup_field { get; set; }
			public string? in_comparison_criteria { get; set; }
			public string? in_comparison_filter { get; set; }
			public string? in_open_flag { get; set; }
			public string? in_returnflag { get; set; }
			public string? in_close_flag { get; set; }
			public string? in_join_condition { get; set; }
			public string? in_source_field_type { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? in_action_by { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		[HttpPost]
		public JsonResult conditiondatasave([FromBody] conditiondata context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			conditiondata objList = new conditiondata();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "Preprocess/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("preprocesscondition", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_preprocesscondition_gid = Convert.ToInt32(result.Rows[i]["in_preprocesscondition_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion

		#region validatequery

		[HttpPost]
		public JsonResult validatequery([FromBody] Validatequerymodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Preprocess/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Validatequery", content).Result;
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
				objcom.errorlog(ex.Message, "validatequery");
				return Json(ex.Message);
			}

		}
		public class Validatequerymodel
		{
			public string in_sql { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		#endregion

		#region fetch condition
		public class getConditionlook
		{
			public String? in_condition_type { get; set; }
			public String? in_field_type { get; set; }
			public String? in_dataset_code { get; set; }
		}
		[HttpPost]
		public JsonResult lookupfilterfetch([FromBody] getConditionlook context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "Preprocess/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getConditionlookup", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}
		#endregion

		#region Preprocesslistfetchclone

		[HttpPost]
		public JsonResult Preprocesslistfetchclone([FromBody] Preprocesslistmodelclone context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Preprocess/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Preprocesslistclone", content).Result;
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
				objcom.errorlog(ex.Message, "Preprocesslistfetchclone");
				return Json(ex.Message);
			}

		}
		public class Preprocesslistmodelclone
		{
			public string? recon_code { get; set; }
			public string? in_user_code { get; set; }
		}
		#endregion

		#region preprocessclone save
		public class clonepreprocessModel
		{
			public String? in_preprocess_name { get; set; }
			public String? in_clone_preprocess_code { get; set; }

		}
		[HttpPost]
		public JsonResult preprocessclone([FromBody] clonepreprocessModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Preprocess/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("clonepreprocess", content).Result;
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

		#region fetch dataset
		public class getdataagainsPreprocess
		{
			public String? in_recon_code { get; set; }
		}
		[HttpPost]
		public JsonResult Preprocessdatasetfetch([FromBody] getdataagainsRecon context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "Preprocess/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getdatasetReconPreprocess", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}
		#endregion

		#region lookup field
		public class lookupfieldmodel
		{
			public int? in_preprocesslookup_gid { get; set; }
			public string? in_preprocess_code { get; set; }
			public Double in_lookup_seqno { get; set; }
			public string? in_lookup_return_field { get; set; }
			public string? in_set_recon_field { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? in_user_code { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		[HttpPost]
		public JsonResult lookupfieldsave([FromBody] lookupfieldmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			lookupfieldmodel objList = new lookupfieldmodel();
			DataTable result = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "Preprocess/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("lookupfieldsave", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_preprocesslookup_gid = Convert.ToInt32(result.Rows[i]["in_preprocesslookup_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = result.Rows[i]["out_result"].ToString();
				}
				return Json(objList);
			}
		}
		#endregion
	}
}
