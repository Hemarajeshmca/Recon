using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Recon_proto.Controllers
{
	public class DataSetController : Controller
	{
		public IActionResult DataSetList()
		{
			return View();
		}
		public IActionResult DataSetForm()
		{
			return View();
		}

		[HttpPost]
		public JsonResult Datasetlistfetch([FromBody] Datasetlistmodel context)
		{
			Datasetlistmodel objList = new Datasetlistmodel();
			DataTable result = new DataTable();
			List<Datasetlistmodel> objcat_lst = new List<Datasetlistmodel>();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("DatasetRead", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				result = JsonConvert.DeserializeObject<DataTable>(d2);
				for (int i = 0; i < result.Rows.Count; i++)
				{
					Datasetlistmodel objcat = new Datasetlistmodel();
					objcat.dataset_id = Convert.ToInt32(result.Rows[i]["dataset_gid"]);
					objcat.dataset_name = result.Rows[i]["dataset_name"].ToString();
					objcat.datasetCode = result.Rows[i]["dataset_code"].ToString();
					objcat.active_status = result.Rows[i]["active_status"].ToString();
					objcat.dataset_category = result.Rows[i]["dataset_category"].ToString();
					objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
					objcat_lst.Add(objcat);
				}
				return Json(objcat_lst);
			}
		}
		#region list
		public class Datasetlistmodel
		{
			public string? dataset_name { get; set; }
			public string? datasetCode { get; set; }
			public int dataset_id { get; set; }
			public string? dataset_category { get; set; }
			public string? active_status { get; set; }
			public string? active_status_desc { get; set; }
			public int in_user_gid { get; set; }
			public string? in_user_code { get; set; }
			public string? in_active_status { get; set; }
		}
		#endregion
		[HttpPost]
		public JsonResult Datasetheader([FromBody] DatasetHeadermodel context)
		{
			DatasetHeadermodel objList = new DatasetHeadermodel();
			DataTable result = new DataTable();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
				client.DefaultRequestHeaders.Accept.Clear();
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

		#region header
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
		[HttpPost]
		public JsonResult Datasetdetail([FromBody] Datasetdetailrmodel context)
		{
			Datasetdetailrmodel objList = new Datasetdetailrmodel();
			DataTable result = new DataTable();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
				client.DefaultRequestHeaders.Accept.Clear();
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
		}
		#region detail
		public class Datasetdetailrmodel
		{
			public string? datasetCode { get; set; }
			public int datasetdetail_id { get; set; }
			public string? field_name { get; set; }
			public string? field_length { get; set; }
			public int? precision_length { get; set; }
			public int? scale_length { get; set; }
			public string? field_type { get; set; }
			public string? field_mandatory { get; set; }
			public string? in_action { get; set; }
			public string? in_action_by { get; set; }
			public string? out_msg { get; set; }
			public string? out_result { get; set; }
		}
		#endregion

		[HttpPost]
		public JsonResult Datasetdetailreadlist([FromBody] Datasetdetailfetch context)
		{
			List<Datasetdetailfetchlist> objcat_lst = new List<Datasetdetailfetchlist>();
			DataTable result = new DataTable();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Dataset/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("DatasetReaddetail", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				string d2 = JsonConvert.DeserializeObject<string>(post_data);
				//result = JsonConvert.DeserializeObject<DataTable>(d2);
				//for (int i = 0; i < result.Rows.Count; i++)
				//{
				//	Datasetdetailfetchlist objcat = new Datasetdetailfetchlist();
				//	objcat.datasetdetail_id = Convert.ToInt32(result.Rows[i]["datasetfield_gid"]);
				//	objcat.datasetCode = result.Rows[i]["dataset_code"].ToString();
				//	objcat.field_name = result.Rows[i]["field_name"].ToString();
				//	objcat.field_type = result.Rows[i]["field_type"].ToString();
				//	objcat.fieldtype_desc = result.Rows[i]["fieldtype_desc"].ToString();
				//	objcat.field_length = result.Rows[i]["field_length"].ToString();
				//	objcat.field_mandatory = result.Rows[i]["field_mandatory"].ToString();
				//	objcat.dataset_table_field = result.Rows[i]["dataset_table_field"].ToString();
				//	objcat_lst.Add(objcat);
				//}
				return Json(d2);
			}
		}
		#region detailfetch
		public class Datasetdetailfetch
		{
			public string? datasetCode { get; set; }			
		}
		public class Datasetdetailfetchlist
		{
			public string? datasetCode { get; set; }
			public int datasetdetail_id { get; set; }
			public string? field_name { get; set; }
			public string? field_type { get; set; }
			public string? fieldtype_desc { get; set; }
		    public string? field_length { get; set; }
			public string? field_mandatory { get; set; }
			public string? dataset_table_field { get; set; }
		}
		#endregion

		[HttpPost]
		public JsonResult getfieldtype()
		{
			DataTable result = new DataTable();
			List<fieldtype> objcat_lst = new List<fieldtype>();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/DataSet/");
				client.DefaultRequestHeaders.Accept.Clear();
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
		#region field type
		public class fieldtype
		{
			public string fieldtype_desc { get; set; }
			public string fieldtype_code { get; set; }
			public int fieldtype_gid { get; set; }
		}
		#endregion
	}
}
