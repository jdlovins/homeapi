using homeapi.Devices;
using homeapi.Models;
using PrimS.Telnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeapi
{
    public static class ExecutionFactory
    {
        private static Dictionary<string, IDevice> _devices { get; set; }

        static ExecutionFactory()
        {
            _devices = new Dictionary<string, IDevice>()
           {
               { "1", new Receiver("1") }
           };
        }
        public static CommandsResponse Execute(string Id, string Action, Dictionary<string, dynamic> Parameters)
        {
            return _devices[Id].ProcessRequest(Action, Parameters);
        }
    }
}
