using FluentAssertions;
using Go_Rest_API_Automation.client;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{
    [TestFixture]
    public class DelExcluirUsuario
    {

        [Test]
        public void DeveExcluirUsuarioExistente()
        {
            string endpoint = "/public-api/users/" + Hooks.GetId();

            var json = ApiClient<User>.Request(endpoint, Method.DELETE);

            json.Code.Should().Be(204);
        }

        [Test]
        public void NaoDeveExcluirUsuarioInexistente()
        {
            string endpoint = "/public-api/users/-" + Hooks.GetId();

            var json = ApiClient<User>.Request(endpoint, Method.DELETE);

            json.Code.Should().Be(404);
        }
    }
}
