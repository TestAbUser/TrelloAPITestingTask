using RestSharp;

namespace TestingTrello
{
    /// <summary>
    /// Builder class. Is used for creating requests.
    /// </summary>
    public class ApiRequestBuilder
    {
        private readonly TrelloServiceObj trelloServiceObj;

        public ApiRequestBuilder()
        {
            trelloServiceObj = new TrelloServiceObj()
            {
                Request = new RestRequest()
            };
        }

        /// <summary>
        /// Assigns a value to a board property.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public ApiRequestBuilder SetBoardProperty(string property, string value)
        {
            trelloServiceObj.Request.AddParameter(property, value);
            return this;
        }

        /// <summary>
        /// Allows to select the request method.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public ApiRequestBuilder SetMethod(Method method)
        {
            trelloServiceObj.Request.Method = method;
            return this;
        }

        /// <summary>
        /// Acquires server response to a request.
        /// </summary>
        /// <param name="requestBuilder"></param>
        public static implicit operator TrelloServiceObj(ApiRequestBuilder requestBuilder)
        {
            requestBuilder.trelloServiceObj.Response = requestBuilder.trelloServiceObj.SendRequest();
            return requestBuilder.trelloServiceObj;
        }
    }
}
