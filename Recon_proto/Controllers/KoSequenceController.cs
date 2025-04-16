using DocumentFormat.OpenXml.Bibliography;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;

namespace Recon_proto.Controllers
{
    public class KoSequenceController : Controller
    {
        private IConfiguration _configuration;
        public KoSequenceController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult KoSequencelist()
        {
            return View();
        }
        #region Qcd combo
        [HttpPost]
        public JsonResult seqtypefetch([FromBody] koseqmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();           
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "KoSequence/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("http://localhost:4195/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("seqtype", content).Result;
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
                objcom.errorlog(ex.Message, "Reconlistfetch");
                return Json(ex.Message);
            }
        }

        public class koseqmodel
        {
            public string? in_recon_code { get; set; }
            public string? in_seq_code { get; set; }
            public string? in_user_code { get; set; }
        }
        #endregion

        #region save
        [HttpPost]
        public JsonResult koseqsave([FromBody] koseqsavemodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            koseqsavemodel objList = new koseqsavemodel();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "KoSequence/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_action_by);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("KoSequenceSave", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        objList.in_Koseq_gid = Convert.ToInt16(result.Rows[i]["in_koseq_gid"]);                       
                        objList.out_msg = result.Rows[i]["out_msg"].ToString();
                        objList.out_result = Convert.ToInt16(result.Rows[i]["out_result"].ToString());
                    }
                    return Json(objList);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Reconheadersave");
                return Json(ex.Message);
            }
        }

        public class koseqsavemodel
        {
            public Int32? in_Koseq_gid { get; set; }
            public String in_recon_code { get; set; }
            public String in_recon_version { get; set; }
            public decimal in_koseq_no { get; set; }
            public String? in_koseq_type { get; set; }
            public String in_koseq_ref_code { get; set; }
            public String in_hold_flag { get; set; }            
            public String in_action { get; set; }
            public String in_active_status { get; set; }
            public String? in_action_by { get; set; }
            public String? out_msg { get; set; }
            public Int32? out_result { get; set; }
        }
        #endregion

        #region list fetch
        [HttpPost]
        public JsonResult seqlistfetch([FromBody] koseqlistmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "KoSequence/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("http://localhost:4195/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("seqlistfetch", content).Result;
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
                objcom.errorlog(ex.Message, "Reconlistfetch");
                return Json(ex.Message);
            }
        }

        public class koseqlistmodel
        {
            public string? in_recon_code { get; set; }
            public string? in_user_code { get; set; }
        }
        #endregion
    }
}
