using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using static Recon_proto.Controllers.DataSetController;
using static Recon_proto.Controllers.ReconController;

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

		[HttpPost]
		public JsonResult jobtypelist()
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
			List<jobtype> objcat_lst = new List<jobtype>();
			string post_data = "";
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

		public class jobtype
		{
			public string? jobtype_desc { get; set; }
			public string? jobtype_code { get; set; }
		}


		[HttpPost]
		public JsonResult getjobinprogresslist([FromBody] Jobstatusmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			Jobstatusmodel objList = new Jobstatusmodel();
			DataTable result = new DataTable();
			List<Joblistmodel> objcat_lst = new List<Joblistmodel>();
			string post_data = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "Utility/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				//client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
				client.DefaultRequestHeaders.Accept.Clear();
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

		[HttpPost]
		public JsonResult Joblistfetch([FromBody] Jobstatusmodel context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Jobstatusmodel objList = new Jobstatusmodel();
			DataTable result = new DataTable();
			List<Joblistmodel> objcat_lst = new List<Joblistmodel>();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Utility/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
                client.DefaultRequestHeaders.Accept.Clear();
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


		public IActionResult Downloads(string jobid)
		{
            urlstring = _configuration.GetSection("Appsettings")["filedownload"].ToString();
            fileModel FileDownloadgrid = new fileModel();
			//string filepath = "/new_volume/tmp/mysqlrpt/flexi_recon/";
			string filepath = "/new_volume/tmp/mysqlrpt/flexi_recon/";
			FileDownloadgrid.jobGid= jobid;
			FileDownloadgrid.jobName= "";
			FileDownloadgrid.filePath = filepath.Replace("'","");

			using (var client = new HttpClient())
			{
				string[] result = { };
                client.BaseAddress = new Uri(urlstring);
                //client.BaseAddress = new Uri("http://146.56.55.230:9091/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				//var json = JsonConvert.SerializeObject(FileDownloadgrid);
				HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownloadgrid), UTF8Encoding.UTF8, "application/json");
				content.Headers.Add("user_code", "12345");

				var response = client.PostAsync("files", content).Result;

				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				string base64data = string.Empty;

				var bytes = new byte[data.Length];
				data.Read(bytes, 0, bytes.Length);
				//byte[] bytes = Convert.FromBase64String(reader.ReadLine().Replace('"', ' '));
				var responses = new FileContentResult(bytes, "application/octet-stream");
				//responses.FileDownloadName = Job_name;

				var fileName = jobid.ToString() + ".zip";


				var contentDisposition = new ContentDisposition
				{
					FileName = jobid,
					Inline = true,
				};

				Response.Clear();
				//Response.AddHeader("content-disposition", "inline; filename=" + Job_gid.ToString() + ConfigurationManager.AppSettings["downloadFiletype"].ToString());
				Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
				Response.Headers.Add("Content-Type", "application/octet-stream");
				return File(bytes, "application/octet-stream", fileName);


				//try
				//{
				//	var (fileType, archiveData, archiveName) = _fileservice.DownloadFiles(filepath, jobid);

				//	return File(archiveData, fileType, archiveName);
				//}
				//catch (Exception ex)
				//{
				//	return BadRequest(ex.Message);
				//}
			}

		}

		public class fileModel
		{
			public String? jobGid { get; set; }

			public string? jobName { get; set; }

			public String? filePath { get; set; }
		}
	
	}
}

