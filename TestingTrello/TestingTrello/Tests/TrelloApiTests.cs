using NUnit.Framework;
using RestSharp;
using TestingTrello.Constants;

namespace TestingTrello.Tests
{
    [TestFixture]
    public class TrelloApiTests
    {
        /// <summary>
        /// Creates a board and gets its ID.
        /// </summary>
        [SetUp]
        public void PrepareForTesting()
        {
            BoardParameterValue.Id = TrelloServiceObj.GetBoard(TrelloServiceObj.CreateBoard()).Id;
        }

        /// <summary>
        /// Checks that the board has been created.
        /// </summary>
        [Test]
        public void TestCreatingBoard()
        {
            TrelloServiceObj trelloServiceObj = new TrelloServiceObj();
            trelloServiceObj.response = trelloServiceObj.requestBuilder.SetMethod(Method.GET);
            Assert.That(trelloServiceObj.response.Content.Contains(BoardParameterValue.Id));
        }

        /// <summary>
        /// Checks that the board has a new name.
        /// </summary>
        [Test]
        public void TestChangingBoardName()
        {
            TrelloServiceObj trelloServiceObj = new TrelloServiceObj();
            trelloServiceObj.response = trelloServiceObj.requestBuilder
                                                        .SetBoardParameter(BoardParameterName.Name, BoardParameterValue.NewName)
                                                        .SetMethod(Method.PUT);
            Assert.That(trelloServiceObj.response.Content.Contains(BoardParameterValue.NewName));
        }

        /// <summary>
        /// Checks that the board has a new description.
        /// </summary>
        [Test]
        public void TestChangingBoardDescription()
        {
            TrelloServiceObj trelloServiceObj = new TrelloServiceObj();
            trelloServiceObj.response = trelloServiceObj.requestBuilder
                                                               .SetBoardParameter(BoardParameterName.Description, BoardParameterName.NewDescription)
                                                               .SetMethod(Method.PUT);
            Assert.That(trelloServiceObj.response.Content.Contains(BoardParameterName.NewDescription));
        }

        /// <summary>
        /// Checks that the board has a new background.
        /// </summary>
        [Test]
        public void TestChangingBoardBackground()
        {
            TrelloServiceObj trelloServiceObj = new TrelloServiceObj();
            trelloServiceObj.response = trelloServiceObj.requestBuilder
                                                                .SetBoardParameter(BoardParameterName.Background, BoardParameterValue.BackgroundColor)
                                                                .SetMethod(Method.PUT);
            string background = TrelloServiceObj.GetBoard(trelloServiceObj.response).Prefs.BackgroundColor;
            Assert.IsTrue(background == BoardParameterValue.NewBackgroundColor);
        }

        /// <summary>
        /// Deletes the board.
        /// </summary>
        [Test]
        public void TestDeletingBoard()
        {
            IRestResponse response = TrelloServiceObj.DeleteBoard();
            Assert.IsFalse(response.Content.Contains(BoardParameterValue.Id));
        }

        /// <summary>
        /// Deletes the board after tests have been completed.
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
            TrelloServiceObj.DeleteBoard();
        }
    }
}
