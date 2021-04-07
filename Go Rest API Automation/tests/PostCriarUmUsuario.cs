﻿using FluentAssertions;
using Go_Rest_API_Automation.client;
using Go_Rest_API_Automation.utils;
using Go_Rest_API_Automation.utils.urls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace Go_Rest_API_Automation.tests
{

    public class PostCriarUmUsuario
    {
        private int id;

        [Test]
        public void DeveCriarUmUsuario()
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
            json.Code.Should().Be(201);
            json.Data.Name.Should().Be("Leroy Borgan");
            json.Data.Gender.Should().Be("Male");
            json.Data.Email.Should().Be("leroy_borgan@outlook.com");
            json.Data.Status.Should().Be("Active");

            restRequest = new RestRequest("/public-api/users/" + id, Method.DELETE);
            restRequest.AddParameter("application/json", ParameterType.RequestBody);
            restRequest.AddHeader("Authorization", Token.BasicToken());

            restResponse = restClient.Execute(restRequest);
        }
    }
}
