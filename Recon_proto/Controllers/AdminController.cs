using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Recon_proto.Controllers
{
	public class AdminController : Controller
	{
		private IConfiguration _configuration;
		public AdminController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string urlstring = "";
		public IActionResult Users()
        {
            return View();
        }
		public IActionResult Role()
		{
			return View();
		}
		
		public IActionResult Usermapping()
		{
			return View();
		}

		public IActionResult Rolemapping()
		{
			return View();
		}

		#region rolesave
		[HttpPost]
		public JsonResult rolesave([FromBody] rolemodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			rolemodel objList = new rolemodel();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Roles/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                    client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                    client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                    client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Roles", content).Result;
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
				objcom.errorlog(ex.Message, "Recondatasetsave");
				return Json(ex.Message);
			}
		}

		public class rolemodel
		{
			public Int32 in_role_gid { get; set; }
			public string in_role_code { get; set; }
			public string in_role_name { get; set; }
			public string in_active_status { get; set; }
			public string in_active_reason { get; set; }
			public string in_action { get; set; }
		}
		#endregion

		#region list
		[HttpPost]
		public JsonResult userlistfetch([FromBody] usermodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();			
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("userlist", content).Result;
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
				objcom.errorlog(ex.Message, "userlistfetch");
				return Json(ex.Message);
			}
		}
		public class usermodel
		{
			public string user_code { get; set; }
			public string role_code { get; set; }
			public string lang_code { get; set; }
		}
		#endregion


		#region rolelist
		[HttpPost]
		public JsonResult rolelist()
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Roles/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Rolelist", content).Result;
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
				objcom.errorlog(ex.Message, "Rolelist");
				return Json(ex.Message);
			}
		}
		#endregion

		#region roledetails

		[HttpPost]
		public JsonResult roledetails([FromBody] roledetailsmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Roles/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Roledetails", content).Result;
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
				objcom.errorlog(ex.Message, "Roledetails");
				return Json(ex.Message);
			}
		}

		public class roledetailsmodel
		{
			public Int32 in_role_gid { get; set;}
		}
		#endregion

		#region rolefetch
		[HttpPost]
		public JsonResult rolefetch([FromBody] rolefetchModel objrolefetch)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Roles/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("in_role_code", objrolefetch.in_role_code);
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(objrolefetch), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Rolefetch", content).Result;
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
				objcom.errorlog(ex.Message, "Rolefetch");
				return Json(ex.Message);
			}
		}

		public class rolefetchModel
		{
			public string in_role_code { get; set;}
		}

		#endregion

		#region
		[HttpPost]
		public IActionResult saveRoleAccess([FromBody] saveroleAccessModel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Roles/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("saveRoleAccess", content).Result;
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
				objcom.errorlog(ex.Message, "saveRoleAccess");
				return Json(ex.Message);
			}
		}
		#endregion

		public class saveroleAccessModel
		{
			public string roledetails { get; set; }
		}

	}

}
