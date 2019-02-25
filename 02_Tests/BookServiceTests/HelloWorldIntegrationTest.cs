using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using Xunit;
using System.Threading.Tasks;

namespace BookServiceTests
{
    public class HelloWorldIntegrationTest : IClassFixture<WebApplicationFactory<BookService.Startup>>
    {
        private readonly WebApplicationFactory<BookService.Startup> webAppFactory;

        public HelloWorldIntegrationTest(WebApplicationFactory<BookService.Startup> webAppFactory)
        {
            this.webAppFactory = webAppFactory;
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/foo")]
        public async Task ItShallReturnHelloWorld(string url)
        {
            // Given
            var client = webAppFactory.CreateClient();

            // When
            var response = await client.GetAsync(url);

            // Then
            response.IsSuccessStatusCode.Should().BeTrue();
            (await response.Content.ReadAsStringAsync()).Should().Contain("Hello World");
        }
    }
}
