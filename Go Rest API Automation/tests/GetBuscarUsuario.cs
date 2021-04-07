using FluentAssertions;
using Go_Rest_API_Automation.client;
using NUnit.Framework;

namespace Go_Rest_API_Automation.tests
{
    [TestFixture]
    public class GetBuscarUsuario
    {
        [Test]
        public void DeveBuscarUsuario()
        {
            string endpoint = "/public-api/users/" + Hooks.GetId();

            var json = ApiClient<User>.Request(endpoint);

            json.Code.Should().Be(200);
            json.Data.Name.Should().Be("Leroy Borgan");
            json.Data.Gender.Should().Be("Male");
            json.Data.Email.Should().Be("leroyborgan123@outlook.com");
            json.Data.Status.Should().Be("Active");

        }

        [Test]
        public void NaoDeveBuscarUsuario()
        {
            string endpoint = "/public-api/users/-" + Hooks.GetId();

            var json = ApiClient<User>.Request(endpoint);

            json.Code.Should().Be(404);
        }
    }
}
