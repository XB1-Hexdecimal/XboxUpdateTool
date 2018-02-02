using System;
using System.Net;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace XboxUpdateTool
{
 

    public partial class CacheGroupId
    {
        [JsonProperty("CacheGroupId")]
        public string PurpleCacheGroupId { get; set; }

        [JsonProperty("SystemVersionId")]
        public string SystemVersionId { get; set; }
    }

    public partial class CacheGroupId
    {
        public static CacheGroupId FromJson(string json) => JsonConvert.DeserializeObject<CacheGroupId>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this CacheGroupId self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
