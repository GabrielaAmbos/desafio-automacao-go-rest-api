using FluentAssertions;
using Go_Rest_API_Automation.client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{

    public class PostCriarUmUsuario
    {
        private int LastUserId;

        [Test]
        public void DeveCriarUmUsuario()
        {
            string endpoint = Hooks.GetUsersEndpoint();

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Tomas Oliver");
            jObjectbody.Add("gender", "Male");
            jObjectbody.Add("email", "tomasoliver@outlook.com");
            jObjectbody.Add("status", "Active");

            var json = ApiClient<User>.Request(endpoint, Method.POST, jObjectbody);

            LastUserId = json.Data.Id;
            json.Code.Should().Be(201);
            json.Data.Name.Should().Be("Tomas Oliver");
            json.Data.Gender.Should().Be("Male");
            json.Data.Email.Should().Be("tomasoliver@outlook.com");
            json.Data.Status.Should().Be("Active");
        }

        [TearDown]
        public void DeveExcluirUsuarioExistente()
        {
            string endpoint = Hooks.GetUsersEndpoint() + LastUserId;

            var json = ApiClient<User>.Request(endpoint, Method.DELETE);
        }
    }
}
