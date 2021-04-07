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
    public class GetBuscarUsuario
    {
        private int id;

        [SetUp]
        public void CriarUmUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Thomas Oliver");
            jObjectbody.Add("gender", "Male");
            jObjectbody.Add("email", "thomas.oliver@outlook.com");
            jObjectbody.Add("status", "Active");

            RestRequest restRequest = new RestRequest("/public-api/users", Method.POST);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            id = json.Data.Id;

        }

        [Test]
        public void DeveBuscarUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/" + id, Method.GET);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(200);
            json.Data.Name.Should().Be("Thomas Oliver");
            json.Data.Gender.Should().Be("Male");
            json.Data.Email.Should().Be("thomas.oliver@outlook.com");
            json.Data.Status.Should().Be("Active");

        }

        [Test]
        public void NaoDeveBuscarUsuario()
        {

            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/-" + id, Method.GET);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(404);
        }

        [TearDown]
        public void DeveExcluirUsuarioExistente()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/" + id, Method.DELETE);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);
        }
    }
}
