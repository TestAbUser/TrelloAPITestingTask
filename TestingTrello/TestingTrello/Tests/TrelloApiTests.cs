using System.Net;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;

namespace TestingTrello.Tests
{
    [TestFixture]
    public class TrelloApiTests
    {
        private const string BoardName = "name";
        private const string BoardBackground = "prefs/background";
        private const string BackgroundColor = "red";
        private const string Name = "TestBoard";
        private const string NewName = "UpdatedBoard";
        private const string POSTRequestResource = "https://api.trello.com/1/boards";
        private const string GETRequestResource = "https://api.trello.com";
        private const string PUTRequestResource = "https://api.trello.com/1/boards/{id}";
        private string boardId;

        /// <summary>
        /// Creates a board and gets its ID.
        /// </summary>
        [OneTimeSetUp]
        public void PrepareForTest()
        {
            IRestResponse serverResponse = CreateBoard();
            boardId = GetBoardId(serverResponse);
        }

        /// <summary>
        /// Creates a board.
        /// </summary>
        [Test]
        public void CreateBoardTest()
        {
            IRestResponse result = CreateBoard();
            Logger.SaveResultsToLog(result.Content);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        ///Makes GET request and receives a board in response.
        /// </summary>
        [Test]
        public void GetBoardTest()
        {
            IRestResponse result = TrelloServiceObj.CreateBuilder()
                                                   .SetResource(GETRequestResource)
                                                   .SetProperty(boardId)
                                                   .SetMethod(Method.GET)
                                                   .BuildRequest()
                                                   .SendRequest();
            Logger.SaveResultsToLog(result.Content);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Changes the name of a board.
        /// </summary>
        [Test]
        public void ChangeBoardNameTest()
        {
            IRestResponse response = TrelloServiceObj.CreateBuilder()
                                                   .SetResource(PUTRequestResource)
                                                   .SetProperty(boardId)
                                                   .SetProperty(BoardName, NewName)
                                                   .SetMethod(Method.PUT)
                                                   .BuildRequest()
                                                   .SendRequest();
            Logger.SaveResultsToLog(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Changes the background of a board.
        /// </summary>
        [Test]
        public void ChangeBoardBackgroundTest()
        {
            IRestResponse response = TrelloServiceObj.CreateBuilder()
                                                   .SetResource(PUTRequestResource)
                                                   .SetProperty(boardId)
                                                   .SetProperty(BoardBackground, BackgroundColor)
                                                   .SetMethod(Method.PUT)
                                                   .BuildRequest()
                                                   .SendRequest();

            Logger.SaveResultsToLog(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Deletes a board.
        /// </summary>
        [Test]
        public void DeleteBoardTest()
        {
            IRestResponse response = DeleteBoard();
            Logger.SaveResultsToLog(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        /// <summary>
        /// Deletes a board after tests have been completed.
        /// </summary>
        [OneTimeTearDown]
        public void CleanUp()
        {
            DeleteBoard();
        }

        /// <summary>
        /// Creates aboard
        /// </summary>
        /// <returns>IRestResponse instance.</returns>
        private IRestResponse CreateBoard()
        {
            IRestResponse response = TrelloServiceObj.CreateBuilder()
                                                     .SetResource(POSTRequestResource)
                                                     .SetProperty(BoardName, Name)
                                                     .SetMethod(Method.POST)
                                                     .BuildRequest()
                                                     .SendRequest();
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response">Is used as a parameter to get the board id.</param>
        /// <returns>Board ID.</returns>
        private string GetBoardId(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            Board board = deserializer.Deserialize<Board>(response);
            return board.id;
        }

        /// <summary>
        /// Deletes a board.
        /// </summary>
        /// <returns>Request response.</returns>
        private IRestResponse DeleteBoard()
        {
            IRestResponse response = TrelloServiceObj.CreateBuilder()
                                                     .SetResource(PUTRequestResource)
                                                     .SetProperty(boardId)
                                                     .SetMethod(Method.DELETE)
                                                     .BuildRequest()
                                                     .SendRequest();
            return response;
        }
    }
}
