using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Update;

namespace XboxUpdateTool
{
    class Program
    {

        static void Main(string[] args)
        {
            if(args.Length < 2)
            {
                Console.WriteLine("Usage: XboxUpdateTool.exe authtoken contentid");
            }
            else
            {
                string authtoken = args[0];
                CheckForUpdateAsync(authtoken, args[1]).Wait();
            }
            
        }
        
        static void UpdateInfo(UpdateXboxLive update)
        {

            Console.WriteLine($"Update Version: {update.TargetVersionId}\nUpdate Type: {update.UpdateType}");

        }


        static async Task DownloadUpdateAsync(UpdateXboxLive update)
        {
            WebClient downloader = new WebClient();

            Console.WriteLine($"Downloading Update: {update.TargetVersionId} - Size: {update.Files.EstimatedTotalDownloadSize} KB");
            foreach(var file in update.Files.PurpleFiles)
            {
                Console.WriteLine($"Downloading {file.Name} - {file.Size} kb");
                Uri updatepath = new Uri($"http://assets1.xboxlive.com/{file.RelativeUrl}");
                await downloader.DownloadFileTaskAsync(updatepath, file.Name + "_" + update.ContentId);
                Console.WriteLine($"Downloaded {file.Name} as {file.Name}_{update.ContentId}");
            }  
        }

        async static Task CheckForUpdateAsync(string authtoken, string contentid)
        {
            Uri GetSystemUpdatePackageUri = new Uri($"https://update.xboxlive.com/GetSystemUpdatePackage/{contentid}");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authtoken);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            StringContent content = new StringContent("{\"UpdateMode\":3,\"LicenseProtocol\":4,\"FileIncludeFilter\":[\"updater.xvd\"],\"IgnoreRootLicense\":0}");
            HttpResponseMessage httpResponse = await client.PostAsync(GetSystemUpdatePackageUri, content);
            if (httpResponse.IsSuccessStatusCode)
            {
                string update = httpResponse.Content.ReadAsStringAsync().Result;
                var updatejson = UpdateXboxLive.FromJson(update);
                if (updatejson.UpdateType != "None")
                {
                    Console.WriteLine("***Update Found***");
                    UpdateInfo(updatejson);
                    await DownloadUpdateAsync(updatejson);
                }
                else
                {
                    Console.WriteLine("No Update Found.");
                }

            }
            else
            {
                Console.WriteLine($"update.xboxlive.com error - status code {httpResponse.StatusCode.ToString()}");
            }
            
        }
    }
}
