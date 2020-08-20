using System;
using System.IO;
using System.Linq;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SQLSeed
{
    public class Program
    {
        static void Main(string[] args)
        {
            // unzip files
            var outputPath = Path.Combine(Path.GetTempPath(), "codespace-test");
            Console.WriteLine($"zip output path: {outputPath}");
            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }
            Directory.CreateDirectory(outputPath);
            ExtractZipFile("adventureworks-small2.zip", outputPath);

            var sqlFilePath = Path.Combine(outputPath, "adventureworks-small2.sql");
            Console.WriteLine($"sqlFilePath  is : {sqlFilePath}");

            var sqlConnectionString = GetDbConnection();
            Console.WriteLine($"Connection string is : {sqlConnectionString}");

            var connectionsStringParts = sqlConnectionString.Split(";").Where(x => !x.StartsWith("Database")).ToList();
            var connectionsString = string.Join(";", connectionsStringParts);

            Console.WriteLine("Starting SQL script");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            FileInfo fileInfo = new FileInfo(sqlFilePath);
            string script = fileInfo.OpenText().ReadToEnd();
            using (SqlConnection connection = new SqlConnection(connectionsString))
            {
                Server server = new Server(new ServerConnection(connection));
                server.ConnectionContext.ExecuteNonQuery(script);
            }
            watch.Stop();
            Console.WriteLine($"DONE in {watch.Elapsed.ToString(@"mm\:ss")}");
        }

        private static string GetDbConnection()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string strConnection = builder.Build().GetConnectionString("DbConnection");

            return strConnection;
        }

        private static void ExtractZipFile(string archivePath, string outFolder)
        {
            using (Stream fsInput = File.OpenRead(archivePath))
            using (var zf = new ZipFile(fsInput))
            {
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        // Ignore directories
                        continue;
                    }
                    String entryFileName = zipEntry.Name;
                    // to remove the folder from the entry:
                    //entryFileName = Path.GetFileName(entryFileName);
                    // Optionally match entrynames against a selection list here
                    // to skip as desired.
                    // The unpacked length is available in the zipEntry.Size property.

                    // Manipulate the output filename here as desired.
                    var fullZipToPath = Path.Combine(outFolder, entryFileName);
                    var directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    // 4K is optimum
                    var buffer = new byte[4096];

                    // Unzip file in buffered chunks. This is just as fast as unpacking
                    // to a buffer the full size of the file, but does not waste memory.
                    // The "using" will close the stream even if an exception occurs.
                    using (var zipStream = zf.GetInputStream(zipEntry))
                    using (Stream fsOutput = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, fsOutput, buffer);
                    }
                }
            }
        }
    }
}
