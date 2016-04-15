using System;
using Newtonsoft.Json;

namespace RealTimeDashboard.App.Domain
{
    public class Beat
    {
        [JsonIgnore]
        public DateTime DateTime { get; set; }

        [JsonProperty("time")]
        public string Time => DateTime.ToString("mm:ss");

        [JsonProperty("windowSize")]
        public TimeSpan WindowSize { get; set; }

        [JsonProperty("fatalCount")]
        public int FatalCount { get; set; }

        [JsonProperty("errorCount")]
        public int ErrorCount { get; set; }

        [JsonProperty("warrningCount")]
        public int WarrningCount { get; set; }

        [JsonProperty("infoCount")]
        public int InfoCount { get; set; }

        [JsonProperty("debugCount")]
        public int DebugCount { get; set; }

        [JsonProperty("perMinute")]
        public string TotalPerMinute => (WindowSize.TotalSeconds > 0 ? (FatalCount + ErrorCount + WarrningCount + InfoCount + DebugCount)/(decimal)(WindowSize.TotalSeconds/60.0) : 0).ToString("###.#");
    }
}