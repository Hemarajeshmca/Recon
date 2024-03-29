﻿using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.OleDb;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Recon_proto.Controllers
{
    public class UtilityController : Controller
    {
        Fileservice _fileservice = new Fileservice();
        private IConfiguration _configuration;
        public UtilityController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult InProgress()
        {
            return View();
        }
        public IActionResult ProcessCompleted()
        {
            return View();
        }

        #region jobtypelist

        [HttpPost]
        public JsonResult jobtypelist()
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            List<jobtype> objcat_lst = new List<jobtype>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Utility/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
                    var response = client.GetAsync("jobtype").Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        jobtype objcat = new jobtype();
                        objcat.jobtype_code = result.Rows[i]["jobtype_code"].ToString();
                        objcat.jobtype_desc = result.Rows[i]["jobtype_desc"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "jobtypelist");
                return Json(ex.Message);
            }
        }

        public class jobtype
        {
            public string? jobtype_desc { get; set; }
            public string? jobtype_code { get; set; }
        }
        #endregion

        #region getjobinprogresslist
        [HttpPost]
        public JsonResult getjobinprogresslist([FromBody] Jobstatusmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Jobstatusmodel objList = new Jobstatusmodel();
            DataTable result = new DataTable();
            List<Joblistmodel> objcat_lst = new List<Joblistmodel>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Utility/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("jobinpogress", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        Joblistmodel objcat = new Joblistmodel();
                        objcat.job_gid = Convert.ToInt32(result.Rows[i]["job_gid"]);
                        objcat.jobtype_code = result.Rows[i]["jobtype_code"].ToString();
                        objcat.job_name = result.Rows[i]["job_name"].ToString();
                        objcat.start_date = result.Rows[i]["start_date"].ToString();
                        objcat.end_date = result.Rows[i]["end_date"].ToString();
                        objcat.job_remark = result.Rows[i]["job_remark"].ToString();
                        objcat.jobstatus_desc = result.Rows[i]["jobstatus_desc"].ToString();
                        objcat.jobtype_desc = result.Rows[i]["jobtype_desc"].ToString();
                        objcat.recon_code = result.Rows[i]["recon_code"].ToString();
                        objcat.recon_name = result.Rows[i]["recon_name"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getjobinprogresslist");
                return Json(ex.Message);
            }
        }
        #endregion

        #region Joblistfetch
        [HttpPost]
        public JsonResult Joblistfetch([FromBody] Jobstatusmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Jobstatusmodel objList = new Jobstatusmodel();
            DataTable result = new DataTable();
            List<Joblistmodel> objcat_lst = new List<Joblistmodel>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Utility/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("jobcompleted", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        Joblistmodel objcat = new Joblistmodel();
                        objcat.job_gid = Convert.ToInt32(result.Rows[i]["job_gid"]);
                        objcat.jobtype_code = result.Rows[i]["jobtype_code"].ToString();
                        objcat.job_name = result.Rows[i]["job_name"].ToString();
                        objcat.start_date = result.Rows[i]["start_date"].ToString();
                        objcat.end_date = result.Rows[i]["end_date"].ToString();
                        objcat.job_remark = result.Rows[i]["job_remark"].ToString();
                        objcat.jobstatus_desc = result.Rows[i]["jobstatus_desc"].ToString();
                        objcat.jobtype_desc = result.Rows[i]["jobtype_desc"].ToString();
                        objcat.recon_code = result.Rows[i]["recon_code"].ToString();
                        objcat.recon_name = result.Rows[i]["recon_name"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Joblistfetch");
                return Json(ex.Message);
            }
        }
        public class Joblistmodel
        {
            public int job_gid { get; set; }
            public String? jobtype_code { get; set; }
            public String? job_name { get; set; }
            public String? start_date { get; set; }
            public String? end_date { get; set; }
            public String? job_remark { get; set; }
            public String? jobstatus_desc { get; set; }
            public String? jobtype_desc { get; set; }
            public String? recon_code { get; set; }
            public String? recon_name { get; set; }
        }

        public class Jobstatusmodel
        {
            public String? in_start_date { get; set; }
            public String? in_end_date { get; set; }
            public String? in_jobtype_code { get; set; }
            public String? in_jobstatus { get; set; }
        }
        #endregion

        #region Downloads

        public JsonResult getfilepath()
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            fileconfigmodel FileDownload = new fileconfigmodel();

            var context = _configuration.GetSection("Appsettings")["fileconfig_value"].ToString();
            FileDownload.in_config_name = context;
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
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownload), UTF8Encoding.UTF8, "application/json");
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
                objcom.errorlog(ex.Message, "Datasetdetail");
                return Json(ex.Message);
            }
        }

        public IActionResult Downloads(string jobid)
        {
            var out_result = getfilepath();
            List<fileconfigmodel> myObjects = JsonConvert.DeserializeObject<List<fileconfigmodel>>(out_result.Value.ToString());
            urlstring = _configuration.GetSection("Appsettings")["filedownload"].ToString();
            fileModel FileDownloadgrid = new fileModel();
            string filepath = "";
            if (myObjects.Count > 0)
            {
                filepath = myObjects[0].out_config_value;
            }
            FileDownloadgrid.jobGid = jobid;
            FileDownloadgrid.jobName = "";
            FileDownloadgrid.filePath = filepath.Replace("'", "");
            try
            {
                using (var client = new HttpClient())
                {
                    string[] result = { };
                    client.BaseAddress = new Uri(urlstring);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownloadgrid), UTF8Encoding.UTF8, "application/json");
                    content.Headers.Add("user_code", HttpContext.Session.GetString("user_code"));
                    var response = client.PostAsync("files", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    string base64data = string.Empty;
                    var bytes = new byte[data.Length];
                    data.Read(bytes, 0, bytes.Length);
                    var responses = new FileContentResult(bytes, "application/octet-stream");
                    var fileName = jobid.ToString() + ".zip";
                    var contentDisposition = new ContentDisposition
                    {
                        FileName = jobid,
                        Inline = true,
                    };

                    Response.Clear();
                    Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
                    Response.Headers.Add("Content-Type", "application/octet-stream");
                    return File(bytes, "application/octet-stream", fileName);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "files");
                return Json(ex.Message);
            }
        }
        public class fileModel
        {
            public String? jobGid { get; set; }

            public string? jobName { get; set; }

            public String? filePath { get; set; }
        }
        public class fileconfigmodel
        {
            public string? in_config_name { get; set; }
            public string? out_config_value { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }

        }

        #endregion

    }
}

