using System;
using Acme.Api.Controllers;
using Xunit;

namespace Acme.UnitTests.Test
{
    public class GreeterBuilderTests
    {
        [Fact]
        public void Build_Default_ReturnsGreeting()
        {
            var greetingBuilder = new Api.Controllers.GreetingBuilder();

            Greeting result = greetingBuilder.Build();

            Assert.Equal("Hello, World!", result.Text);
        }

        [Fact]
        public void Build_WithCustomName_ReturnsCustomGreeting()
        {
            var greetingBuilder = new Api.Controllers.GreetingBuilder();

            Greeting result = greetingBuilder.Build("Emmanuel");

            Assert.Equal("Hello, Emmanuel!", result.Text);
        }
    }
}
