using FluentAssertions;
using Go_Rest_API_Automation.client;
using Go_Rest_API_Automation.utils;
using Go_Rest_API_Automation.utils.urls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace Go_Rest_API_Automation.tests
{
    [TestFixture]
    public class GetBuscarUsuario
    {

        [Test]
        public void DeveBuscarUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/" + Hooks.GetId(), Method.GET);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(200);
            json.Data.Name.Should().Be("Leroy Borgan");
            json.Data.Gender.Should().Be("Male");
            json.Data.Email.Should().Be("leroy_borgan@outlook.com");
            json.Data.Status.Should().Be("Active");

        }

        [Test]
        public void NaoDeveBuscarUsuario()
        {

            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/-" + Hooks.GetId(), Method.GET);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(404);
        }
    }
}
