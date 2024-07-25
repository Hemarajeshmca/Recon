using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.IO.Compression;
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
						objcat.job_initiated_by = result.Rows[i]["job_initiated_by"].ToString();
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
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
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
						objcat.file_type = result.Rows[i]["file_type"].ToString();
                        objcat.job_initiated_by = result.Rows[i]["job_initiated_by"].ToString();
                        objcat.file_name = result.Rows[i]["file_name"].ToString();
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
            public String? file_type { get; set; }
            public string? job_initiated_by { get; set; }
            public string? file_name { get; set; }

        }

        public class Jobstatusmodel
        {
            public String? in_start_date { get; set; }
            public String? in_end_date { get; set; }
            public String? in_jobtype_code { get; set; }
            public String? in_jobstatus { get; set; }
			public string? in_user_code { get; set; }
		}
        #endregion

        #region Downloads

        public JsonResult getfilepath(string confing_val)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            fileconfigmodel FileDownload = new fileconfigmodel();

            var context = _configuration.GetSection("Appsettings")[confing_val];
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

        public IActionResult Downloads(string jobid, string filetype, string file_name)
        {
            var out_result = getfilepath("fileconfig_value");
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
                    if(filetype != "xlsx")
                    {
						Stream data = response.Content.ReadAsStreamAsync().Result;
						StreamReader reader = new StreamReader(data);
						string base64data = string.Empty;
						var bytes = new byte[data.Length];
						data.Read(bytes, 0, bytes.Length);
						var responses = new FileContentResult(bytes, "application/octet-stream");
						var fileName = file_name.ToString() + ".zip";
						var contentDisposition = new ContentDisposition
						{
							FileName = file_name,
							Inline = true,
						};
						Response.Clear();
						Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
						Response.Headers.Add("Content-Type", "application/octet-stream");
						return File(bytes, "application/octet-stream", fileName);
					} else
                    {
						var get_outresult = getfilepath("download_xls_folder");
						List<fileconfigmodel> obj_outresult = JsonConvert.DeserializeObject<List<fileconfigmodel>>(get_outresult.Value.ToString());
                        string out_filepath = "";
                        string fileName = "";
                        if (obj_outresult.Count > 0)
						{
							out_filepath = obj_outresult[0].out_config_value;
						}
                        if (file_name.ToLower().Contains(".xlsx"))
                        {
                            fileName = file_name;
                        }
                        else
                        {
                            fileName = file_name + ".xlsx";
                        }
						string filePath = Path.Combine(out_filepath, fileName);

						if (!System.IO.File.Exists(filePath))
						{
							return NotFound(); 
						}
						var zipName = $"{file_name}.zip";
						using (var memoryStream = new MemoryStream())
						{
							using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
							{
								var fileName1 = Path.GetFileName(filePath);
								var entry = archive.CreateEntry(fileName1, CompressionLevel.Optimal);

								using (var entryStream = entry.Open())
								using (var fileStream = System.IO.File.OpenRead(filePath))
								{
									fileStream.CopyTo(entryStream);
								}
							}

							memoryStream.Seek(0, SeekOrigin.Begin);
							return File(memoryStream.ToArray(), "application/zip", zipName);
						}
					}
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


		public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory, string y)
		{
			string jobid = y;
			int filelength = jobid.Length;
			var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
			var files = Directory.GetFiles(subDirectory).ToList();
			string supportedExtensions = String.Concat(jobid.ToString(), ".csv,", jobid.ToString(), "_*.*");

			List<string> myList = new List<string>();

			foreach (string file in Directory.GetFiles(subDirectory, String.Concat(jobid, ".*"), SearchOption.AllDirectories).Union(
									Directory.GetFiles(subDirectory, String.Concat(jobid, "_*.*"), SearchOption.AllDirectories)))
			{

				var fil = Path.GetFileName(file);
				string filess = file;
				myList.Add(filess);

			}


            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    myList.ForEach(file =>
                    {
                        var filename = Path.GetFileName(file);
                        var theFile = archive.CreateEntry(filename);
                        using (var streamWriter = new StreamWriter(theFile.Open()))
                        {
                            streamWriter.Write(System.IO.File.ReadAllText(file));
                        }
                    });
                }
                return ("application/zip", memoryStream.ToArray(), zipName);
            }

		}

	}
}

