using Go_Rest_API_Automation.client;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{
    [SetUpFixture]
    public static class Hooks
    {
        private static int id;

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

            id = json.Data.Id;
        }

        public static int GetId()
        {
            return id;
        }

        [OneTimeTearDown]
        public static void DeveExcluirUsuario()
        {
            string endpoint = "/public-api/users/" + GetId();

            var json = ApiClient<User>.Request(endpoint, Method.DELETE);
        }
    }
}
