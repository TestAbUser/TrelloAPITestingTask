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
        private const string POSTRequestResource = "1/boards/?";
        private const string GETRequestResource = "1/boards/{id}";
        private const string ApiKey = "key";
        private const string KeyValue = "f524090fa5d2f73d157f216bef8357af";
        private const string Token = "token";
        private const string TokenValue = "bda7d73fa7b7369ed2a5ecbd71714293a0251627a715190487c5997daa5fd60f";
        private const string Url = "https://api.trello.com";
        public RestRequest Request { get; set; }
        public IRestResponse Response { get; set; }

        public static ApiRequestBuilder CreateBuilder()
        {
            return new ApiRequestBuilder();
        }

        /// <summary>
        /// Changes the request resource depending on the request method.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        private void SetBoardResource(string parameterName, string parameterValue)
        {
            if (Request.Method == Method.POST)
            {
                Request.Resource = POSTRequestResource;
            }
            else
            {
                Request.AddUrlSegment(parameterName, parameterValue);
                Request.Resource = GETRequestResource;
            }
        }

        /// <summary>
        /// Sends the request to the server.
        /// </summary>
        /// <returns></returns>
        public IRestResponse SendRequest() 
        {
            RestClient client = new RestClient(Url);
            client.Authenticator = new SimpleAuthenticator(ApiKey, KeyValue, Token, TokenValue);
            SetBoardResource(ParameterName.BoardId, ParameterValue.boardId);
            IRestResponse response = client.Execute(Request);
            Logger.SaveResultsToLog(response.Content);
            return response;
        }

        /// <summary>
        /// Extracts the value for board id from the response.
        /// </summary>
        /// <param name="response">Is used as a parameter to get the board id.</param>
        /// <returns>Board ID.</returns>
        internal static string GetBoardId(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            Board board = deserializer.Deserialize<Board>(response);
            return board.Id;
        }

        /// <summary>
        /// Extracts the value for board background from the response.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        internal static string GetBoardBackground(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            Board board = deserializer.Deserialize<Board>(response);
            return board.Prefs.Background;
        }

        internal static IRestResponse CreateBoard()
        {
            TrelloServiceObj trelloServiceObj = CreateBuilder().SetBoardProperty(ParameterName.Name, ParameterValue.TestBoard)
                                                               .SetMethod(Method.POST);
            return trelloServiceObj.Response;
        }

        internal static IRestResponse DeleteBoard()
        {
            TrelloServiceObj trelloServiceObj = CreateBuilder().SetMethod(Method.DELETE);
            return trelloServiceObj.Response;
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
