using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static Recon_proto.Controllers.UtilityController;
using System.Data.SqlTypes;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

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

		[HttpPost]
		public IActionResult datasetfile(string pipeline_code, IFormFile file, string initiated_by)
		{
			datasetfileModel objcontent = new datasetfileModel();
			objcontent.pipeline_code = pipeline_code;
			objcontent.file = file;
			objcontent.initiated_by = initiated_by;
			urlstring = _configuration.GetSection("Appsettings")["connectorUrl"].ToString();
			string post_data = "";
			var filepath = "/Pipeline/NewScheduler";
			using (var client = new HttpClient())
			using (var content1 = new MultipartFormDataContent())
			using (var fileStream = file.OpenReadStream())
			{
				string[] result = { };
				client.BaseAddress = new Uri(urlstring + filepath);
				client.DefaultRequestHeaders.Accept.Clear();
				content1.Add(new StreamContent(fileStream), "file", file.FileName);
				content1.Add(new StringContent(pipeline_code), "pipeline_code");
				content1.Add(new StringContent(initiated_by), "initiated_by");
				HttpContent content = new StringContent(JsonConvert.SerializeObject(objcontent));
				var response = client.PostAsync(filepath, content1).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				return Json(post_data);
			}

		}
		public class datasetfileModel
		{
			public string? pipeline_code { get; set; }
			public IFormFile file { get; set; }
			public string? initiated_by { get; set; }
		}


	}
}
