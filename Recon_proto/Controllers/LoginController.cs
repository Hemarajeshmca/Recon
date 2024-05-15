using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Recon_proto.Models;
using static Recon_proto.Controllers.LoginController;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using DocumentFormat.OpenXml.EMMA;

namespace Recon_proto.Controllers
{
    public class LoginController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string urlstring = "";
        string ipAddress = "";
		public LoginController(
     IConfiguration configuration,
     ILogger<LoginController> logger,
     IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
       

        [HttpPost]
        public string Login_validation([FromBody] Login_model1 model)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            Login_model loginmodel = new Login_model();
            List<user_model> objcat_lst = new List<user_model>();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    ipAddress = GetLocalIPAddress();
					var pass = Encrypt(model.Password);
                    loginmodel.user_id = model.UserName;
                    loginmodel.password = pass;
                    loginmodel.ip = ipAddress;
                    loginmodel.msg = "";
                    loginmodel.ip_address = ipAddress;
					loginmodel.datasource = "";
                    loginmodel.user_code = "";
                    loginmodel.user_name = "";
                    loginmodel.oldpassword = "";
                    string Urlcon = "UserManagement/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(loginmodel), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("Loginvalidation", content).Result;
                    Stream data = response.Content.ReadAsStreamAsync().Result;
                    StreamReader reader = new StreamReader(data);
                    post_data = reader.ReadToEnd();
                    string d2 = JsonConvert.DeserializeObject<string>(post_data);
                    result = JsonConvert.DeserializeObject<DataTable>(d2);
                    for (int i = 0; i < result.Rows.Count; i++)
                    {
                        user_model objcat = new user_model();
                        objcat.user_gid = Convert.ToInt32(result.Rows[i]["user_gid"]);
						objcat.user_code = result.Rows[i]["user_code"].ToString();
						objcat.user_name = result.Rows[i]["user_name"].ToString();
                        objcat.passwordexpdate = result.Rows[i]["password_expiry_date"].ToString();
                        objcat.usergroup_code =result.Rows[i]["usergroup_code"].ToString();
						objcat.usergroup_desc = result.Rows[i]["usergroup_desc"].ToString();
						objcat.result = Convert.ToInt32(result.Rows[i]["out_result"]);
                        objcat.msg = result.Rows[i]["out_msg"].ToString();
                        objcat.oldpassworrd = Decrypt(pass);
                        objcat.user_status = result.Rows[i]["user_status"].ToString();
                        objcat.lastlogin = Convert.ToDateTime(result.Rows[i]["lastlogin"].ToString());
						objcat_lst.Add(objcat);
                        ViewBag.user_gid = objcat.user_gid;
                        ViewBag.user_name = objcat.user_name;
                        _configuration.GetSection("AppSettings")["user_code"] = model.UserName;
                        _configuration.GetSection("AppSettings")["lang_code"] = "en_US";
                        _configuration.GetSection("AppSettings")["role_code"] = result.Rows[i]["usergroup_code"].ToString();
						_configuration.GetSection("AppSettings")["lastlogin"] = result.Rows[i]["lastlogin"].ToString();
						_configuration.GetSection("AppSettings")["ipAddress"] = ipAddress;
                        HttpContext.Session.SetString("user_code", model.UserName);
                        HttpContext.Session.SetString("username", result.Rows[i]["user_name"].ToString());
                        //HttpContext.Session.SetString("mindate", result.Rows[i]["min_tran_date"].ToString());
                        //HttpContext.Session.SetString("fin_date", result.Rows[i]["fin_start_date"].ToString());
                        HttpContext.Session.SetString("user_gid", result.Rows[i]["user_gid"].ToString());
						HttpContext.Session.SetString("role_code", result.Rows[i]["usergroup_code"].ToString());
						HttpContext.Session.SetString("lang_code", "en_US");
						HttpContext.Session.SetString("usergroup_code", result.Rows[i]["usergroup_code"].ToString());
                        HttpContext.Session.SetString("userrole", "ADMIN");
                        HttpContext.Session.SetString("ipAddress", ipAddress);
                    }
                    return JsonConvert.SerializeObject(objcat_lst);
                }
            } catch (Exception ex)
            {
                CommonController objcom = new CommonController(_configuration);
                objcom.errorlog(ex.Message, "Login_validation");
                return ex.Message;
            }
        }

		private static string GetLocalIPAddress()
		{
			string hostName = Dns.GetHostName();
			IPAddress[] addresses = Dns.GetHostAddresses(hostName);
			IPAddress ipv4Address = addresses.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
			return ipv4Address?.ToString() ?? "No IPv4 address found";
		}

		[HttpPost]
        public IActionResult changepassword_Save([FromBody] changePassword model)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            string post_data = "";
            List<user_model> objcat_lst = new List<user_model>();
            try
            {
                var dataFromSession = HttpContext.Session.GetString("user_code");
                var oldpassword = Encrypt(model.oldpass);
                var pass = Encrypt(model.newpass);
                changepass_save changepassModal = new changepass_save();
                changepassModal.old_password = oldpassword;
                changepassModal.new_password = pass;
                changepassModal.user_id = dataFromSession;
                changepassModal.out_msg = "";
                using (var client = new HttpClient())
                {
                    string[] result = { };
                    try
                    {
                        string Urlcon = "UserManagement/";
                        client.BaseAddress = new Uri(urlstring + Urlcon);
                        //client.BaseAddress = new Uri("https://localhost:44348/api/UserManagement/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpContent content = new StringContent(JsonConvert.SerializeObject(changepassModal), UTF8Encoding.UTF8, "application/json");
                        var response = client.PostAsync("Loginvalidation", content).Result;
                        Stream data = response.Content.ReadAsStreamAsync().Result;
                        StreamReader reader = new StreamReader(data);
                        post_data = reader.ReadToEnd();
                        DataTable d2 = JsonConvert.DeserializeObject<DataTable>(post_data);
                        //  result = JsonConvert.DeserializeObject<DataTable>(d2);

                        //result = JsonConvert.DeserializeObject<DataTable>(d2);
                        //result = (string[])JsonConvert.DeserializeObject(post_data1, result.GetType());
                        return Json(d2);
                    }
                    catch (Exception ex)
                    {
                        string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                        CommonController objcom = new CommonController(_configuration);
                        objcom.errorlog(ex.Message, "changepassword_Save");
                        return Json(ex.Message);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                string control = this.ControllerContext.RouteData.Values["controller"].ToString();
                //LogHelper.WriteLog(ex.ToString(), control);
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }
		public IActionResult session_expires()
		{
			return View();
		}

		public class changePassword
        {
            public string? user_gid { get; set; }
            public string? oldpass { get; set; }
            public string? newpass { get; set; }
        }

        public class Login_model1
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }


        private string Encrypt(string clearText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return clearText;
        }


        private string Decrypt(string cipherText)
        {
            try
            {
                string EncryptionKey = "MAKV2SPBNI99212";
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return cipherText;
        }
		#region signout
		[HttpPost]
		public ActionResult Signout()
		{
			try
			{
				_configuration.GetSection("AppSettings")["user_code"] = "";
				_configuration.GetSection("AppSettings")["lang_code"] = "";
				_configuration.GetSection("AppSettings")["role_code"] = "";
				_configuration.GetSection("AppSettings")["lastlogin"] = "";
				_configuration.GetSection("AppSettings")["ipAddress"] = "";
				return Redirect("~/Login/Login");
			}
			catch (Exception ex)
			{
				CommonController objcom = new CommonController(_configuration);
				objcom.errorlog(ex.Message, "Signout");
				return Json(ex.Message);
			}
		}
        #endregion

        #region lastlogin
        [HttpPost]
        public JsonResult lastloginsession([FromBody] lastloginmodel context)
        {
            urlstring = _configuration.GetSection("Appsettings")["apiurl"].ToString();
            DataTable result = new DataTable();
            string post_data = "";
            try
            {
                using (var client = new HttpClient())
                {
                    string Urlcon = "UserManagement/";
                    client.BaseAddress = new Uri(urlstring + Urlcon);
                    //client.BaseAddress = new Uri("https://localhost:44348/api/Recon/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    client.DefaultRequestHeaders.Add("user_code", _configuration.GetSection("AppSettings")["user_code"].ToString());
                    client.DefaultRequestHeaders.Add("lang_code", _configuration.GetSection("AppSettings")["lang_code"].ToString());
                    client.DefaultRequestHeaders.Add("role_code", _configuration.GetSection("AppSettings")["role_code"].ToString());
                    client.DefaultRequestHeaders.Add("ipaddress", _configuration.GetSection("AppSettings")["ipaddress"].ToString());
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(context), UTF8Encoding.UTF8, "application/json");
                    var response = client.PostAsync("lastsession", content).Result;
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
                objcom.errorlog(ex.Message, "lastlogin");
                return Json(ex.Message);
            }
        }

        public class lastloginmodel
        {            
            public String user_code { get; set; }
			public string status { get; set; }
		}
        #endregion
    }
}
