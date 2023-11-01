using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.DataSetController;
using static Recon_proto.Controllers.ReconController;

namespace Recon_proto.Controllers
{
	public class UtilityController : Controller
	{
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
			DataTable result = new DataTable();
			List<jobtype> objcat_lst = new List<jobtype>();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
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
		public JsonResult Joblistfetch([FromBody] Jobstatusmodel context)
		{
			Jobstatusmodel objList = new Jobstatusmodel();
			DataTable result = new DataTable();
			List<Joblistmodel> objcat_lst = new List<Joblistmodel>();
			string post_data = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Utility/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("jobStatus", content).Result;
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
		}

		public class Jobstatusmodel
		{
			public String? in_start_date { get; set; }
			public String? in_end_date { get; set; }
			public String? in_jobtype_code { get; set; }
			public String? in_jobstatus { get; set; }
		}
	}
}
