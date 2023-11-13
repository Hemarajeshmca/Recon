using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using static Recon_proto.Controllers.DataSetController;
using static Recon_proto.Controllers.ReconController;

namespace Recon_proto.Controllers
{
    public class RulesetupController : Controller
    {
        public IActionResult Rulesetup()
        {
            return View();
        }
        public IActionResult Rulesetupdetail()
        {
            return View();
        }

        #region list
        public class Rulesetuplist
        {
            public String? in_user_code { get; set; }
			public string? in_recon_code { get; set; }
		}
        public class Rulesetuplistmodel
        {
            public string? rule_code { get; set; }
            public string? rule_name { get; set; }
            public int rule_gid { get; set; }
            public string? recon_code { get; set; }
            public string? recon_name { get; set; }
            public string? active_status { get; set; }
            public string? active_status_desc { get; set; }
        }

        [HttpPost]
        public JsonResult Rulesetuplistfetch([FromBody] Rulesetuplist context)
        {
            DataTable result = new DataTable();
            List<Rulesetuplistmodel> objcat_lst = new List<Rulesetuplistmodel>();
            string post_data = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("rulelist", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                string d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    Rulesetuplistmodel objcat = new Rulesetuplistmodel();
                    objcat.rule_gid = Convert.ToInt32(result.Rows[i]["rule_gid"]);
                    objcat.rule_code = result.Rows[i]["rule_code"].ToString();
                    objcat.rule_name = result.Rows[i]["rule_name"].ToString();
                    objcat.recon_code = result.Rows[i]["recon_code"].ToString();
                    objcat.recon_name = result.Rows[i]["recon_name"].ToString();
                    objcat.active_status = result.Rows[i]["active_status"].ToString();
                    objcat.active_status_desc = result.Rows[i]["active_status_desc"].ToString();
                    objcat_lst.Add(objcat);
                }
                return Json(objcat_lst);
            }
        }
        #endregion

        #region rule header
        public class Rulesetupheader
        {
            public Int64? in_rule_gid { get; set; }
            public string? in_rule_code { get; set; }
            public string? in_rule_name { get; set; }
            public string? in_recon_code { get; set; }
            public String in_period_from { get; set; }
            public String? in_period_to { get; set; }
            public string? in_until_active_flag { get; set; }
            public string? in_applyrule_on { get; set; }
            public string? in_source_dataset_code { get; set; }
            public string? in_comparison_dataset_code { get; set; }
            public string? in_source_acc_mode { get; set; }
            public string? in_parent_dataset_code { get; set; }
            public string? in_support_dataset_code { get; set; }
            public string? in_parent_acc_mode { get; set; }
            public string? in_reversal_flag { get; set; }
            public string? in_group_flag { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_user_code { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }
        [HttpPost]
        public JsonResult Ruleheader([FromBody] Rulesetupheader context)
        {
            Rulesetupheader objList = new Rulesetupheader();
            DataTable result = new DataTable();
            string post_data = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("ruleheader", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                string d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objList.in_rule_gid = Convert.ToInt32(result.Rows[i]["in_rule_gid"]);
                    objList.out_msg = result.Rows[i]["out_msg"].ToString();
                    objList.out_result = result.Rows[i]["out_result"].ToString();
                }
                return Json(objList);
            }
        }
        #endregion

        #region fetch
        public class fetchRule
        {
            public string? in_rule_code { get; set; }
        }
        [HttpPost]
        public JsonResult rulefetch([FromBody] fetchRule context)
        {
            DataSet result = new DataSet();
            DataTable result1 = new DataTable();
            List<fetchRecondataset> objcat_lst = new List<fetchRecondataset>();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("fetchrule", content).Result;
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
                return Json(d2);
            }
        }
        #endregion

        #region fetch dataset
        public class getdataagainsRecon
        {
            public String? in_recon_code { get; set; }
        }
        [HttpPost]
        public JsonResult rulerecondatasetfetch([FromBody] getdataagainsRecon context)
        {
            DataSet result = new DataSet();
            DataTable result1 = new DataTable();
            List<fetchRecondataset> objcat_lst = new List<fetchRecondataset>();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("getdataagainsRecon", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                return Json(d2);
            }
        }
        #endregion

        #region fetch condition
        public class getCondition
        {
            public String? in_condition_type { get; set; }
            public String? in_field_type { get; set; }
        }
        [HttpPost]
        public JsonResult rulefilterfetch([FromBody] getCondition context)
        {
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("getCondition", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                return Json(d2);
            }
        }
        #endregion

        #region  rule condition
        public class RuleCondition
        {
            public int? in_rulecondition_gid { get; set; }
            public string? in_rule_code { get; set; }
            public string? in_source_field { get; set; }
            public string? in_comparison_field { get; set; }
            public string? in_extraction_criteria { get; set; }
            public string? in_comparison_criteria { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_user_code { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }

        [HttpPost]
        public JsonResult ruleconditionsave([FromBody] RuleCondition context)
        {
            RuleCondition objList = new RuleCondition();
            DataTable result = new DataTable();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("rulecondition", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objList.in_rulecondition_gid = Convert.ToInt32(result.Rows[i]["in_rulecondition_gid"]);
                    objList.out_msg = result.Rows[i]["out_msg"].ToString();
                    objList.out_result = result.Rows[i]["out_result"].ToString();
                }
                return Json(objList);
            }
        }
        #endregion

        #region grouping
        public class Rulegrouping
        {
            public int? in_rulegrpfield_gid { get; set; }
            public int? in_rule_gid { get; set; }
            public string? in_group_method_flag { get; set; }
            public string? in_manytomany_match_flag { get; set; }
            public string? in_grp_field { get; set; }
            public string? in_rule_code { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_user_code { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }
        [HttpPost]
        public JsonResult rulegroupsave([FromBody] Rulegrouping context)
        {
            Rulegrouping objList = new Rulegrouping();
            DataTable result = new DataTable();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("rulegrouping", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objList.in_rulegrpfield_gid = Convert.ToInt32(result.Rows[i]["in_rulegrpfield_gid"]);
                    objList.out_msg = result.Rows[i]["out_msg"].ToString();
                    objList.out_result = result.Rows[i]["out_result"].ToString();
                }
                return Json(objList);
            }
        }
        #endregion

        #region getFieldAgainstReconList
        public class getFieldAgainstReconList
        {
            public String? in_recon_code { get; set; }
        }
        [HttpPost]
        public JsonResult FieldAgainstRecon([FromBody] getFieldAgainstReconList context)
        {
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("getFieldAgainstRecon", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                return Json(d2);
            }
        }
        #endregion

        #region RuleIdentifier
        public class RuleIdentifier
        {
            public int? in_ruleselefilter_gid { get; set; }
            public string? in_rule_code { get; set; }
            public string? in_filter_applied_on { get; set; }
            public string? in_filter_field { get; set; }
            public string? in_filter_criteria { get; set; }
            public string? in_ident_criteria { get; set; }
            public string? in_ident_value { get; set; }
            public string? in_active_status { get; set; }
            public string? in_action { get; set; }
            public string? in_user_code { get; set; }
            public string? out_msg { get; set; }
            public string? out_result { get; set; }
        }
        [HttpPost]
        public JsonResult ruleIdentifiersave([FromBody] RuleIdentifier context)
        {
            RuleIdentifier objList = new RuleIdentifier();
            DataTable result = new DataTable();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("ruleidentifier", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    objList.in_ruleselefilter_gid = Convert.ToInt32(result.Rows[i]["in_ruleselefilter_gid"]);
                    objList.out_msg = result.Rows[i]["out_msg"].ToString();
                    objList.out_result = result.Rows[i]["out_result"].ToString();
                }
                return Json(objList);
            }
        }
        #endregion


        public class Ruleagainstrecon
        {
            public string? in_recon_code { get; set; }
            public string? in_rule_apply_on { get; set; }
        }

        public class RuleAgainstReconList
        {
            public Int32? rule_gid { get; set; }
            public String? rule_code { get; set; }
            public String? rule_name { get; set; }
            public String? source_dataset_code { get; set; }
            public string? comparison_dataset_code { get; set; }
            public string? source_dataset_desc { get; set; }
            public string? comparison_dataset_desc { get; set; }
        }

        [HttpPost]
        public JsonResult RuleAgainstRecon([FromBody] Ruleagainstrecon context)
        {
            List<RuleAgainstReconList> objcat_lst = new List<RuleAgainstReconList>();

            DataTable result = new DataTable();
            string post_data = "";
            string d2 = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/api/Rulesetup/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                var response = client.PostAsync("getruleagainstRecon", content).Result;
                Stream data = response.Content.ReadAsStreamAsync().Result;
                StreamReader reader = new StreamReader(data);
                post_data = reader.ReadToEnd();
                d2 = JsonConvert.DeserializeObject<string>(post_data);
                result = JsonConvert.DeserializeObject<DataTable>(d2);
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    RuleAgainstReconList objList = new RuleAgainstReconList();
                    objList.rule_gid = Convert.ToInt32(result.Rows[i]["rule_gid"]);
                    objList.rule_code = result.Rows[i]["rule_code"].ToString();
                    objList.rule_name = result.Rows[i]["rule_name"].ToString();
                    objList.source_dataset_code = result.Rows[i]["source_dataset_code"].ToString();
                    objList.source_dataset_desc = result.Rows[i]["source_dataset_desc"].ToString();
                    objList.comparison_dataset_code = result.Rows[i]["comparison_dataset_code"].ToString();
                    objList.comparison_dataset_desc = result.Rows[i]["comparison_dataset_desc"].ToString();
                    objcat_lst.Add(objList);
                }
                return Json(objcat_lst);
            }
        }
    }
}
