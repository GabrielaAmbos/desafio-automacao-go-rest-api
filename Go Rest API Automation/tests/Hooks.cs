using Go_Rest_API_Automation.client;
using Go_Rest_API_Automation.utils.providers;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{
    [SetUpFixture]
    public static class Hooks
    {
        private static int LastUserId;

        [OneTimeSetUp]
        public static void CriarUsuario()
        {
            string endpoint = "/public-api/users";

            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Leroy Borgan");
            jObjectbody.Add("gender", "Male");
            jObjectbody.Add("email", "leroyborgan123@outlook.com");
            jObjectbody.Add("status", "Active");

            var json = ApiClient<User>.Request(endpoint, Method.POST, jObjectbody);

            LastUserId = json.Data.Id;
        }

        public static string GetId()
        {
            return "/" + LastUserId;
        }

        public static string GetUsersEndpoint()
        {
            return EndpointProvider.Users();
        }

        [OneTimeTearDown]
        public static void DeveExcluirUsuario()
        {
            string endpoint = "/public-api/users/" + GetId();

            var json = ApiClient<User>.Request(endpoint, Method.DELETE);
        }
    }
}
