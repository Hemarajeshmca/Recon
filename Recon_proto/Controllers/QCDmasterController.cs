using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net;
using static Recon_proto.Controllers.DataSetController;
using System.Net.Http.Headers;
using System.Text;
using Recon_proto.Models;
using System.Data;
using System.Net;

namespace Recon_proto.Controllers
{
    public class QCDmasterController : Controller
    {
        public IActionResult QcdMaster()
        {
            return View();
        }

        [HttpPost]
        public JsonResult QcdMasterGridRead([FromBody] QcdlistModal context)
        {
            QcdlistModal objList = new QcdlistModal();
            DataTable result = new DataTable();
            List<QcdMasterModel> objcat_lst = new List<QcdMasterModel>();
            string post_data = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/qcdmaster/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("QcdMasterGridRead", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                string d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    QcdMasterModel objcat = new QcdMasterModel();
                    objcat.master_gid = Convert.ToInt32(result.Rows[i]["master_gid"]);
                    objcat.master_syscode = result.Rows[i]["master_syscode"].ToString();
                    objcat.master_code = result.Rows[i]["master_code"].ToString();
                    objcat.master_short_code = result.Rows[i]["master_short_code"].ToString();
                    objcat.master_name = result.Rows[i]["master_name"].ToString();
                    objcat.parent_master_syscode = result.Rows[i]["parent_master_syscode"].ToString();
                    objcat.active_status = result.Rows[i]["active_status"].ToString();
                    objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat.master_multiple_name = result.Rows[i]["master_multiple_name"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst);
            }
        }


        [HttpPost]
        public JsonResult QcdCrud([FromBody] QcdCrudModal context)
        {
            QcdCrudModal objList = new QcdCrudModal();
            DataTable result = new DataTable();
            string post_data = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/qcdmaster/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("QcdMaster", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                string d2 = JsonConvert.DeserializeObject<string>(post_data);
               // result = JsonConvert.DeserializeObject<DataTable>(d2);
                return Json(d2);
            }
        }


        public class QcdlistModal
        {
            public string? in_user_code { get; set; }
            public string? in_master_code { get; set; }

        }

        public class QcdCrudModal
        {
            public string? masterCode { get; set; }
            public string? active_status { get; set; }
            public string? action { get; set; }
            public int? masterGid { get; set; }
            public string? mastermutiplename { get; set; }
            public string? masterName { get; set; }
            public string? masterShortCode { get; set; }
            public string? masterSyscode { get; set; }
            public string? parentMasterSyscode { get; set; }

        }

    }
}
