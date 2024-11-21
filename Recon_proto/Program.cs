namespace Recon_proto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
					//webBuilder.UseKestrel();
					webBuilder.UseStartup<Startup>();
					//webBuilder.UseUrls("http://localhost:8001"); 
					webBuilder.ConfigureKestrel(options =>
					{
						// Set the maximum request body size (in bytes)
						options.Limits.MaxRequestBodySize = 524288000; // 500 MB
					});
				});

    }
}
   

