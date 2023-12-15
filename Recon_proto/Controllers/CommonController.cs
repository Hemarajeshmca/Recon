using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;

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
            model.in_ip_addr = "localhost";
            model.in_source_name = "API";
            model.in_proc_name = proc_name;
            model.user_code = "Hema";
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
	}
}
