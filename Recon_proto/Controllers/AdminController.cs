using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using static Recon_proto.Controllers.DataSetController;

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
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("in_role_code", objrolefetch.in_role_code);
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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

		#region list
		[HttpPost]
		public JsonResult userlistfetch([FromBody] usermodellist context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
		public class usermodellist
		{
			public string user_code { get; set; }
			public string role_code { get; set; }
			public string lang_code { get; set; }
		}
		#endregion

		#region user save
		[HttpPost]
		public JsonResult usersave([FromBody] usermodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			var pass = Encrypt(context.user_password);
			usermodel usr = new usermodel();
			usr.user_password = pass;
			usr.user_code = context.user_code;
			usr.user_emailid = context.user_emailid;
			usr.user_name = context.user_name;
			usr.action_by = context.action_by;
			usr.action = context.action;
			usr.user_contact_no = context.user_contact_no;
			usr.user_gid = context.user_gid;
			usr.role_code = context.role_code;
			usr.in_active_reason = context.in_active_reason;
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(usr), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Usersave", content).Result;
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
				objcom.errorlog(ex.Message, "Usersave");
				return Json(ex.Message);
			}
		}
		public class usermodel
		{
			public string user_code { get; set; }
			public string user_contact_no { get; set; }
			public string user_emailid { get; set; }
			public int user_gid { get; set; }
			public string user_name { get; set; }
			public string user_password { get; set; }
			public string action_by { get; set; }
			public string in_active_reason { get; set; }
			public string action { get; set; }
			public string role_code { get; set; }
			public String? out_msg { get; set; }
			public Int16? out_result { get; set; }
		}
		#endregion

		#region treeview
		[HttpPost]
		public JsonResult treeviewread()
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<treeviewllist> objList = new List<treeviewllist>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("treeviewlist", content).Result;
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
		public class treeviewllist
		{
			public List<treeviewllist> items = new List<treeviewllist>();
			public string depend_code { get; set; }
			public string master_code { get; set; }
			public string master_name { get; set; }
			public string parent_code { get; set; }
			public string id { get; set; }			
		}
		public treeviewllist add_node(DataTable _dt, DataRow _dr)
		{
			treeviewllist node = new treeviewllist();
			try
			{
				node.id = _dr["id"].ToString();
				node.parent_code = _dr["parent_code"].ToString();
				node.master_code = _dr["master_code"].ToString();
				node.master_name = _dr["master_name"].ToString();
				node.depend_code = _dr["depend_code"].ToString();

				var rows = _dt.AsEnumerable()
							   .Where(r => r.Field<string>("depend_code") == node.master_code);

				foreach (DataRow _row in rows)
				{
					treeviewllist child1 = new treeviewllist();
					child1 = add_node(_dt, _row);
					node.items.Add(child1);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return node;
		}
		#endregion
		private string Encrypt(string clearText)
		{
			try
			{
				string EncryptionKey = "MAKV2SPBNI99212";
				byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
				using (Aes encryptor = Aes.Create())
				{
					Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
					encryptor.Key = pdb.GetBytes(32);
					encryptor.IV = pdb.GetBytes(16);
					using (MemoryStream ms = new MemoryStream())
					{
						using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cs.Write(clearBytes, 0, clearBytes.Length);
							cs.Close();
						}
						clearText = Convert.ToBase64String(ms.ToArray());
					}
				}
			}
			catch (Exception e)
			{
				return e.Message;
			}
			return clearText;
		}

		#region usermapping save
		[HttpPost]
		public JsonResult usermappingsave([FromBody] usermappingmodel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Usermappingsave", content).Result;
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
				objcom.errorlog(ex.Message, "Usersave");
				return Json(ex.Message);
			}
		}
		public class usermappingmodel
		{
			public string user_code { get; set; }			
			public int user_gid { get; set; }		
			public string level_mapping { get; set; }		
			public String? out_msg { get; set; }
			public Int16? out_result { get; set; }
		}
		#endregion

		#region getcheckednode
		[HttpPost]
		public JsonResult getcheckednode([FromBody] getcheckedmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<treeviewllist> objList = new List<treeviewllist>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("getcheckednode", content).Result;
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
		public class getcheckedmodel
		{
			public string user_code { get; set; }
			public int user_gid { get; set; }
		}
		#endregion

		#region context
		[HttpPost]
		public JsonResult setcontextread([FromBody] getcheckedmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<treeviewllist> objList = new List<treeviewllist>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("setcontext", content).Result;
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
		#endregion

		#region applycontext
		[HttpPost]
		public JsonResult applycontextsave([FromBody] applycontextmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<treeviewllist> objList = new List<treeviewllist>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("applycontext", content).Result;
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
		public class applycontextmodel
		{
			public string user_code { get; set; }
			public string level_mapping { get; set; }
			public String? out_msg { get; set; }
			public Int16? out_result { get; set; }
		}
		#endregion

		#region getcontext
		[HttpPost]
		public JsonResult getcontext([FromBody] getcontexttmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<treeviewllist> objList = new List<treeviewllist>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("getcontext", content).Result;
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
		public class getcontexttmodel
		{
			public string user_code { get; set; }	
		}
		#endregion

		#region getmenu
		[HttpPost]
		public JsonResult getmenu([FromBody] getmenutmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<treeviewllist> objList = new List<treeviewllist>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "UserManagement/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("getmenu", content).Result;
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
		public class getmenutmodel
		{
			public string user_code { get; set; }
		}
		#endregion
	}

}
