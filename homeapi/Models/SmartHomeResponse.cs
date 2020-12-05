using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace homeapi.Models
{
    public class SmartHomeResponse { 
        public string requestId { get; set; } 
        public Payload payload { get; set; }

        public SmartHomeResponse()
        {
            payload = new Payload();
        }
    }
    public class Payload
    {
        public string agentUserId { get; set; }
        public List<Device> devices { get; set; }
        public List<CommandsResponse> commands { get; set; }
    }

    public class CommandsResponse
    {
        public List<string> ids { get; set; }
        public string status { get; set; }
        public Dictionary<string, dynamic> states { get; set; }
        public string errorCode { get; set; }

        public CommandsResponse()
        {
            ids = new List<string>();
            states = new Dictionary<string, dynamic>();
        }
    }
    public class Device
    {
        public string id { get; set; }

        [JsonProperty("type")]
        public string DeviceType { get; set; }
        public List<string> traits { get; set; }
        public DeviceName name { get; set; }
        public bool willReportState { get; set; }
        public AvReceiverAttributes attributes { get; set; }

        public Device()
        {
            name = new DeviceName();
            attributes = new AvReceiverAttributes();
        }
    }
    public class DeviceName
    {
        //public List<string> defaultNames { get; set; }
        public string name { get; set; }
    }

    public class AvReceiverAttributes
    {
        public int volumeMaxLevel { get; set; }
        public bool volumeCanMuteAndUnmute { get; set; }
        public List<AcReceiverInputs> availableInputs { get; set; }
        public DeviceInfo deviceInfo { get; set; }

        public AvReceiverAttributes()
        {
            availableInputs = new List<AcReceiverInputs>();
            deviceInfo = new DeviceInfo();
        }

    }

    public class AcReceiverInputs
    {
        public string key { get; set; }
        public List<Name> names { get; set; }

        public AcReceiverInputs()
        {
            names = new List<Name>();
        }
    }

    public class Name
    {
        public List<string> name_synonym { get; set; }
        public string lang { get; set; }

        public Name()
        {
            name_synonym = new List<string>();
        }
    }

    public class DeviceInfo
    {
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string hwVersion { get; set; }
        public string swVersion { get; set; }
    }
}
