using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeapi.Models
{

    public class SmartHomeRequest
    {
        public string requestId { get; set; }
        public List<RequestInput> inputs { get; set; }

        public SmartHomeRequest()
        {
            inputs = new List<RequestInput>();
        }
    }
    public class RequestInput
    {
        public string intent { get; set; }
        public RequestPayload payload { get; set; }
    }
    public class RequestPayload
    {
        public List<Command> commands { get; set; }
    }
    public class Command
    {
        public List<RequestDevice> devices { get; set; }
        public List<Execution> execution { get; set; }
    }
    public class RequestDevice
    {
        public string id { get; set; }
    }

    public class Execution
    {
        public string command { get; set; }
        [JsonProperty("params")]
        public Dictionary<string, dynamic> parameters { get; set; }
    }   
}
