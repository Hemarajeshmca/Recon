using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Recon_proto.Controllers
{
	public class ReconController : Controller
	{
        private IConfiguration _configuration;
        public ReconController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult ReconView()
		{
			return View();
		}
		public IActionResult ReconList()
		{
			return View();
		}
		#region list
		[HttpPost]
		public JsonResult Reconlistfetch([FromBody] Reconlistmodel context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
			List<Reconlistmodel> objcat_lst = new List<Reconlistmodel>();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("http://localhost:4195/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("reconlist", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					Reconlistmodel objcat = new Reconlistmodel();
					objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
					objcat.recon_code = result.Rows[i]["recon_code"].ToString();
					objcat.recon_name = result.Rows[i]["recon_name"].ToString();
					objcat.recontype_code = result.Rows[i]["recontype_code"].ToString();
					objcat.recontype_desc = result.Rows[i]["recontype_desc"].ToString();
					objcat.period_from = result.Rows[i]["period_from"].ToString();
					objcat.period_to = result.Rows[i]["period_to"].ToString();
					objcat.until_active_flag = result.Rows[i]["until_active_flag"].ToString();
					objcat.active_status = result.Rows[i]["active_status"].ToString();				
					objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
					objcat_lst.Add(objcat);
				}
				return Json(objcat_lst);
			}
		}
	
		public class Reconlistmodel
		{
			public string? recon_code { get; set; }
			public string? recon_name { get; set; }
			public int recon_gid { get; set; }
			public string? recontype_code { get; set; }
			public string? recontype_desc { get; set; }
			public string? period_from { get; set; }
			public string? period_to { get; set; }
			public string? until_active_flag { get; set; }
			public string? active_status { get; set; }
			public string? active_status_desc { get; set; }			
			public string? in_user_code { get; set; }			
		}
		#endregion

		#region recon type
		[HttpPost]
		public JsonResult getrecontype()
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
			List<recontype> objcat_lst = new List<recontype>();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
				var response = client.GetAsync("recontype").Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					recontype objcat = new recontype();
					objcat.recontype_gid = Convert.ToInt32(result.Rows[i]["recontype_gid"]);
					objcat.recontype_code = result.Rows[i]["recontype_code"].ToString();
					objcat.recontype_desc = result.Rows[i]["recontype_desc"].ToString();
					objcat_lst.Add(objcat);
				}
				return Json(objcat_lst);
			}
		}
		
		public class recontype
		{
			public string? recontype_desc { get; set; }
			public string? recontype_code { get; set; }
			public int recontype_gid { get; set; }
		}
		#endregion

		#region header
		[HttpPost]
		public JsonResult Reconheadersave([FromBody] Reconheader context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Reconheader objList = new Reconheader();
			DataTable result = new DataTable();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("Recon", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_recon_gid = Convert.ToInt16(result.Rows[i]["in_recon_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = Convert.ToInt16(result.Rows[i]["out_result"].ToString());
				}
				return Json(objList);
			}
		}
		
		public class Reconheader
		{
			public Int32? in_recon_gid { get; set; }
			public String in_recon_code { get; set; }
			public String in_recon_name { get; set; }
			public String in_recontype_code { get; set; }
			public String? in_recon_automatch_partial { get; set; }
			public String in_period_from { get; set; }
			public String in_period_to { get; set; }
			public String? in_until_active_flag { get; set; }
			public String? in_active_status { get; set; }
			public String? in_recon_date_flag { get; set; }
			public String? in_recon_date_field { get; set; }
			public String? in_recon_value_flag { get; set; }
			public String? in_recon_value_field { get; set; }
			public Double in_threshold_plus_value { get; set; }
			public Double in_threshold_minus_value { get; set; }
			public String in_active_reason { get; set; }
			public String in_action { get; set; }
			public String? in_action_by { get; set; }
			public String? out_msg { get; set; }
			public Int32? out_result { get; set; }
		}
		#endregion

		#region dataset recon
		[HttpPost]
		public JsonResult Recondatasetsave([FromBody] Recondataset context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Recondataset objList = new Recondataset();
			DataTable result = new DataTable();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("Recondataset", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_recondataset_gid = Convert.ToInt16(result.Rows[i]["in_recondataset_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = Convert.ToInt16(result.Rows[i]["out_result"].ToString());
				}
				return Json(objList);
			}
		}
		
		public class Recondataset
		{
			public Int16? in_recondataset_gid { get; set; }
			public String in_recon_code { get; set; }
			public String in_dataset_code { get; set; }
			public String in_dataset_type { get; set; }
			public String in_parent_dataset_code { get; set; }
			public String in_active_status { get; set; }
			public String in_action { get; set; }
			public String? in_user_code { get; set; }
			public String? out_msg { get; set; }
			public Int16? out_result { get; set; }
		}
		#endregion

		[HttpPost]
		public JsonResult Reconheaderfetch([FromBody] fetchRecon context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataSet result = new DataSet();
			DataTable result1 = new DataTable();
			List<fetchRecondataset> objcat_lst = new List<fetchRecondataset>();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("fetchrecondetails", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataSet>(d2);
				var rr = result.Tables.Count;
				if (rr <= 0)
				{
					d2 = "";
				}
				//result1 = result.Tables[1];
				//for (int i = 0; i < result1.Rows.Count; i++)
				//{
				//	fetchRecondataset objcat = new fetchRecondataset();
				//	objcat.recondataset_gid = Convert.ToInt16(result1.Rows[i]["recondataset_gid"]);
				//	objcat.recon_code = result1.Rows[i]["recon_code"].ToString();
				//	objcat.dataset_code = result1.Rows[i]["dataset_code"].ToString();
				//	objcat.dataset_name = result1.Rows[i]["dataset_name"].ToString();
				//	objcat.dataset_type = result1.Rows[i]["dataset_type"].ToString();
				//	objcat.dataset_type_desc = result1.Rows[i]["dataset_type_desc"].ToString();
				//	objcat.parent_dataset_code = result1.Rows[i]["parent_dataset_code"].ToString();
				//	objcat.active_status = result1.Rows[i]["active_status"].ToString();
				//	objcat.active_status_desc = result1.Rows[i]["active_status_desc"].ToString();
				//	objcat_lst.Add(objcat);
				//}
				return Json(d2);
			}
		}
		#region reconfetch
		public class fetchRecon
		{
			public Int16? in_recon_code { get; set; }

		}
		public class fetchRecondataset
		{
			public Int16? recondataset_gid { get; set; }
			public String recon_code { get; set; }
			public String dataset_code { get; set; }
			public String dataset_name { get; set; }
			public String dataset_type { get; set; }
			public String dataset_type_desc { get; set; }
			public String parent_dataset_code { get; set; }
			public String active_status { get; set; }
			public String active_status_desc { get; set; }
		}
		#endregion

		[HttpPost]
		public JsonResult Recondatasetmappingsave([FromBody] datamapping context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            datamapping objList = new datamapping();
			DataTable result = new DataTable();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("recondatamapping", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					objList.in_reconfield_gid = Convert.ToInt16(result.Rows[i]["in_reconfield_gid"]);
					objList.out_msg = result.Rows[i]["out_msg"].ToString();
					objList.out_result = Convert.ToInt16(result.Rows[i]["out_result"].ToString());
				}
				return Json(objList);
			}
		}
		#region dataset mapping
		public class datamapping
		{
			public Int16? in_reconfield_gid { get; set; }
			public Int16? in_reconfieldmapping_gid { get; set; }
			public string? in_recon_code { get; set; }
			public string? in_recon_field_name { get; set; }
			public Decimal? in_display_order { get; set; }
			public string? in_dataset_code { get; set; }
			public string? in_dataset_field_name { get; set; }
			public string? in_active_status { get; set; }
			public string? in_action { get; set; }
			public string? in_user_code { get; set; }
			public String? out_msg { get; set; }
			public Int16? out_result { get; set; }

		}
		#endregion

		[HttpPost]
		public JsonResult Recondatasetmappingfetch([FromBody] getReconDataMappingList context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
			List<getReconDataMapping> objcat_lst = new List<getReconDataMapping>();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getReconDataMappingList", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result1 = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result1.Rows.Count; i++)
				{
					getReconDataMapping objcat = new getReconDataMapping();
					objcat.reconfieldmapping_gid = Convert.ToInt16(result1.Rows[i]["reconfieldmapping_gid"]);
					objcat.in_dataset_code = result1.Rows[i]["dataset_code"].ToString();
					objcat.in_datasetname_code = result1.Rows[i]["dataset_name"].ToString();
					objcat.in_dataset_field_name = result1.Rows[i]["field_name"].ToString();
					objcat.dataset_table_field = result1.Rows[i]["dataset_table_field"].ToString();
					objcat.active_status = result1.Rows[i]["active_status"].ToString();
					objcat.active_status_desc = result1.Rows[i]["active_status_desc"].ToString();					
					objcat_lst.Add(objcat);
				}
				return Json(objcat_lst); 
			}
		}
		#region reconfetch
		public class getReconDataMappingList
		{
			public String? in_recon_code { get; set; }
			public String? in_recon_field_name { get; set; }
			public String? in_dataset_code { get; set; }
	}
		public class getReconDataMapping
		{
			public Int16 reconfieldmapping_gid { get; set; }
			public String? in_dataset_code { get; set; }
			public String? in_datasetname_code { get; set; }
			public String? in_dataset_field_name { get; set; }
			public String? dataset_table_field { get; set; }
			public String? active_status { get; set; }
			public String? active_status_desc { get; set; }
		}
		#endregion
		[HttpPost]
		public JsonResult Reconlistknockoff()
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result1 = new DataTable();
			List<getReconknockoff> objcat_lst = new List<getReconknockoff>();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(""), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getreconknockofflist", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result1 = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result1.Rows.Count; i++)
				{
					getReconknockoff objcat = new getReconknockoff();
					objcat.recon_gid = Convert.ToInt16(result1.Rows[i]["recon_gid"]);
					objcat.recon_code = result1.Rows[i]["recon_code"].ToString();
					objcat.recon_name = result1.Rows[i]["recon_name"].ToString();
					objcat.recontype_code = result1.Rows[i]["recontype_code"].ToString();
					objcat.recontype_desc = result1.Rows[i]["recontype_desc"].ToString();
					objcat.start_date = result1.Rows[i]["start_date"].ToString();
					objcat.job_remark = result1.Rows[i]["job_remark"].ToString();
					objcat.job_status = result1.Rows[i]["job_status"].ToString();
					objcat.jobstatus_desc = result1.Rows[i]["jobstatus_desc"].ToString();
					objcat_lst.Add(objcat);
				}
				return Json(objcat_lst);
			}
		}

		public class getReconknockoff
		{
			public Int32? recon_gid { get; set; }
			public String? recon_code { get; set; }
			public String? recon_name { get; set; }
			public String? recontype_code { get; set; }
			public String? recontype_desc { get; set; }
			public String? start_date { get; set; }
			public String? job_remark { get; set; }
			public String? job_status { get; set; }
			public String? jobstatus_desc { get; set; }
		}


		[HttpPost]
		public JsonResult getReconAgainstTypeCode([FromBody] recontypemodel context)
		{
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
			List<Reconlistmodel> objcat_lst = new List<Reconlistmodel>();
			string post_data = "";
			using (var client = new HttpClient())
			{
                string Urlcon = "Recon/";
                client.BaseAddress = new Uri(urlstring + Urlcon);
                //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("getReconAgainstTypecode", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					Reconlistmodel objcat = new Reconlistmodel();
					objcat.recon_gid = Convert.ToInt32(result.Rows[i]["recon_gid"]);
					objcat.recon_code = result.Rows[i]["recon_code"].ToString();
					objcat.recon_name = result.Rows[i]["recon_name"].ToString();
					objcat.recontype_code = result.Rows[i]["recontype_code"].ToString();
					objcat.recontype_desc = result.Rows[i]["recontype_desc"].ToString();
					objcat.period_from = result.Rows[i]["period_from"].ToString();
					objcat.period_to = result.Rows[i]["period_to"].ToString();
					objcat.until_active_flag = result.Rows[i]["until_active_flag"].ToString();
					objcat.active_status = result.Rows[i]["active_status"].ToString();
					objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
					objcat_lst.Add(objcat);
				}
				return Json(objcat_lst);
			}
		}

		public class recontypemodel
		{
			public String? in_recontype_code { get; set;}
		}



	}
}
