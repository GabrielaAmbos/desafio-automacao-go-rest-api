using FluentAssertions;
using Go_Rest_API_Automation.client;
using NUnit.Framework;

namespace Go_Rest_API_Automation.tests
{
    public class GetListarTodosOsUsuarios
    {

        [Test]
        public void DeveBuscarTodosOsUsuarios()
        {
            string endpoint = "/public-api/users/" + Hooks.GetId();

            var json = ApiClient<User>.Request(endpoint);

            json.Code.Should().Be(200);

        }
    }
}
