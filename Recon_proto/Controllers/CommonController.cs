using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.KnockOffController;

namespace Recon_proto.Controllers
{
	public class CommonController : Controller
	{
        private IConfiguration _configuration;
        public CommonController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult Index()
		{
			return View();
		}

        #region Qcdmaster
        [HttpPost]
		public JsonResult Qcdmaster([FromBody] Qcdgridread context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
			List<mainQCDMaster> objcat_lst = new List<mainQCDMaster>();
			string post_data = "";
			try
			{
                using (var client = new HttpClient())
                {
                    string Urlcon = "Qcdmaster/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("http://localhost:4195/api/Qcdmaster/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                errorlog(ex.Message, "QcdMasterGridRead");
                return Json(ex);
            }
        }
        #endregion

        #region errorlog

        [HttpPost]
        public JsonResult errorlog(string message, string proc_name)
        {
            errormodel model = new errormodel();
            model.in_errorlog_text = message;
            model.in_ip_addr = "";
            model.in_source_name = "";
            model.in_proc_name = proc_name;
            model.user_code = "";
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            List<mainQCDMaster> objcat_lst = new List<mainQCDMaster>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Common/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(model), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("errorLog", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }


        public class errormodel
        {
            public string in_ip_addr { get; set; }
            public string in_source_name { get; set; }
            public string in_proc_name { get; set; }
            public string in_errorlog_text { get; set; }
            public string user_code { get; set; }
        }

        #endregion

        #region masterlist
        public class Qcdgridread
		{
			public string in_user_code { get; set; }
			public string in_master_code { get; set; }
		}
		public class mainQCDMaster
		{
			public string active_status_desc { get; set; }
			public string active_status { get; set; }
			public string masterCode { get; set; }
			public int master_gid { get; set; }
			public string mastermutiplename { get; set; }
			public string masterName { get; set; }
			public string masterShortCode { get; set; }
			public string masterSyscode { get; set; }
			public string ParentMasterSyscode { get; set; }
			public string and { get; set; }
			public string or { get; set; }
		}
        #endregion

        #region Dashboard Information
        public class dashboardinfo
        {
            public string in_recon_code { get; set; }
            public string in_period_from {get; set; }
            public string in_period_to { get; set; }
        }

        [HttpPost]
        public JsonResult GetDashboarddtl([FromBody] dashboardinfo context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            string d2 = "";
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
                    var response = client.PostAsync("dashboard", content).Result;
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
                objcom.errorlog(ex.Message, "dashboard");
                return Json(ex.Message);
            }
        }
		#endregion

		#region rolevalidate
		[HttpPost]
		public JsonResult rolevalidate([FromBody] rolevalidatemodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			rolevalidatemodel role = new rolevalidatemodel();
            role.in_screen_code=context.in_screen_code;
			role.add = "";
			role.edit = "";
			role.delete = "";
			role.view = "";
			role.process = "";
			role.download = "";
			role.deny = "";
			List<rolevalidatemodel> objcat_lst = new List<rolevalidatemodel>();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Common/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(role), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("roleconfig", content).Result;
					Stream data = response.Content.ReadAsStreamAsync().Result;
					StreamReader reader = new StreamReader(data);
					post_data = reader.ReadToEnd();
					string d2 = JsonConvert.DeserializeObject<string>(post_data);
					result = JsonConvert.DeserializeObject<DataTable>(d2);
					for (int i = 0; i < result.Rows.Count; i++)
					{
						rolevalidatemodel objcat = new rolevalidatemodel();
						objcat.in_screen_code = result.Rows[i]["menu_code"].ToString();
						objcat.add = result.Rows[i]["add_flag"].ToString();
						objcat.edit = result.Rows[i]["modify_flag"].ToString();
						objcat.delete = result.Rows[i]["deleteflag"].ToString();
						objcat.view = result.Rows[i]["view_flag"].ToString();
						objcat.process = result.Rows[i]["process_flag"].ToString();
						objcat.download = result.Rows[i]["download_flag"].ToString();
						objcat.deny = result.Rows[i]["deny_flag"].ToString();
						objcat_lst.Add(objcat);
					}
					return Json(objcat_lst);
				}
			}
			catch (Exception ex)
			{
				errorlog(ex.Message, "rolevalidate");
				return Json(ex);
			}
		}
		public class rolevalidatemodel
		{
			public string in_screen_code { get; set; }
			public string add { get; set; }
			public string edit { get; set; }
			public string delete { get; set; }
			public string view { get; set; }
			public string process { get; set; }
			public string download { get; set; }
			public string deny { get; set; }
		}
		#endregion

		#region getconfig
		public class configvalueModel
		{
			public string in_config_name { get; set; }
		}
		public JsonResult getconfigvalue([FromBody] configvalueModel confing_val)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Common/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(confing_val), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("configvalue", content).Result;
					Stream data = response.Content.ReadAsStreamAsync().Result;
					StreamReader reader = new StreamReader(data);
					post_data = reader.ReadToEnd();
					string d2 = JsonConvert.DeserializeObject<string>(post_data);
					result = JsonConvert.DeserializeObject<DataTable>(d2);
					return Json(d2);
				}
			}
			catch (Exception ex)
			{
				CommonController objcom = new CommonController(_configuration);
				objcom.errorlog(ex.Message, "getconfigvalue");
				return Json(ex.Message);
			}
		}
		#endregion

		#region Reconmindate
		public class Reconmindate
		{
			public string in_recon_code { get; set; }
		}
		public JsonResult reconmindate([FromBody] Reconmindate recon_date)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
				using (var client = new HttpClient())
				{
					string Urlcon = "Common/";
					client.BaseAddress = new Uri(urlstring + Urlcon);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(recon_date), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("reconmindate", content).Result;
					Stream data = response.Content.ReadAsStreamAsync().Result;
					StreamReader reader = new StreamReader(data);
					post_data = reader.ReadToEnd();
					string d2 = JsonConvert.DeserializeObject<string>(post_data);
					result = JsonConvert.DeserializeObject<DataTable>(d2);
					return Json(d2);
				}
			}
			catch (Exception ex)
			{
				CommonController objcom = new CommonController(_configuration);
				objcom.errorlog(ex.Message, "getconfigvalue");
				return Json(ex.Message);
			}
		}
		#endregion
	}
}
