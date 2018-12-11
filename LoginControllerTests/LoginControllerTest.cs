using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http;
using Xunit;

using gtd_timer;


namespace LoginControllerTests
{
    public class LoginControllerTest
    {
        private readonly HttpClient httpClient;
        private readonly TokenClient tokenClient;

        public LoginControllerTest()
        {
            var webhost = new WebHostBuilder()
            .UseUrls("http://*:8000")
            .UseStartup<Startup>();

            var server = new TestServer(webhost);
            this.httpClient = server.CreateClient();

            var disco = DiscoveryClient.GetAsync("http://localhost:5000").Result;
            this.tokenClient = new TokenClient(disco.TokenEndpoint, "name", "password");
        }

        [Fact]
        public async void ShouldNotAllowAnonymousUser()
        {
            var result = await httpClient.GetAsync("http://localhost:44398/api/login");
            Xunit.Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
        }

        [Fact]
        public async void ShouldReturnValuesForAuthenticatedUser()
        {
            var tokenResponse = tokenClient.RequestResourceOwnerPasswordAsync("Bob", "password").Result;
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await httpClient.GetStringAsync("http://localhost:44398/api/login");
            Xunit.Assert.Equal("[\"value1\",\"value2\"]", result);
        }
    }
}
