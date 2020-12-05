using homeapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeapi.Devices
{
    public interface IDevice
    {
        CommandsResponse ProcessRequest(string Action, Dictionary<string, dynamic> Parameters);
    }
}
