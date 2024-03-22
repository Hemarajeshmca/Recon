using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Recon_proto.Models;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using static Recon_proto.Controllers.LoginController;

namespace Recon_proto.Controllers
{
	public class DataSetController : Controller
	{
        private IConfiguration _configuration;
        public DataSetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult DataSetList()
		{
			return View();
		}
		public IActionResult DataSetForm()
		{
			return View();
		}

        #region Datasetlistfetch

        [HttpPost]
		public JsonResult Datasetlistfetch([FromBody] Datasetlistmodel context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Datasetlistmodel objList = new Datasetlistmodel();
			DataTable result = new DataTable();
			List<Datasetlistmodel> objcat_lst = new List<Datasetlistmodel>();
			string post_data = "";
			try
			{
                using (var client = new HttpClient())
                {
                    string Urlcon = "Dataset/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("DatasetRead", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                   string d2 = JsonConvert.DeserializeObject<string>(post_data);
					return Json(d2);
				}
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Datasetlistfetch");
                return Json(ex.Message);
            }

        }
		public class Datasetlistmodel
		{
			public string? dataset_name { get; set; }
			public string? datasetCode { get; set; }
			public int dataset_gid { get; set; }
			public string? dataset_category { get; set; }
			public string? active_status { get; set; }
			public string? active_status_desc { get; set; }
			public int in_user_gid { get; set; }
			public string? in_user_code { get; set; }
			public string? in_active_status { get; set; }
			public string? sl_no { get; set; }
			public string? last_sync_date { get; set; }
			public string? last_sync_status { get; set; }
            public string? completed_date { get; set; }
            public string? job_remark { get; set; }

        }
        #endregion

        #region Datasetheader
        [HttpPost]
		public JsonResult Datasetheader([FromBody] DatasetHeadermodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DatasetHeadermodel objList = new DatasetHeadermodel();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
                using (var client = new HttpClient())
                {
                    string Urlcon = "Dataset/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("DatasetHeader", content).Result;
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
                objcom.errorlog(ex.Message, "Datasetheader");
                return Json(ex.Message);
            }
        }
		public class DatasetHeadermodel
		{
			public string? dataset_name { get; set; }
			public string? datasetCode { get; set; }
			public Int32 dataset_id { get; set; }
			public string? dataset_category { get; set; }
            public string? clone_dataset { get; set; }
            public string? active_status { get; set; }
			public string? inactive_reason { get; set; }
			public string? in_action { get; set; }
			public string? in_action_by { get; set; }

		}
        #endregion

        #region Datasetdetail

        [HttpPost]
		public JsonResult Datasetdetail([FromBody] Datasetdetailrmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			Datasetdetailrmodel objList = new Datasetdetailrmodel();
			DataTable result = new DataTable();
			string post_data = "";
			try
			{
                using (var client = new HttpClient())
                {
                    string Urlcon = "Dataset/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    // client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("DatasetDetail", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        objList.datasetdetail_id = Convert.ToInt32(result.Rows[i]["in_datasetfield_gid"]);
                        objList.out_msg = result.Rows[i]["out_msg"].ToString();
                        objList.out_result = result.Rows[i]["out_result"].ToString();
                    }
                    return Json(objList);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Datasetdetail");
                return Json(ex.Message);
            }

        }
		public class Datasetdetailrmodel
		{
			public string? datasetCode { get; set; }
			public int datasetdetail_id { get; set; }
			public string? field_name { get; set; }
			public string? field_length { get; set; }
            public int? precision_length { get; set; } = 0;
            public int? scale_length { get; set; } = 0;
			public string? field_type { get; set; }
			public string? field_mandatory { get; set; }
			public string? in_action { get; set; }
			public string? in_action_by { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
        #endregion

        #region Datasetdetailreadlist

        [HttpPost]
		public JsonResult Datasetdetailreadlist([FromBody] Datasetdetailfetch context)
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
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("DatasetReaddetail", content).Result;
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
                objcom.errorlog(ex.Message, "Datasetdetailreadlist");
                return Json(ex.Message);
            }
          
		}

		public class Datasetdetailfetch
		{
			public Int16 dataset_gid { get; set; }			
		}
        #endregion

        #region getfieldtype
        [HttpPost]
		public JsonResult getfieldtype()
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<fieldtype> objcat_lst = new List<fieldtype>();
			string post_data = "";
			try
			{
                using (var client = new HttpClient())
                {
                    string Urlcon = "Dataset/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/DataSet/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getfieldtype", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        fieldtype objcat = new fieldtype();
                        objcat.fieldtype_gid = Convert.ToInt32(result.Rows[i]["fieldtype_gid"]);
                        objcat.fieldtype_code = result.Rows[i]["fieldtype_code"].ToString();
                        objcat.fieldtype_desc = result.Rows[i]["fieldtype_desc"].ToString();
                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getfieldtype");
                return Json(ex.Message);
            }
          
		}
		
		public class fieldtype
		{
			public string fieldtype_desc { get; set; }
			public string fieldtype_code { get; set; }
			public int fieldtype_gid { get; set; }
		}
        #endregion

        #region getPipelinelist
        [HttpPost]
		public JsonResult getPipelinelist([FromBody] pipelinelist context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			DataTable result = new DataTable();
			List<pipelinemodal> objcat_lst = new List<pipelinemodal>();
			string post_data = "";

			try
			{
                using (var client = new HttpClient())
                {
                    string Urlcon = "Process/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/DataSet/");
                    client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("getPipelinelist", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        pipelinemodal objcat = new pipelinemodal();
                        objcat.pipeline_gid = Convert.ToInt32(result.Rows[i]["pipeline_gid"]);
                        objcat.pipeline_code = result.Rows[i]["pipeline_code"].ToString();
                        objcat.pipeline_desc = result.Rows[i]["pipeline_desc"].ToString();
                        objcat.pipeline_name = result.Rows[i]["pipeline_name"].ToString();
                        objcat.pipeline_status = result.Rows[i]["pipeline_status"].ToString();

                        objcat_lst.Add(objcat);
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "getPipelinelist");
                return Json(ex.Message);
            }
        }

		public class pipelinelist
		{
			public string? in_target_dataset_code { get; set; }
		}

		public class pipelinemodal
		{
			public int pipeline_gid { get; set; }
			public string pipeline_code { get; set; }
			public string pipeline_desc { get; set; }
			public string pipeline_name { get; set; }
			public string pipeline_status { get; set; }
		}
        #endregion


    }
}
