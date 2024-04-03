namespace Recon_proto.Models
{
    public class QcdMasterModel
    {
        public int master_gid { get; set; }
        public string master_syscode { get; set; }
        public string master_code { get; set; }
        public string master_short_code { get; set; }
        public string master_name { get; set; }
        public string parent_master_syscode { get; set; }
        public string active_status { get; set; }
        public string active_status_desc { get; set; }
        public string master_multiple_name { get; set; }
        public string? parent_master_syscode_desc { get; set; }
		public string depend_flag { get; set; }
		public string depend_parent_master_syscode_desc { get; set; }
		public string depend_parent_master_syscode { get; set; }

	}
}
