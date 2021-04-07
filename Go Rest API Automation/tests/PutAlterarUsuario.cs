using FluentAssertions;
using Go_Rest_API_Automation.client;
using Go_Rest_API_Automation.utils;
using Go_Rest_API_Automation.utils.urls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{
    [TestFixture]
    public class PutAlterarUsuario
    {

        [Test]
        public void DeveAlterarUsuarioExistente()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Ronald McDonald");
            jObjectbody.Add("email", "ronald_mcdonald@hotmail.com");
            jObjectbody.Add("status", "Active");

            RestRequest restRequest = new RestRequest("/public-api/users/" + Hooks.GetId(), Method.PUT);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(200);
            json.Data.Name.Should().Be("Ronald McDonald");
            json.Data.Email.Should().Be("ronald_mcdonald@hotmail.com");
            json.Data.Gender.Should().Be("Male");
            json.Data.Status.Should().Be("Active");
        }

        [Test]
        public void NaoDeveAlterarUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Ronald McDonald");
            jObjectbody.Add("email", "ronald_mcdonald@hotmail.com");
            jObjectbody.Add("status", "Active");

            RestRequest restRequest = new RestRequest("/public-api/users/-" + Hooks.GetId(), Method.PUT);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(404);
        }
    }
}
