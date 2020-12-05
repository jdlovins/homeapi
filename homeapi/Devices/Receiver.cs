using homeapi.Models;
using PrimS.Telnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeapi.Devices
{
    public class Receiver : IDevice
    {
        private Dictionary<string, Dictionary<string, Func<Client, Dictionary<string, dynamic>, Dictionary<string, dynamic>>>> _commands = new Dictionary<string, Dictionary<string, Func<Client, Dictionary<string, dynamic>, Dictionary<string, dynamic>>>>();
        private string _id { get; set; }
        public Receiver(string Id)
        {
            _commands.Add("1", new Dictionary<string, Func<Client, Dictionary<string, dynamic>, Dictionary<string, dynamic>>>()
            {
                {"action.devices.commands.setVolume", ProcessVolumeRequest},
                {"action.devices.commands.OnOff", ProcessPowerRequest },
                {"action.devices.commands.SetInput", ProcessInputRequest }
            });

            _id = Id;
        }

        public CommandsResponse ProcessRequest(string Action, Dictionary<string, dynamic> Parameters)
        {
            CommandsResponse resp = new CommandsResponse();
            resp.ids.Add(_id);
            try
            {
                using Client client = new Client("10.10.10.31", 23, new System.Threading.CancellationToken());
                resp.states = _commands[_id][Action](client, Parameters);
                resp.status = "SUCCESS";
            }catch(Exception ex)
            {
                resp.status = "ERROR";
                resp.errorCode = "hardError";
            }

            return resp;
        }

        private Dictionary<string, dynamic> ProcessVolumeRequest(Client client, Dictionary<string, dynamic> data)
        {
            client.WriteLine($"MV{data["volumeLevel"]}");
            var resp = client.TerminatedReadAsync("\r").Result;
            return new Dictionary<string, dynamic>()
            {
                { "online", true },
                { "currentVolume", resp.ToString()  },
                { "isMuted", false }
            };
        }

        private Dictionary<string, dynamic> ProcessPowerRequest(Client client, Dictionary<string, dynamic> data)
        {
            string pwrStatus = (bool)data["on"] ? "ON" : "STANDBY";
            client.WriteLine($"PW{pwrStatus}");

            return new Dictionary<string, dynamic>()
            {
                { "online", true },
                { "on", (bool)data["on"] }
            };
        }

        private Dictionary<string, dynamic> ProcessInputRequest(Client client, Dictionary<string, dynamic> data)
        {
            client.WriteLine($"SI{data["newInput"]}");

            return new Dictionary<string, dynamic>()
            {
                { "online", true },
                { "currentInput", data["newInput"] }
            };
        }
    }
}