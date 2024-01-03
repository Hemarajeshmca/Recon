using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Recon_proto.Controllers.UtilityController;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Net;
using static Recon_proto.Controllers.DatasettoReconController;
using System.Net.Http;

namespace Recon_proto.Controllers
{
    public class DatasetfileController : Controller
    {
        private IConfiguration _configuration;
        public DatasetfileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult Inputfile()
        {
            return View();
        }

        public IActionResult deletedata()
        {
            return View();
        }
        public IActionResult Datasetconversion()
        {
            ViewBag.baseurl = _configuration.GetSection("Appsettings")["connectorUrl"].ToString();
            return View();
        }

        #region datasetfile
        [HttpPost]
        public async Task<string> datasetfile(string pipeline_code, IFormFile file, string initiated_by)
        {
            datasetfileModel objcontent = new datasetfileModel();
            objcontent.pipeline_code = pipeline_code;
            objcontent.file = file;
            objcontent.initiated_by = initiated_by;
            urlstring = _configuration.GetSection("Appsettings")["connectorUrl"].ToString();
            string post_data = "";
            var filepath = "Pipeline/NewScheduler";
            using (var client = new HttpClient())

            using (var content1 = new MultipartFormDataContent())
                try
                {
                    using (var fileStream = file.OpenReadStream())
                    {
                        string[] result = { };
                        client.Timeout = Timeout.InfiniteTimeSpan;
                        client.BaseAddress = new Uri(urlstring);
                        client.DefaultRequestHeaders.Accept.Clear();
						client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
						client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
						client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
						client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
						content1.Add(new StreamContent(fileStream), "file", file.FileName);
                        content1.Add(new StringContent(pipeline_code), "pipeline_code");
                        content1.Add(new StringContent(initiated_by), "initiated_by");
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(objcontent));
                        //var response = await client.PostAsync(filepath, content1).Result;
                        HttpResponseMessage response = await client.PostAsync(filepath, content1);
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        var res = "";
                        if (post_data != "")
                        {
                            //res = setProcessdataset(post_data);
                        }
                        return res;
                    }
                }
                catch (Exception ex)
                {
                    CommonController objcom = new CommonController(_configuration);
                    objcom.errorlog(ex.Message, "datasetfile");
                    return ex.Message;
                }

        }

        public string setProcessdataset(string gid)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            setProcessdatasetModel objcontent = new setProcessdatasetModel();
            objcontent.in_scheduler_gid = Convert.ToInt32(gid);
            //string hostName = Dns.GetHostName();
            //IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            //objcontent.in_ip_addr = addresses[0];
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "DatasettoRecon/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(objcontent), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("runProcessdataset", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    return d2;
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "setProcessdataset");
                return ex.Message;
            }


        }

        public class setProcessdatasetModel
        {
            public int in_scheduler_gid { get; set; }
            public string in_ip_addr { get; set; } = "localhost";

        }

        public class datasetfileModel
        {
            public string? pipeline_code { get; set; }
            public IFormFile file { get; set; }
            public string? initiated_by { get; set; }
        }

        #endregion

        #region getschedulerlist
        public string getschedulerlist([FromBody] getschedulerlistModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Dataset/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getScheduler", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    return d2;
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getschedulerlist");
                return ex.Message;
            }


        }

        public class getschedulerlistModel
        {
            public string in_processed_date { get; set; }
            public string? in_scheduler_status { get; set; }
        }

        #endregion

        #region 
        public string deletescheduler([FromBody] deleteschedulerModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Dataset/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", HttpContext.Session.GetString("user_code"));
					client.DefaultRequestHeaders.Add("lang_code", HttpContext.Session.GetString("lang_code"));
					client.DefaultRequestHeaders.Add("role_code", HttpContext.Session.GetString("role_code"));
					client.DefaultRequestHeaders.Add("ipaddress", HttpContext.Session.GetString("ipAddress"));
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("delScheduler", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    return d2;
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "delScheduler");
                return ex.Message;
            }


        }

        public class deleteschedulerModel
        {
            public int in_scheduler_gid { get; set; }
            public string? in_remark { get; set; }
        }
        #endregion
    }
}
