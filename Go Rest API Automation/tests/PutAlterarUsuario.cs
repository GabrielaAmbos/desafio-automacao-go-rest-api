using FluentAssertions;
using Go_Rest_API_Automation.client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{
    [TestFixture]
    public class PutAlterarUsuario
    {
        private int id;

        [Test]
        public void DeveAlterarUsuarioExistente()
        {
            string endpoint = "/public-api/users/" + Hooks.GetId();

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Ronald McDonald");
            jObjectbody.Add("email", "ronaldmcdonald@hotmail.com");
            jObjectbody.Add("status", "Active");

            var json = ApiClient<User>.Request(endpoint, Method.PUT, jObjectbody);

            json.Code.Should().Be(200);
            json.Data.Name.Should().Be("Ronald McDonald");
            json.Data.Email.Should().Be("ronaldmcdonald@hotmail.com");
            json.Data.Gender.Should().Be("Male");
            json.Data.Status.Should().Be("Active");
            id = json.Data.Id;
        }

        [Test]
        public void NaoDeveAlterarUsuario()
        {
            string endpoint = "/public-api/users/-" + Hooks.GetId();

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Ronald McDonald");
            jObjectbody.Add("email", "ronald_mcdonald@hotmail.com");
            jObjectbody.Add("status", "Active");

            var json = ApiClient<User>.Request(endpoint, Method.PUT, jObjectbody);

            json.Code.Should().Be(404);
        }

        [TearDown]
        public void DeveExcluirUsuarioExistente()
        {
            string endpoint = "/public-api/users/" + id;

            var json = ApiClient<User>.Request(endpoint, Method.DELETE);
        }
    }
}
