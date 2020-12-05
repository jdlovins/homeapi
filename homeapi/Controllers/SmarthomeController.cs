using homeapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrimS.Telnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homeapi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SmarthomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] SmartHomeRequest smartRequest)
        {
            if (Request.Headers.ContainsKey("Authorization"))
            {
                SmartHomeResponse resp = new SmartHomeResponse();
                resp.requestId = smartRequest.requestId;
                resp.payload.agentUserId = "josh";

                foreach (var intent in smartRequest.inputs)
                {

                    if (intent.intent == "action.devices.SYNC")
                    {
                        resp.payload.devices = new List<Device>() { Utils.GetDevice() };

                    }
                    else if (intent.intent == "action.devices.EXECUTE")
                    {
                        var commandInfo = intent.payload.commands.FirstOrDefault();
                        var executionInfo = commandInfo.execution.FirstOrDefault();

                        var commandsResponse = ExecutionFactory.Execute(commandInfo.devices.FirstOrDefault().id, executionInfo.command, executionInfo.parameters);

                        resp.payload.commands = new List<CommandsResponse>() { commandsResponse };
                    }                    
                }

                return Ok(resp);
            }

            return StatusCode(400);
        }

        [HttpGet]
        [Route("fakeauth")]
        public IActionResult FakeAuth(string client_id, string redirect_uri, string state, string response_type)
        {
            Console.WriteLine("Got Fake auth request");
            string url = $"{redirect_uri}?code={Utils.RandomString(10)}&state={state}";
            return Redirect(url);
        }

        [HttpPost]
        [Route("faketoken")]
        public IActionResult FakeToken([FromForm] OAuthTokenRequest request)
        {
            Console.WriteLine("Got Fake token request");
            object resp = null;
            if (request.grant_type == "authorization_code")
            {
                resp = new
                {
                    token_type = "Bearer",
                    access_token = Utils.RandomString(10),
                    refresh_token = Utils.RandomString(10),
                    expires_in = 3600
                };
            }
            else
            {
                resp = new
                {
                    token_type = "Bearer",
                    access_token = Utils.RandomString(10),
                    expires_in = 3600
                };
            }

            return Ok(resp);
        }

    }
}
