using System.Linq;
using System.Collections.Generic;
using RestSharp.Serialization.Json;
using RestSharp;
using TestingTrello.Constants;
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
        private const string TokenValue = "bda7d73fa7b7369ed2a5ecbd71714293a0251627a715190487c5997daa5fd60f";
        private const string Url = "https://api.trello.com";
        public static RestClient client = new RestClient(Url);
        internal ApiRequestBuilder requestBuilder;
        internal static Board board;
        internal RestResponse response;

        public TrelloServiceObj()
        {
            client.Authenticator = new SimpleAuthenticator(ApiKey, KeyValue, Token, TokenValue);
            requestBuilder = new ApiRequestBuilder();
        }

        /// <summary>
        /// Sends the request to the server.
        /// </summary>
        /// <returns></returns>
        public static RestResponse SendRequest(RestRequest request)
        {
            RestResponse response = (RestResponse)client.Execute(request);
            Logger.SaveResultsToLog(response.Content);
            return response;
        }

        /// <summary>
        /// Deserializes the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static Board GetBoard(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            board = deserializer.Deserialize<Board>(response);
            return board;
        }

        internal static RestResponse CreateBoard()
        {
            TrelloServiceObj trelloServiceObj = new TrelloServiceObj();
            trelloServiceObj.response = trelloServiceObj.requestBuilder
                                   .SetBoardParameter(BoardParameterName.Name, BoardParameterValue.TestBoard)
                                   .SetMethod(Method.POST);
            return trelloServiceObj.response;
        }

        internal static RestResponse DeleteBoard()
        {
            TrelloServiceObj trelloServiceObj = new TrelloServiceObj();
            trelloServiceObj.response = trelloServiceObj.requestBuilder.SetMethod(Method.DELETE);
            return trelloServiceObj.response;
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
