using System.IO.Compression;

namespace Recon_proto
{
	public class Fileservice
	{
		public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory, string y)
		{
			string jobid = y;
			int filelength = jobid.Length;
			var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
			var files = Directory.GetFiles(subDirectory).ToList();
			string supportedExtensions = String.Concat(jobid.ToString(), ".csv,", jobid.ToString(), "_*.*");

			List<string> myList = new List<string>();

			foreach (string file in Directory.GetFiles(subDirectory, String.Concat(jobid, ".*"), SearchOption.AllDirectories).Union(
									Directory.GetFiles(subDirectory, String.Concat(jobid, "_*.*"), SearchOption.AllDirectories)))
			{

				var fil = Path.GetFileName(file);
				string filess = file;
				myList.Add(filess);

			}


			using (var memoryStream = new MemoryStream())
			{
				using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
				{
					myList.ForEach(file =>
					{
						var filename = Path.GetFileName(file);
						//var theFile = archive.CreateEntry(file);
						var theFile = archive.CreateEntry(filename);

						// var res= theFile.Name;

						using (var streamWriter = new StreamWriter(theFile.Open()))
						{
							streamWriter.Write(File.ReadAllText(file));
						}
						// }

					});
				}
				return ("application/zip", memoryStream.ToArray(), zipName);
			}





			//return ("ss", null, "");


		}

	}
}
