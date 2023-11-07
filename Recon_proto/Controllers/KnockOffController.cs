using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace Recon_proto.Controllers
{
	public class KnockOffController : Controller
	{
		public IActionResult AutoRuleBase()
		{
			return View();
		}
		public IActionResult PreviewRuleBase()
		{
			return View();
		}
		public IActionResult ManualKO()
		{
			return View();
		}

		public IActionResult UndoKO()
		{
			return View();
		}

		public IActionResult UndoKObyJob()
		{
			return View();
		}

		public IActionResult KoFilebased()
		{
			return View();
		}

        public class getpreviewRulebase
        {
            public String? in_recon_code { get; set; }
            public String? in_ko_post_flag { get; set; }
            public String? in_period_from { get; set; }
            public String? in_period_to { get; set; }
            public String? in_automatch_flag { get; set; }
            public String? in_ip_addr { get; set; }
            public String? in_user_code { get; set; }
        }

        [HttpPost]
        public JsonResult previewRulebase([FromBody] getpreviewRulebase context)
        {
            DataSet result = new DataSet();
            DataTable result1 = new DataTable();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Process/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("automatchpreview", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                return Json(d2);
            }
        }


		public JsonResult matchReconbase([FromBody] getpreviewRulebase context)
		{
			DataSet result = new DataSet();
			DataTable result1 = new DataTable();
			string post_data = "";
			string d2 = "";
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://localhost:44348/api/Process/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
				var response = client.PostAsync("automatchpreview", content).Result;
				Stream data = response.Content.ReadAsStreamAsync().Result;
				StreamReader reader = new StreamReader(data);
				post_data = reader.ReadToEnd();
				d2 = JsonConvert.DeserializeObject<string>(post_data);
				return Json(d2);
			}
		}

	}
}
