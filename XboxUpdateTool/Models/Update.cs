
namespace XboxUpdateTool.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class UpdateXboxLive
    {
        [JsonProperty("ContentId")]
        public string ContentId { get; set; }

        [JsonProperty("TargetVersionId")]
        public string TargetVersionId { get; set; }

        [JsonProperty("RootLicense")]
        public string RootLicense { get; set; }

        [JsonProperty("UpdateType")]
        public string UpdateType { get; set; }

        [JsonProperty("Files")]
        public Files Files { get; set; }

        [JsonProperty("ApiSource")]
        public string ApiSource { get; set; }
    }

    public partial class Files
    {
        [JsonProperty("CdnRoots")]
        public List<string> CdnRoots { get; set; }

        [JsonProperty("Files")]
        public List<File> PurpleFiles { get; set; }

        [JsonProperty("EstimatedTotalDownloadSize")]
        public long EstimatedTotalDownloadSize { get; set; }
    }

    public partial class File
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Size")]
        public long Size { get; set; }

        [JsonProperty("RelativeUrl")]
        public string RelativeUrl { get; set; }

        [JsonProperty("License")]
        public string License { get; set; }
    }

    public partial class UpdateXboxLive
    {
        public static UpdateXboxLive FromJson(string json) => JsonConvert.DeserializeObject<UpdateXboxLive>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this UpdateXboxLive self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
