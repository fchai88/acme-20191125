using Acme.Api.Controllers;
using Xunit;

namespace Acme.UnitTests
{
    public class GreetingBuilderTest
    {
        [Fact]
        public void Build_Returns_DefaultGreetings()
        {
            var greetingsBuilder = new GreetingBuilder();

            Greeting result = greetingsBuilder.Build();

            Assert.Equal("Hello, World!", result.Text);
        }

        [Fact]
        public void BuildWithName_Returns_GreetingWithName()
        {
            var greetingsBuilder = new GreetingBuilder();
            string name = "Felix Chai";

            Greeting result = greetingsBuilder.Build(name);

            Assert.Equal($"Hello, {name}!", result.Text);
        }
        
    }
}
