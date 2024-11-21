using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Recon_proto.Controllers
{
	public class ReconversionController : Controller
	{
		private IConfiguration _configuration;
		public ReconversionController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string urlstring = "";
		public IActionResult ReconversionList()
		{
			return View();
		}
		#region list
		[HttpPost]
		public JsonResult ReconversionListfetch([FromBody] ReconversionListfetchmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "ReconVersion/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("ReconVersionfetch", content).Result;
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
				objcom.errorlog(ex.Message, "ReconversionListfetch");
				return Json(ex.Message);
			}

		}

		public class ReconversionListfetchmodel
		{
			public string in_recon_code { get; set; }
			public string? in_user_code { get; set; }
		}
		#endregion

		#region save
		[HttpPost]
		public JsonResult Reconversionsave([FromBody] ReconVersionmodelsave context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "ReconVersion/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.Timeout = Timeout.InfiniteTimeSpan;
				client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
				client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
				client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
				client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("ReconVersionsave", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}

		public class ReconVersionmodelsave
		{
			public string in_recon_code { get; set; }
			public string in_rule_code { get; set; }
			public string in_reconrule_version { get; set; }
			public string in_theme_code { get; set; }
			public string in_preprocess_code { get; set; }
			public string in_user_code { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		#endregion

	}
}
