using System.IO;
using System.Linq;
using System.Collections.Generic;
using RestSharp.Serialization.Json;
using RestSharp;

using RestSharp.Authenticators;

namespace TestingTrello
{
    /// <summary>
    /// ServiceObject class.
    /// </summary>
    public class TrelloServiceObj
    {
        private const string ApiKey = "key";
        private const string KeyValue = "f524090fa5d2f73d157f216bef8357af";
        private const string Token = "token";
        private const string TokenValue = "bf6f908bebc021cc4ab793d26bfc4d045139f0411456fb39f903f41c6e2356cb";
        private const string Url = "https://api.trello.com";
        private readonly string resource;
        private readonly Method requestMethod;
        private readonly RestRequest request;

        public TrelloServiceObj(string resource, RestRequest request, Method requestMethod)
        {
            this.resource = resource;
            this.request = request;
            this.requestMethod = requestMethod;
        }

        public static ApiRequestBuilder CreateBuilder()
        {
            return new ApiRequestBuilder();
        }

        /// <summary>
        /// Sends a completed request to server.
        /// </summary>
        /// <returns>IRestResponse instance.</returns>
        public IRestResponse SendRequest()
        {
            RestClient client = new RestClient(Url)
            {
                Authenticator = new SimpleAuthenticator(ApiKey, KeyValue, Token, TokenValue)
            };
            request.Resource = resource;
            request.Method = requestMethod;
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// In development.
        /// </summary>
        /// <returns></returns>
        public IRestResponse SendGetRequest()
        {
            RestClient client = new RestClient(Url)
            {
                Authenticator = new SimpleAuthenticator(ApiKey, KeyValue, Token, TokenValue)
            };
            request.Method = requestMethod;
            request.Resource = resource;
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// In development.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static List<string> GetStringResult(IRestResponse response)
        {
            return response.Content.Split(',').ToList();
        }

        /// <summary>
        /// In development.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static List<Board> GetAnswers(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            List<Board> answers = deserializer.Deserialize<List<Board>>(response);
            return answers;
        }
    }
}
