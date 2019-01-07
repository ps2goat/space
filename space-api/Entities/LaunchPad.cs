using Newtonsoft.Json;
using System.Collections.Generic;

namespace SpaceApi.Entities
{
    public class LaunchPad
    {
        [JsonProperty("padid")]
        public int PadId { get; set; }
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public Location Location { get; set; }
        public List<string> VehiclesLaunched { get; set; }
        public int AttemptedLaunches { get; set; }
        public int SuccessfulLaunches { get; set; }
        public string Wikipedia { get; set; }
        public string Details { get; set; }
    }
}