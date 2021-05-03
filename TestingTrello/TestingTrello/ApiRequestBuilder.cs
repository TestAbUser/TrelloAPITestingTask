using RestSharp;
using TestingTrello.Constants;

namespace TestingTrello
{
    /// <summary>
    /// Builder class. Is used for creating requests.
    /// </summary>
    public class ApiRequestBuilder
    {
        private const string POSTRequestResource = "1/boards/?";
        private const string GETRequestResource = "1/boards/{id}";
        private readonly RestRequest request;
        private RestResponse response;

        public ApiRequestBuilder()
        {
            request = new RestRequest();
            response = new RestResponse();
        }

        /// <summary>
        /// Assigns a value to a board property.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ApiRequestBuilder SetBoardParameter(string parameterName, string value)
        {
            request.AddParameter(parameterName, value);
            return this;
        }

        /// <summary>
        /// Allows to select the request method.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public ApiRequestBuilder SetMethod(Method method)
        {
            request.Method = method;
            SetRequestResource();
            return this;
        }

        /// <summary>
        /// Selects request resource depending on the method used in request.
        /// </summary>
        private void SetRequestResource()
        {
            if (request.Method == Method.POST)
            {
                request.Resource = POSTRequestResource;
            }
            else
            {
                request.AddUrlSegment(BoardParameterName.BoardId, BoardParameterValue.Id);
                request.Resource = GETRequestResource;
            }
        }

        /// <summary>
        /// Acquires server response to a request.
        /// </summary>
        /// <param name="requestBuilder"></param>
        public static implicit operator RestResponse(ApiRequestBuilder requestBuilder)
        {
            requestBuilder.response = TrelloServiceObj.SendRequest(requestBuilder.request);
            return requestBuilder.response;
        }
    }
}
