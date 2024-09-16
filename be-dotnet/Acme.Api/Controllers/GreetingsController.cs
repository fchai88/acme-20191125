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

        [HttpGet()]
        public CommandResponse GetWithName(string name)
        {
            var greetingBuilder = new GreetingBuilder();
            return CommandResponse.WithData(greetingBuilder.Build(name));
        }

    }

    public class GreetingBuilder
    {
        public Greeting Build()
        {
            return new Greeting
            {
                Text = "Hello, World!"
            };

        }
        public Greeting Build(string name)
        {
            return new Greeting
            {
                Text = $"Hello, {name}!"
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