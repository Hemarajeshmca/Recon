﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Recon_proto.Controllers
{
	public class DatasettoReconController : Controller
	{
		private IConfiguration _configuration;
		public DatasettoReconController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		string urlstring = "";
		public IActionResult DatasettoReconList()
		{
			return View();
		}
		#region list
		[HttpPost]
		public JsonResult DatasettoReconListfetch([FromBody] DatasettoReconListmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();		
			string post_data = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "DatasettoRecon/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("DatasettoReconRead", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}
	
		public class DatasettoReconListmodel
		{			
			public int in_user_gid { get; set; }			
		}
		#endregion

		#region process
		[HttpPost]
		public JsonResult DatasettoReconprocess([FromBody] DatasettoReconListmodel context)
		{
			urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string post_data = "";
			using (var client = new HttpClient())
			{
				string Urlcon = "DatasettoRecon/";
				client.BaseAddress = new Uri(urlstring + Urlcon);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("DatasettoReconprocess", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}

		public class DatasettoReconprocessmodel
		{
			public int in_user_gid { get; set; }
			public int in_scheduler_gid { get; set; }
		}
#endregion
	}
}