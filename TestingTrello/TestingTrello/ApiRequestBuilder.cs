using RestSharp;

namespace TestingTrello
{
    /// <summary>
    /// Builder class. Is used for creating requests.
    /// </summary>
    public class ApiRequestBuilder
    {
        private string requestResource = "resource";
        private const string BoardId = "id";
        private readonly RestRequest request = new RestRequest();
        private static Method requestMethod = Method.GET;

        public ApiRequestBuilder SetResource(string resource)
        {
            requestResource = resource;
            return this;
        }

        public ApiRequestBuilder SetProperty(string property, string value)
        {
            request.AddParameter(property, value);
            return this;
        }

        public ApiRequestBuilder SetProperty(string value)
        {
            request.AddUrlSegment(BoardId, value);
            return this;
        }

        public ApiRequestBuilder SetMethod(Method method)
        {
            requestMethod = method;
            return this;
        }

        public TrelloServiceObj BuildRequest()
        {
            return new TrelloServiceObj(requestResource, request, requestMethod);
        }
    }
}
