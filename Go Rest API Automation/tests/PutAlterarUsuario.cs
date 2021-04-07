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
    public class PutAlterarUsuario
    {

        private int id;

        [Before]
        public void CriarUsuario()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Leroy Bogan");
            jObjectbody.Add("gender", "Male");
            jObjectbody.Add("email", "leroy_bogan@hotmail.com");
            jObjectbody.Add("status", "Active");

            RestRequest restRequest = new RestRequest("/public-api/users", Method.POST);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            id = json.Data.Id;
        }

        [Test]
        public void DeveAlterarUsuarioExistente()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Ronald McDonald");
            jObjectbody.Add("email", "ronald_mcdonald@hotmail.com");
            jObjectbody.Add("status", "Active");

            RestRequest restRequest = new RestRequest("/public-api/users/" + id, Method.PUT);

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

            RestRequest restRequest = new RestRequest("/public-api/users/-" + id, Method.PUT);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(404);
        }

        [Test]
        public void NaoDeveAlterarUsuarioInexistente()
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", " ");
            jObjectbody.Add("email", " ");
            jObjectbody.Add("status", " ");

            RestRequest restRequest = new RestRequest("/public-api/users/" + id, Method.PUT);

            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            var json = JsonConvert.DeserializeObject<User>(restResponse.Content);
            json.Code.Should().Be(404);
        }

        [After]
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
