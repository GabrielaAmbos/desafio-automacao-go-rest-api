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
    public class DelExcluirUsuario
    {
        private int id;

        [SetUp]
        public void CriarUmUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Leroy Borgan");
            jObjectbody.Add("gender", "Male");
            jObjectbody.Add("email", "leroy_borgan@outlook.com");
            jObjectbody.Add("status", "Active");

            RestRequest restRequest = new RestRequest("/public-api/users", Method.POST);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            id = json.Data.Id;
        }

        [Test]
        public void DeveExcluirUsuarioExistente()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            RestRequest restRequest = new RestRequest("/public-api/users/" + id, Method.DELETE);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(204);
        }

        [Test]
        public void NaoDeveExcluirUsuarioInexistente()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/-" + id, Method.DELETE);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(404);
        }

        [TearDown]
        public void DeveExcluirUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest("/public-api/users/" + id, Method.DELETE);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);
        }
    }
}
