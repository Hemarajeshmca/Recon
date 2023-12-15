using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.ReconController;
using static Recon_proto.Controllers.RulesetupController;

namespace Recon_proto.Controllers
{
	public class KnockOffController : Controller
	{
        private IConfiguration _configuration;
        public KnockOffController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult AutoRuleBase()
		{
			return View();
		}
		public IActionResult PreviewRuleBase()
		{
			return View();
		}
		public IActionResult ManualKO()
		{
			return View();
		}

		public IActionResult UndoKO()
		{
			return View();
		}

		public IActionResult UndoKObyJob()
		{
			return View();
		}

		public IActionResult KoFilebased()
		{
			return View();
		}

        public IActionResult ManualMatching()
        {
            return View();
        }

        //ManualMatchDetails

        public IActionResult ManualMatchDetails()
        {
            return View();
        }

        public IActionResult MIS()
        {
            return View();
        }

        #region previewRulebase

        public class getpreviewRulebase
        {
            public String? in_recon_code { get; set; }
            public String? in_period_from { get; set; }
            public String? in_period_to { get; set; }
            public String? in_automatch_flag { get; set; }
            public String? in_ip_addr { get; set; }
            public String? in_user_code { get; set; }
        }

        [HttpPost]
        public JsonResult previewRulebase([FromBody] getpreviewRulebase context)
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
                    string Urlcon = "Process/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Process/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runreconrule", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "previewRulebase");
                return Json(ex.Message);
            }
        }

        #endregion

        #region matchReconbase
        public JsonResult matchReconbase([FromBody] getpreviewRulebase context)
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
                    string Urlcon = "Process/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    // client.BaseAddress = new Uri("https://localhost:44348/api/Process/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("automatchpreview", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "matchReconbase");
                return Json(ex.Message);
            }
        }
        #endregion

        public ActionResult DownloadFile(string Subfolder, string Excelfiles)
        {
            string Result = "";
            byte[] filebyte;
           // string filepath1 = @"D:\MonthEndReport\" + Subfolder + "\\" + Excelfiles + "";
            string filepath1 = @"D:\recon\Training.xlsx";
            string subDirectory = filepath1;

            try
            {
                filebyte = GetFile(subDirectory);
                return File(filebyte, System.Net.Mime.MediaTypeNames.Application.Octet, "Recons.xlsx");
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "DownloadFile");
                return Json(ex.Message);
            }
            
        }

        byte[] GetFile(string s)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(s);
            byte[] data = new byte[fs.Length];
            int br = fs.Read(data, 0, data.Length);
            if (br != fs.Length)
                throw new System.IO.IOException(s);
            return data;
        }

        #region RuleAgainstRecon
        [HttpPost]
        public JsonResult RuleAgainstRecon([FromBody] Ruleagainstrecon context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            string d2 = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Rulesetup/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("http://localhost:44348/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getruleagainstRecon", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataSet>(d2);
                    var rr = result.Tables.Count;
                    if (rr <= 0)
                    {
                        d2 = "";
                    }
                    return Json(d2);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "RuleAgainstRecon");
                return Json(ex.Message);
            }
        }
        #endregion

        #region ManualMatchList
        [HttpPost]

        public JsonResult ManualMatchList()
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            string d2 = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "DatasettoRecon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("DatasettoManualRead", content).Result;
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
                objcom.errorlog(ex.Message, "ManualMatchList");
                return Json(ex.Message);
            }

        }

        #endregion


        #region ManualMatchDetails
        [HttpPost]
        public JsonResult ManualMatchDetails([FromBody] manualmatchinfo context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            string d2 = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "DatasettoRecon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("ManuaMatchInfo", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "ManualMatchDetails");
                return Json(ex.Message);
            }
        }
      
        public class manualmatchinfo
        {
            public int in_scheduler_gid { get; set; }
        }
        #endregion

        #region runmanualfile
        [HttpPost]
        public JsonResult runmanualfile([FromBody] manualfile context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            string d2 = "";
            try {
                using (var client = new HttpClient())
                {
                    string Urlcon = "DatasettoRecon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runManualfile", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            }  catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "RuleAgainstRecon");
                return Json(ex.Message);
            }
        }

        public class manualfile
        {
            public int in_scheduler_gid { get; set; }
            public string in_ip_addr { get; set; }  
        }

        #endregion

        #region runkosum
        [HttpPost]
		public JsonResult runkosum([FromBody] runkosummodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataSet result = new DataSet();
			string post_data = "";
			string d2 = "";
			try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "knockoff/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runkosumm", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    return Json(d2);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "RuleAgainstRecon");
                return Json(ex.Message);
            }
        }

        public class runkosummodel
        {
            public String in_recon_code { get; set; }
            public String in_period_from { get; set; }
            public String in_period_to { get; set; }
            public String in_ip_addr { get; set;}
            public String in_user_code { get; set; }
        }
        #endregion
    }
}