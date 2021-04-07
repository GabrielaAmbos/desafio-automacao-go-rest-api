using Go_Rest_API_Automation.utils.urls;

namespace Go_Rest_API_Automation.utils.providers
{
    public static class EndpointProvider
    {
        public static string Users()
        {
            return UrlProvider.BaseUrl() + "/users";
        }
    }
}
