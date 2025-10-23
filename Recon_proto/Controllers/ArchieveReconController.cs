using DocumentFormat.OpenXml.Bibliography;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Recon_proto.Controllers
{
    public class ArchieveReconController : Controller
    {

        private IConfiguration _configuration;
        public ArchieveReconController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult ArchieveRecon()
        {
            return View();
        }
        public IActionResult archivalsetup()
        {
            return View();
        }

        #region ArcheiveRecon
        public class ArcheiveReconModel
        {
            public string in_recon_code { get; set; }
            public string? in_user_code { get; set; }
        }
        [HttpPost]
        public JsonResult ArcheiveRecon([FromBody] ArcheiveReconModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Recon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("ArcheiveRecon", content).Result;
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
                objcom.errorlog(ex.Message, "ArcheiveRecon");
                return Json(ex.Message);
            }

        }
        #endregion

        #region ArcheiveReconlist
        public class ArcheiveReconlistModel
        {
            public string in_recon_code { get; set; }
            public string? in_user_code { get; set; }
        }
        [HttpPost]
        public JsonResult ArcheiveReconlist([FromBody] ArcheiveReconlistModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Recon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("ArcheiveReconlist", content).Result;
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
                objcom.errorlog(ex.Message, "ArcheiveRecon");
                return Json(ex.Message);
            }

        }
        #endregion

        #region Archeivedatasetlist
        public class ArcheivedatasetlistModel
        {
            public string in_recon_code { get; set; }
            public string? in_user_code { get; set; }
        }
        [HttpPost]
        public JsonResult Archeivedatasetlist([FromBody] ArcheivedatasetlistModel context)
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
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("Archeivedatasetlist", content).Result;
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
                objcom.errorlog(ex.Message, "ArcheiveRecon");
                return Json(ex.Message);
            }

        }
        #endregion

        #region archivaldataset save
        public class archivaldataset
        {
            public int? in_archivaldataset_gid { get; set; }
            public int? in_archivaldatasetfilter_gid { get; set; }
            public string? in_dataset_code { get; set; }           
            public string? in_recon_code { get; set; }
            public string? in_dataset_type { get; set; }
            public string? in_archival_flag { get; set; }
            public Decimal? in_filter_seqno { get; set; }
            public string? in_filter_field { get; set; }
            public string? in_filter_criteria { get; set; }
            public string? in_filter_value_flag { get; set; }
            public string? in_filter_value { get; set; }
            public string? in_open_parentheses_flag { get; set; }
            public string? in_close_parentheses_flag { get; set; }
            public string? in_join_condition { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_user_code { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }
        [HttpPost]
        public JsonResult archivaldatasetsave([FromBody] archivaldataset context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            archivaldataset objList = new archivaldataset();
            DataTable result = new DataTable();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                string Urlcon = "Common/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                client.DefaultRequestHeaders.Accept.Clear();
                client.Timeout = Timeout.InfiniteTimeSpan;
                client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("archivaldatasetsave", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objList.in_archivaldataset_gid = Convert.ToInt32(result.Rows[i]["in_archivaldataset_gid"]);
                    objList.out_msg = result.Rows[i]["out_msg"].ToString();
                    objList.out_result = result.Rows[i]["out_result"].ToString();
                }
                return Json(objList);
            }
        }
        #endregion

        #region Archeivedatasetfetch
        public class ArcheivedatasetfetchModel
        {
            public string in_recon_code { get; set; }
            public string? in_dataset_code { get; set; }
            public string? in_user_code { get; set; }
        }
        [HttpPost]
        public JsonResult Archeivedatasetfetch([FromBody] ArcheivedatasetfetchModel context)
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
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("Archeivedatasetfetch", content).Result;
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
                objcom.errorlog(ex.Message, "Archeivedatasetfetch");
                return Json(ex.Message);
            }

        }
        #endregion

    }
}
