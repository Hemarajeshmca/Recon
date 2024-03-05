using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

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
        public JsonResult Preprocesslistfetch()
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
                    client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
                    client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
                    client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
                    client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
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
       
        #endregion
    }
}
