using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlTypes;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.HomeController;
using static Recon_proto.Controllers.ReconController;
using static Recon_proto.Controllers.ReportsController;
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
            var conversionTypes = _configuration.GetSection("ConversionSettings:ConversionTypes").Get<List<ConversionType>>();
            var defaultType = _configuration["ConversionSettings:DefaultConversionType"];

            ViewBag.ConversionTypes = conversionTypes;
            ViewBag.DefaultConversionType = defaultType;
            return View();
        }

        public IActionResult UndoKObyRule()
        {
            return View();
        }

        public IActionResult AutoThemeBase()
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
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "runreconrule");
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
            }
            catch (Exception ex)
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
        public class ManualMatchListmodel
        {
            public String? in_user_code { get; set; }
        }

        [HttpPost]
        public JsonResult ManualMatchList(ManualMatchListmodel context)
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("ManuaMatchInfo", content).Result;
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
                objcom.errorlog(ex.Message, "ManualMatchDetails");
                return Json(ex.Message);
            }
        }

        public class manualmatchinfo
        {
            public int in_scheduler_gid { get; set; }
            public String? in_user_code { get; set; }
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
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "DatasettoRecon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runManualfile", content).Result;
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
                objcom.errorlog(ex.Message, "runmanualfile");
                return Json(ex.Message);
            }
        }

        public class manualfile
        {
            public int in_scheduler_gid { get; set; }
            public string in_ip_addr { get; set; }
            public String? in_user_code { get; set; }

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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runkosumm", content).Result;
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
                objcom.errorlog(ex.Message, "runkosum");
                return Json(ex.Message);
            }
        }

        public class runkosummodel
        {
            public String in_recon_code { get; set; }
            public String in_period_from { get; set; }
            public String in_period_to { get; set; }
            public String in_ip_addr { get; set; }
            public String in_user_code { get; set; }
            public String in_conversion_type { get; set; }
            public String in_dataset_formt { get; set; }
        }
        #endregion

        #region recondatasetinfo
        [HttpPost]
        public JsonResult recondatasetinfo([FromBody] recondatasetinfoModel context)
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("recondatasetinfo", content).Result;
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
                objcom.errorlog(ex.Message, "runProcessdataset");
                return Json(ex.Message);
            }
        }

        public class recondatasetinfoModel
        {
            public String in_recon_code { get; set; }
            public String in_dataset_code { get; set; }
            public String in_automatch_flag { get; set; }
            public String in_user_code { get; set; }
        }
        #endregion

        #region undorunreport
        [HttpPost]
        public JsonResult undorunreport([FromBody] undorunreportModel context)
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    try
                    {
                        var response = client.PostAsync("undorunreport", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        d2 = JsonConvert.DeserializeObject<string>(post_data);
                        return Json(d2);
                    }
                    catch (HttpRequestException ex)
                    {
                        return Json(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "undorunreport");
                return Json(ex.Message);
            }
        }

        public class undorunreportModel
        {
            public String in_recon_code { get; set; }
            public String in_report_code { get; set; }
            public String in_report_param { get; set; }
            public String in_report_condition { get; set; }
            public String in_ip_addr { get; set; }
            public Boolean in_outputfile_flag { get; set; }
            public String in_user_code { get; set; }
        }
        #endregion

        #region undoKO
        [HttpPost]
        public JsonResult undoKO([FromBody] undoKOModel context)
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("undoKO", content).Result;
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
                objcom.errorlog(ex.Message, "undoKO");
                return Json(ex.Message);
            }
        }

        public class undoKOModel
        {
            public String in_recon_code { get; set; }
            public int in_ko_gid { get; set; }
            public string in_undo_ko_reason { get; set; }
            public string in_user_code { get; set; }
        }
        #endregion

        #region undoKOjob
        [HttpPost]
        public JsonResult undoKOjob([FromBody] undoKOjobModel context)
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
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("undoKOjob", content).Result;
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
                objcom.errorlog(ex.Message, "undoKOjob");
                return Json(ex.Message);
            }
        }

        public class undoKOjobModel
        {
            public string in_recon_code { get; set; }
            public string in_job_type { get; set; }
            public string in_job_status { get; set; }
            public string in_from_date { get; set; }
            public string in_to_date { get; set; }
            public string in_user_code { get; set; }
        }

        [HttpPost]
        public JsonResult undomatchjob([FromBody] undomatchmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            undomatchmodel objList = new undomatchmodel();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "knockoff/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    // client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("undomatchjob", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        objList.out_msg = result.Rows[i]["out_msg"].ToString();
                        objList.out_result = result.Rows[i]["out_result"].ToString();
                    }
                    return Json(objList);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "undomatchjob");
                return Json(ex.Message);
            }

        }
        public class undomatchmodel
        {
            public string? reconcode { get; set; }
            public int job_id { get; set; }
            public string? undo_job_reason { get; set; }
            public string? in_ip_addr { get; set; }
            public string? in_user_code { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }
        #endregion

        #region getundoKOjobrule

        public class getundoKOjobrulemodel
        {
            public string? in_recon_code { get; set; }
            public string? in_start_date { get; set; }
            public string? in_end_date { get; set; }
            public string? in_user_code { get; set; }
        }

        [HttpPost]
        public JsonResult getundoKOjobrule([FromBody] getundoKOjobrulemodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "knockoff/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    // client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getundojobrule", content).Result;
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
                objcom.errorlog(ex.Message, "getundoKOjobrule");
                return Json(ex.Message);
            }

        }
        #endregion


        #region KObyRule

        public class kobyRuleModel
        {
            public string? in_recon_code { get; set; }
            public int? in_job_gid { get; set; }
            public string? in_rule_code { get; set; }
            public string? in_undo_job_reason { get; set; }
            public string? in_user_code { get; set; }

        }

        [HttpPost]
        public JsonResult kobyRule([FromBody] kobyRuleModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "knockoff/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    // client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("setundojobrule", content).Result;
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
                objcom.errorlog(ex.Message, "kobyRule");
                return Json(ex.Message);
            }

        }
        #endregion

        #region KOMISreport
        public ActionResult KOMISreport(string in_period_from, string in_period_to, string in_recon_code, string user_code)
        {

            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result2 = new DataSet();
            DataTable Table1 = new DataTable();
            string post_data = "";
            string d2 = "";
            int subtotal_count = 0;
            string filename = "Knockoff MIS.xlsx";
            try
            {
                using (var client = new HttpClient())
                {
                    runkosummodel report = new runkosummodel();
                    report.in_period_from = in_period_from;
                    report.in_period_to = in_period_to;
                    report.in_recon_code = in_recon_code;
                    report.in_ip_addr = "";
                    report.in_user_code = "";
                    string Urlcon = "knockoff/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(report), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runkosumm", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result2 = JsonConvert.DeserializeObject<DataSet>(d2);
                    Table1 = result2.Tables[0].Copy();
                    Table1.Columns.RemoveAt(5);
                    Table1.Columns.RemoveAt(5);
                    //Table1.Columns.RemoveAt(7);
                    //Table1.Columns.RemoveAt(7);
                    Table1.Columns[0].ColumnName = "Rows Labels";
                    Table1.Columns[1].ColumnName = "Debit Count";
                    Table1.Columns[2].ColumnName = "Debit Amount";
                    Table1.Columns[3].ColumnName = "Credit Count";
                    Table1.Columns[4].ColumnName = "Credit Amount";
                    //Table1.Columns[5].ColumnName = "Total Count";
                    //Table1.Columns[6].ColumnName = "Total Amount";

                    for (int i = 0; i < Table1.Rows.Count; i++)
                    {
                        string rowslabel = Table1.Rows[i]["Rows Labels"].ToString();
                        if (rowslabel.Contains("Sub Total"))
                        {
                            subtotal_count = i + 4;
                            break;
                        }

                    }

                    int rowscount = Table1.Rows.Count + 3;
                    string celltxt = "A" + rowscount.ToString() + "";
                    string celltxt2 = "E" + rowscount.ToString() + "";
                    //string celltxt2 = "G" + rowscount.ToString() + "";
                    string rangetxt = celltxt + ":" + celltxt2;
                    using (XLWorkbook wb = new XLWorkbook())
                    {

                        var ws = wb.AddWorksheet("Recon-Knockoff MIS");

                        //Insrting Table1 Data
                        ws.Cell("A1").SetValue("FLEXICODE BUSINESS FINANCE LTD"); ws.Cell("A1").Style.Font.Bold = true;
                        ws.Cell("A1").Style.Font.Underline = XLFontUnderlineValues.Single;
                        ws.Cell("A4").Style.Font.Bold = true; ws.Cell("A5").Style.Font.Bold = true;
                        ws.Cell("A3").InsertTable(Table1);
                        wb.Worksheet(1).Table(0).ShowAutoFilter = false;// Disable AutoFilter.
                        wb.Worksheet(1).Table(0).Theme = XLTableTheme.None;
                        wb.Worksheet(1).Table(0).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.BottomBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.TopBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.LeftBorderColor = XLColor.Black;
                        wb.Worksheet(1).Table(0).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        wb.Worksheet(1).Table(0).Style.Border.RightBorderColor = XLColor.Black;
                        ws.Range("A3:E3").Style.Font.Bold = true;
                        ws.Range("A3:E3").Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);
                        ws.Range(rangetxt).Style.Font.Bold = true;
                        ws.Range(rangetxt).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        rowscount = rowscount - 2;
                        celltxt = "A" + rowscount.ToString() + "";
                        celltxt2 = "E" + rowscount.ToString() + "";
                        rangetxt = celltxt + ":" + celltxt2;
                        ws.Range(rangetxt).Style.Font.Bold = true;
                        ws.Range(rangetxt).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        celltxt = "A" + subtotal_count.ToString() + "";
                        celltxt2 = "E" + subtotal_count.ToString() + "";
                        rangetxt = celltxt + ":" + celltxt2;
                        ws.Range(rangetxt).Style.Font.Bold = true;
                        ws.Range(rangetxt).Style.Fill.BackgroundColor = XLColor.FromTheme(XLThemeColor.Accent1, 0.5);

                        subtotal_count = subtotal_count + 1;
                        celltxt = "A" + subtotal_count.ToString() + "";
                        ws.Cell(celltxt).Style.Font.Bold = true;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "KOMISreport");
                return Json(ex.Message);
            }
        }

        #endregion


        #region getRuleAgainstJob
        [HttpPost]
        public JsonResult getRuleAgainstJob([FromBody] getRuleAgainstJobmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "knockoff/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    // client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getruleagainstjob", content).Result;
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
                objcom.errorlog(ex.Message, "getruleagainstjob");
                return Json(ex.Message);
            }

        }

        public class getRuleAgainstJobmodel
        {
            public int in_job_gid { get; set; }
            public string? in_user_code { get; set; }
        }

        #endregion


        #region ThemeAgainstRecon
        [HttpPost]
        public JsonResult ThemeAgainstRecon([FromBody] Themeagainstrecon context)
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
                    //client.BaseAddress = new Uri("http://localhost:44348/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getthemeagainstRecon", content).Result;
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
                objcom.errorlog(ex.Message, "ThemeAgainstRecon");
                return Json(ex.Message);
            }
        }

        public class Themeagainstrecon
        {
            public string? in_recon_code { get; set; }
            public string? in_user_code { get; set; }
        }


        #endregion

    }
}