﻿using DocumentFormat.OpenXml.Bibliography;
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
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
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
		}
		#endregion
	}
}