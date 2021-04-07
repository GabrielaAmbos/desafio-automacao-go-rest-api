

using Go_Rest_API_Automation.tests;
using Go_Rest_API_Automation.utils;
using Go_Rest_API_Automation.utils.urls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Go_Rest_API_Automation.client
{
    public static class ApiClient<T>
    {
        public static T Request(string endpoint, Method method = Method.GET, JObject jObjectbody = null)
        {
            RestClient restClient = new RestClient(BaseUrl.UrlBase());
            RestRequest restRequest = new RestRequest(endpoint, method);

            if(method.Equals(Method.POST) || method.Equals(Method.PUT))
            {
                restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            } else
            {
                restRequest.AddParameter("application/json", ParameterType.RequestBody);
            }
            restRequest.AddHeader("Authorization", Token.BasicToken());

            IRestResponse restResponse = restClient.Execute(restRequest);

            return JsonConvert.DeserializeObject<T>(restResponse.Content);
        }
    }
}
