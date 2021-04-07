using Go_Rest_API_Automation.utils;
using Go_Rest_API_Automation.utils.urls;
using NUnit.Framework;
using RestSharp;


namespace Go_Rest_API_Automation.tests
{
    public class GetListarTodosOsUsuarios
    {

        [Test]
        public void DeveBuscarTodosOsUsuarios()
        {

            RestClient restClient = new RestClient(BaseUrl.UrlBase());

            RestRequest restRequest = new RestRequest("/public-api/users/", Method.GET);

            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }
    }
}
