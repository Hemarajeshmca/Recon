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
					string Urlcon = "Recon/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					//client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("Recondataset", content).Result;
					Stream data = response.Content.ReadAsStreamAsync().Result;
					StreamReader reader = new StreamReader(data);
					post_data = reader.ReadToEnd();
					string d2 = JsonConvert.DeserializeObject<string>(post_data);
					result = JsonConvert.DeserializeObject<DataTable>(d2);
					for (int i = 0; i < result.Rows.Count; i++)
					{
						objList.in_role_gid = Convert.ToInt16(result.Rows[i]["in_role_gid"]);
						objList.out_msg = result.Rows[i]["out_msg"].ToString();
						objList.out_result = Convert.ToInt16(result.Rows[i]["out_result"].ToString());
					}
					return Json(objList);
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
			public Int16? in_role_gid { get; set; }
			public String in_role_code { get; set; }
			public String in_role_name { get; set; }			
			public String in_active_status { get; set; }
			public String in_action { get; set; }
			public String? in_user_code { get; set; }
			public String? out_msg { get; set; }
			public Int16? out_result { get; set; }
		}
#endregion
	}
}
