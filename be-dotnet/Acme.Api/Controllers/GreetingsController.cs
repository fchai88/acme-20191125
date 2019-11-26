using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Acme.Api.Controllers
{
    [ApiController]
    [Route("greetings")]
    public class GreetingsController : ControllerBase
    {
        private readonly ILogger<GreetingsController> _logger;

        public GreetingsController(
            IConfiguration configuration,
            ILogger<GreetingsController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public CommandResponse Get()
        {
            var greetingBuilding = new GreetingBuilder();
            
            return CommandResponse.WithData(greetingBuilding.Build());
        }
    }

    public class GreetingBuilder
    {
        public Greeting Build(string name = "")
        {
            string greetingText = "Hello, ";

            if (name.Length <= 0)
            {
                greetingText += "World!";
            }
            else
            {
                greetingText += $"{name}!";
            }

            return new Greeting
            {
                Text = greetingText
            };
        }
    }

    public class Greeting
    {
        public string Text { get; set; }
    }

    public class CommandResponse
    {
        public string Error { get; set; }
        public object Data { get; set; }

        public static CommandResponse WithData(object data)
        {
            return new CommandResponse
            {
                Data = data
            };
        }

        public static CommandResponse WithError(string error)
        {
            return new CommandResponse
            {
                Error = error
            };
        }
    }
}