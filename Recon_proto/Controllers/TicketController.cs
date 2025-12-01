using System;
using System.Data;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Recon_proto.Controllers.AdminController;
using static Recon_proto.Controllers.CommonController;

namespace Recon_proto.Controllers
{
    public class TicketController : Controller
    {
        private IConfiguration _configuration;
        public TicketController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string urlstring = "";
        public IActionResult Ticketlist()
        {
            ViewBag.ModalMode = true;
            return View();
        }

        #region assigedUsers
        [HttpPost]
        public JsonResult assigedUsers([FromBody] ticketassign context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            List<ticketdetails> objcat_lst = new List<ticketdetails>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Ticket/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("http://localhost:4195/api/Qcdmaster/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("TicketAssignUser", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        ticketdetails objcat1 = new ticketdetails();
                        objcat1.user_gid = Convert.ToInt32(result.Rows[i]["user_gid"]);
                        objcat1.user_code = result.Rows[i]["user_code"].ToString();
                        objcat1.user_name = result.Rows[i]["user_name"].ToString();
                        objcat1.role_code = result.Rows[i]["role_code"].ToString();
                        objcat1.role_name = result.Rows[i]["role_name"].ToString();
                        objcat_lst.Add(objcat1);
                    }
                    return Json(objcat_lst);
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "TicketAssignUser");
                return Json(ex.Message);
            }
        }
        #endregion

        #region ticketsave
        [HttpPost]
        public JsonResult ticketsave([FromBody] tktdtlmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";

            tktdtlmodel usr = new tktdtlmodel();
            usr.ticket_gid = context.ticket_gid;
            usr.ticket_no = context.ticket_no;
            usr.ticket_for = context.ticket_for;
            usr.ticket_date = context.ticket_date;
            usr.ticket_type = context.ticket_type;
            usr.ticket_subject = context.ticket_subject;
            usr.ticket_description = context.ticket_description;
            usr.ticket_priority = context.ticket_priority;
            usr.ticket_status = context.ticket_status;
            usr.ticket_assigned = context.ticket_assigned;
            usr.entity = context.entity;
            usr.department = context.department;
            usr.unit = context.unit;
            usr.in_screen_code = context.in_screen_code;
            usr.in_screen_name = context.in_screen_name;
            usr.in_recon_code = context.in_recon_code;
            usr.in_recon_name = context.in_recon_name;
            usr.in_report_code = context.in_report_code;
            usr.in_report_name = context.in_report_name;
            usr.ticket_isattachment = context.ticket_isattachment;
            usr.in_user = context.in_user;
            usr.in_ip_addr = context.in_ip_addr;
            usr.in_action = context.in_action;
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Ticket/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", usr.in_user);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(usr), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("Ticketsave", content).Result;
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
                objcom.errorlog(ex.Message, "Ticketsave");
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult addattachment([FromForm] tktattachmentmodel context, IFormFile file)
        {
            //string uploadFolder = @"E:\Venki Shared Project\ReconTicket\attachments";
            string uploadFolder = _configuration.GetSection("Appsettings")["Ticketattach"].ToString();

            string filePath = string.Empty;

            try
            {
                if (file != null && file.Length > 0)
                {
                    // Ensure directory exists
                    if (!Directory.Exists(uploadFolder))
                        Directory.CreateDirectory(uploadFolder);

                    // Call API first to get attachment_gid
                    using (var client = new HttpClient())
                    {
                        string url = _configuration.GetSection("Appsettings")["apiurl"].ToString();
                        string Urlcon = "Ticket/";
                        client.BaseAddress = new Uri(url + Urlcon);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.Timeout = Timeout.InfiniteTimeSpan;
                        client.DefaultRequestHeaders.Add("user_code", context.in_user);
                        client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                        client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                        client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // ✅ Send metadata (without file yet)
                        var metaData = new
                        {
                            context.in_action,
                            context.ticket_gid,
                            context.queue_gid,
                            file_name = file.FileName, // send original name, API will return attachment_gid
                            context.in_user,
                            context.in_ip_addr
                        };

                        HttpContent metaContent = new StringContent(JsonConvert.SerializeObject(metaData), Encoding.UTF8, "application/json");
                        var response = client.PostAsync("Attachmentsave", metaContent).Result;

                        string post_data = response.Content.ReadAsStringAsync().Result;
                        string result = JsonConvert.DeserializeObject<string>(post_data);

                        // ✅ Extract attachment_gid from result (assuming API returns it)
                        dynamic apiResult = JsonConvert.DeserializeObject(result);

                        // ✅ Handle both array or single object response
                        string attachment_gid = "0";
                        if (apiResult is JArray && apiResult.Count > 0)
                        {
                            attachment_gid = apiResult[0]["attachment_gid"]?.ToString() ?? "0";
                        }
                        else if (apiResult is JObject)
                        {
                            attachment_gid = apiResult["attachment_gid"]?.ToString() ?? "0";
                        }

                        // ✅ Build new filename using attachment_gid and original extension
                        string extension = Path.GetExtension(file.FileName);
                        string newFileName = $"{attachment_gid}{extension}";
                        filePath = Path.Combine(uploadFolder, newFileName);

                        // ✅ Save file to local folder
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        return Json(new
                        {
                            status = "success",
                            message = "File uploaded successfully",
                            saved_as = newFileName,
                            api_gid = attachment_gid,
                            path = filePath
                        });
                    }
                }
                else
                {
                    return Json(new { status = "error", message = "No file uploaded" });
                }
            }
            catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "addattachment");
                return Json(new { status = "error", message = ex.Message });
            }
        }



        [HttpPost]
        public JsonResult TicketListFetch([FromBody] Ticketlistmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Ticketlistmodel objList = new Ticketlistmodel();
            DataTable result = new DataTable();
            List<Ticketlistmodel> objcat_lst = new List<Ticketlistmodel>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Ticket/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("TicketList", content).Result;
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
                objcom.errorlog(ex.Message, "TicketListFetch");
                return Json(ex.Message);
            }

        }

        [HttpPost]
        public JsonResult threadFetch([FromBody] Threadlistmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Threadlistmodel objList = new Threadlistmodel();
            DataTable result = new DataTable();
            List<Threadlistmodel> objcat_lst = new List<Threadlistmodel>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Ticket/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user_code);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("ThreadList", content).Result;
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
                objcom.errorlog(ex.Message, "threadFetch");
                return Json(ex.Message);
            }

        }

        [HttpPost]
        public JsonResult AttachFetch([FromBody] ViewAttachModel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            ViewAttachModel objList = new ViewAttachModel();
            DataTable result = new DataTable();
            List<ViewAttachModel> objcat_lst = new List<ViewAttachModel>();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Ticket/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", context.in_user);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("ViewAttachment", content).Result;
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
                objcom.errorlog(ex.Message, "threadFetch");
                return Json(ex.Message);
            }

        }

        [HttpGet("Ticket/DownloadAttachment")]     
        public IActionResult DownloadAttachment(string attachment_gid, string originalFileName)
        {
            if (string.IsNullOrWhiteSpace(attachment_gid))
                return BadRequest("Attachment ID is required.");

            if (string.IsNullOrEmpty(originalFileName))
                originalFileName = "attachment";

            //string folderPath = @"E:\Venki Shared Project\ReconTicket\attachments";
            string folderPath = _configuration.GetSection("Appsettings")["Ticketattach"].ToString();

            // ✅ Find file with any extension (not just .pdf)
            string[] files = Directory.GetFiles(folderPath, $"{attachment_gid}.*");

            if (files.Length == 0)
                return NotFound($"No file found for attachment ID {attachment_gid} in {folderPath}");

            string fullPath = files[0];
            string extension = Path.GetExtension(fullPath).ToLowerInvariant();

            // ✅ Determine MIME type dynamically
            string mimeType = extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",               
                ".txt" => "text/plain",
                ".csv" => "text/csv",          
             _ => "application/octet-stream" // default (unknown type)
            };

            byte[] fileBytes = System.IO.File.ReadAllBytes(fullPath);

            // ✅ Return original filename (with proper extension)
            if (!Path.HasExtension(originalFileName))
            {
                originalFileName += extension;
            }

            string downloadName = originalFileName;

            return File(fileBytes, mimeType, downloadName);
        }

        [HttpPost]
        public JsonResult ticketbulkassign([FromBody] tktdtlmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";

            tktdtlmodel usr = new tktdtlmodel();
            usr.ticket_gid = context.ticket_gid;
            usr.ticket_no = context.ticket_no;
            usr.ticket_for = context.ticket_for;
            usr.ticket_date = context.ticket_date;
            usr.ticket_type = context.ticket_type;
            usr.ticket_subject = context.ticket_subject;
            usr.ticket_description = context.ticket_description;
            usr.ticket_priority = context.ticket_priority;
            usr.ticket_status = context.ticket_status;
            usr.ticket_assigned = context.ticket_assigned;
            usr.entity = context.entity;
            usr.department = context.department;
            usr.unit = context.unit;
            usr.ticket_isattachment = context.ticket_isattachment;
            usr.in_user = context.in_user;
            usr.in_ip_addr = context.in_ip_addr;
            usr.in_action = context.in_action;
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "Ticket/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", usr.in_user);
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(usr), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("BulkTicketSave", content).Result;
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
                objcom.errorlog(ex.Message, "BulkTicketSave");
                return Json(ex.Message);
            }
        }

		[HttpPost]
		public JsonResult DeleteAttachment([FromBody] tktattachmentmodel context)
		{
			string apiBaseUrl = _configuration.GetSection("Appsettings")["apiurl"].ToString();
			string responseData = "";

			try
			{
				using (var client = new HttpClient())
				{
					string apiEndpoint = "Ticket/Attachmentsave";
					client.BaseAddress = new Uri(apiBaseUrl);
					client.DefaultRequestHeaders.Accept.Clear();
					client.Timeout = Timeout.InfiniteTimeSpan;
                     
					client.DefaultRequestHeaders.Add("user_code", context.in_user);
					client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
					client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
					client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                     
					HttpContent content = new StringContent(JsonConvert.SerializeObject(context), Encoding.UTF8, "application/json");
                     
					var response = client.PostAsync(apiEndpoint, content).Result;

					if (response.IsSuccessStatusCode)
					{
						responseData = response.Content.ReadAsStringAsync().Result;
						var parsed = JsonConvert.DeserializeObject<object>(responseData);

						return Json(new { status = "success", data = parsed });
						//return Json(new { status = "success", message = "Attachment deleted successfully", data = responseData });
					}
					else
					{
						return Json(new { status = "error", message = $"API returned {response.StatusCode}" });
					}
				}
			}
			catch (Exception ex)
			{
				new CommonController(_configuration).errorlog(ex.Message, "DeleteAttachment");
				return Json(new { status = "error", message = ex.Message });
			}
		}

	}

	public class Ticketlistmodel
    {
        public string? in_user_code { get; set; }
        public string? in_role_code { get; set; }
        public string? in_lang_code { get; set; }
        public string? in_ip_addr { get; set; }
        public string? in_action { get; set; }
        public string? in_screen_access { get; set; }        
    }

    public class Threadlistmodel
    {
        public string? in_user_code { get; set; }
        public int? in_ticket_gid { get; set; }
        public string? in_ticket_no { get; set; }
        public string? in_lang_code { get; set; }
        public string? in_ip_addr { get; set; }
        public string? in_action { get; set; }
    }


    public class ticketassign
    {
        public string? in_lang_code { get; set; }
        public string? in_ip_addr { get; set; }
        public string? in_context_code { get; set; }
    }

    public class tktattachmentmodel
    {
        public string? in_action { get; set; }
        public int? attachment_gid { get; set; }
        public int? ticket_gid { get; set; }
        public int? queue_gid { get; set; }
        public string? file_name { get; set; }
        public string? in_user { get; set; }
        public string? in_ip_addr { get; set; }
    }

    public class tktdtlmodel
    {
        public string? ticket_gid { get; set; }
        public string? ticket_no { get; set; }
        public string? ticket_for { get; set; }
        public string? ticket_date { get; set; }
        public string? ticket_type { get; set; }
        public string? ticket_subject { get; set; }
        public string? ticket_description { get; set; }
        public string? ticket_priority { get; set; }
        public string? ticket_assigned { get; set; }
        public string? ticket_status { get; set; }
        public string? entity { get; set; }
        public string? department { get; set; }
        public string? unit { get; set; }
        public string? ticket_isattachment { get; set; }
        public string? in_user { get; set; }
        public string? in_ip_addr { get; set; }
        public string? in_action { get; set; }
        public string? in_screen_code { get; set; }
        public string? in_screen_name { get; set; }
        public string? in_recon_code { get; set; }
        public string? in_recon_name { get; set; }
        public string? in_report_code { get; set; }
        public string? in_report_name { get; set; }
    }
    #endregion
    public class ticketdetails
    {
        public int user_gid { get; set; }
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string role_code { get; set; }
        public string role_name { get; set; }
    }

    public class ViewAttachModel
    {
        public string? in_user { get; set; }
        public int? in_ticket_gid { get; set; }
        public string? in_lang_code { get; set; }
        public string? in_ip_addr { get; set; }
        public string? in_action { get; set; }
        public int? in_queue_gid { get; set; }
        public int? in_attachment_gid { get; set; }
        public string? in_file_name { get; set; }
    }

    public class tktbulkassignmodel
    {
        public string? ticket_gid { get; set; }        
        public string? ticket_assigned { get; set; }       
        public string? in_user { get; set; }
        public string? in_ip_addr { get; set; }
        public string? in_action { get; set; }
    }
}