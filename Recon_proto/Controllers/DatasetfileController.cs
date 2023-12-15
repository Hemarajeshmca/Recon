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
		public IActionResult Datasetconversion()
		{
			ViewBag.baseurl = _configuration.GetSection("Appsettings")["connectorUrl"].ToString();
			return View();
		}

        #region datasetfile
        [HttpPost]
		public string datasetfile(string pipeline_code, IFormFile file, string initiated_by)
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
                        client.BaseAddress = new Uri(urlstring);
                        client.DefaultRequestHeaders.Accept.Clear();
                        content1.Add(new StreamContent(fileStream), "file", file.FileName);
                        content1.Add(new StringContent(pipeline_code), "pipeline_code");
                        content1.Add(new StringContent(initiated_by), "initiated_by");
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(objcontent));
                        var response = client.PostAsync(filepath, content1).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        var res = "";
                        if (post_data != "")
                        {
                            res = setProcessdataset(post_data);
                        }
                        return res;
                        //return Json(post_data);
                    }
                } catch (Exception ex)
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
            } catch (Exception ex)
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

	}
}
