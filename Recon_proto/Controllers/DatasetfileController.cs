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

		public IActionResult OpeningBalance()
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
			DataTable dat_tab = new DataTable();

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
						client.DefaultRequestHeaders.Add("user_code", initiated_by);
						client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
						client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
						client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
						content1.Add(new StreamContent(fileStream), "file", file.FileName);
						content1.Add(new StringContent(pipeline_code), "pipeline_code");
						content1.Add(new StringContent(initiated_by), "initiated_by");
						HttpContent content = new StringContent(JsonConvert.SerializeObject(objcontent));
						try
						{
							HttpResponseMessage response = await client.PostAsync(filepath, content1);
							Stream data = response.Content.ReadAsStreamAsync().Result;
							StreamReader reader = new StreamReader(data);
							post_data = reader.ReadToEnd();
							return post_data;
							//dynamic jsonObject = JsonConvert.DeserializeObject(post_data);
							//string message = jsonObject.message;
							//int result1 = jsonObject.result;
							//var res = message.ToString();
							//                     if (result1 == 1)
							//                     {
							//                         res = setProcessdataset(message);
							//                     }
							//                     return res;
						}
						catch (HttpRequestException ex)
						{
							return "Error";
						}
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
			public string in_ip_addr { get; set; }

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
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
			public string? in_user_code { get; set; }
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
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
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
			public string? in_user_code { get; set; }
		}
		#endregion


		#region datasetagainstRecon
		public string datasetAgainstRecon([FromBody] datasetAgainstReconModel context)
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
					client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("datasetAgainstRecon", content).Result;
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
				objcom.errorlog(ex.Message, "datasetAgainstRecon");
				return ex.Message;
			}
		}

		public class datasetAgainstReconModel
		{
			public string? in_recon_code { get; set; }
			public string? in_user_code { get; set; }
		}
		#endregion

		#region datasethistory
		public string datasethistory([FromBody] datasethistoryModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("datasethistory", content).Result;
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
				objcom.errorlog(ex.Message, "datasethistory");
				return ex.Message;
			}
		}

		public class datasethistoryModel
		{
			public string? in_dataset_code { get; set; }
		}
		#endregion


		#region getaccbaldataset

		public JsonResult getaccbaldataset([FromBody] getaccbaldatasetModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("getaccbaldataset", content).Result;
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
				objcom.errorlog(ex.Message, "datasetAgainstRecon");
				return Json(ex.Message);
			}


		}

		public class getaccbaldatasetModel
		{
			public string? in_dataset_code { get; set; }
		}

		#endregion

		#region setAccountbalance
		public JsonResult setAccountbalance([FromBody] setAccountbalanceModel context)
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
					client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
					var response = client.PostAsync("setAccountbalance", content).Result;
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
				objcom.errorlog(ex.Message, "setAccountbalance");
				return Json(ex.Message);
			}


		}

		public class setAccountbalanceModel
		{
			public int in_accbal_gid { get; set; }
			public String? in_dataset_code { get; set; }
			public String? in_tran_date { get; set; }
			public Double? in_bal_value { get; set; }
			public String? in_action { get; set; }

		}
		#endregion

		#region template_download

		public IActionResult TemplateDownload(string pipeline_code, string file_extension, string file_name, string username)
		{
			var out_result = getfilepath("template_download", username);
			List<fileconfigmodel> myObjects = JsonConvert.DeserializeObject<List<fileconfigmodel>>(out_result.Value.ToString());
			var folderPath = _configuration.GetSection("Appsettings")["template_download"].ToString();
			string filepath = "";
			if (myObjects.Count > 0)
			{
				filepath = myObjects[0].out_config_value;
			}
			try
			{
				string fileName = pipeline_code + file_extension; // Assuming the file extension is .csv; adjust as needed
				
				string filePath = Path.Combine(filepath, fileName);

				// Check if the file exists
				if (!System.IO.File.Exists(filePath))
				{
					return NotFound("File not found.");
				}

				// Read the file into a stream
				var fileBytes = System.IO.File.ReadAllBytes(filePath);

				// Return the file for download
				return File(fileBytes, "application/octet-stream", fileName);

				//using (var client = new HttpClient())
				//{
				//	string[] result = { };
				//	client.BaseAddress = new Uri(urlstring);
				//	client.DefaultRequestHeaders.Accept.Clear();
				//	client.Timeout = Timeout.InfiniteTimeSpan;
				//	client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				//	HttpContent content = new StringContent(JsonConvert.SerializeObject(FileDownloadgrid), UTF8Encoding.UTF8, "application/json");
				//	content.Headers.Add("user_code", username);
				//	var response = client.PostAsync("files", content).Result;
				//	if (filetype != "xlsx")
				//	{
				//		Stream data = response.Content.ReadAsStreamAsync().Result;
				//		StreamReader reader = new StreamReader(data);
				//		string base64data = string.Empty;
				//		var bytes = new byte[data.Length];
				//		data.Read(bytes, 0, bytes.Length);
				//		var responses = new FileContentResult(bytes, "application/octet-stream");
				//		var fileName = file_name.ToString() + ".zip";
				//		var contentDisposition = new ContentDisposition
				//		{
				//			FileName = file_name,
				//			Inline = true,
				//		};
				//		Response.Clear();
				//		Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
				//		Response.Headers.Add("Content-Type", "application/octet-stream");
				//		return File(bytes, "application/octet-stream", fileName);
				//	}
				//	else
				//	{
				//		var get_outresult = getfilepath("download_xls_folder", username);
				//		List<fileconfigmodel> obj_outresult = JsonConvert.DeserializeObject<List<fileconfigmodel>>(get_outresult.Value.ToString());
				//		string out_filepath = "";
				//		string fileName = "";
				//		if (obj_outresult.Count > 0)
				//		{
				//			out_filepath = obj_outresult[0].out_config_value;
				//		}
				//		if (file_name.ToLower().Contains(".xlsx"))
				//		{
				//			fileName = file_name;
				//		}
				//		else
				//		{
				//			fileName = file_name + ".xlsx";
				//		}
				//		string filePath = Path.Combine(out_filepath, fileName);

				//		if (!System.IO.File.Exists(filePath))
				//		{
				//			return NotFound();
				//		}

				//		using (var memoryStream = new MemoryStream())
				//		{
				//			using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
				//			{
				//				var fileName1 = Path.GetFileName(filePath);
				//				var entry = archive.CreateEntry(fileName1, CompressionLevel.Optimal);

				//				using (var entryStream = entry.Open())
				//				using (var fileStream = System.IO.File.OpenRead(filePath))
				//				{
				//					fileStream.CopyTo(entryStream);
				//				}
				//			}
				//			var zipName = $"{file_name}.zip";
				//			memoryStream.Seek(0, SeekOrigin.Begin);
				//			return File(memoryStream.ToArray(), "application/zip", zipName);
				//		}
				//	}
				//}
			}
			catch (Exception ex)
			{
				CommonController objcom = new CommonController(_configuration);
				objcom.errorlog(ex.Message, "ppl_template_download");
				return Json(ex.Message);
			}
		}


		public JsonResult getfilepath(string confing_val, string username)
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
					client.DefaultRequestHeaders.Add("user_code", username);
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
				objcom.errorlog(ex.Message, "getfilepath");
				return Json(ex.Message);
			}
		}


		#endregion
	}

}