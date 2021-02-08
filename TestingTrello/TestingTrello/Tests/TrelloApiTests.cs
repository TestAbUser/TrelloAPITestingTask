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
            ParameterValue.boardId = TrelloServiceObj.GetBoardId(TrelloServiceObj.CreateBoard());
        }

        /// <summary>
        /// Checks that the board has been created.
        /// </summary>
        [Test]
        public void TestCreatingBoard()
        {
            TrelloServiceObj trelloServiceObj = TrelloServiceObj.CreateBuilder()
                                                                .SetMethod(Method.GET);
            Assert.That(trelloServiceObj.Response.Content.Contains(ParameterValue.boardId));
        }

        /// <summary>
        /// Checks that the board has a new name.
        /// </summary>
        [Test]
        public void TestChangingBoardName()
        {
            TrelloServiceObj trelloServiceObj = TrelloServiceObj.CreateBuilder()
                                                                .SetBoardProperty(ParameterName.Name, ParameterValue.NewName)
                                                                .SetMethod(Method.PUT);
            Assert.That(trelloServiceObj.Response.Content.Contains(ParameterValue.NewName));
        }

        /// <summary>
        /// Checks that the board has a new description.
        /// </summary>
        [Test]
        public void TestChangingBoardDescription()
        {
            TrelloServiceObj trelloServiceObj = TrelloServiceObj.CreateBuilder()
                                                                .SetBoardProperty(ParameterName.Description, ParameterName.NewDescription)
                                                                .SetMethod(Method.PUT);
            Assert.That(trelloServiceObj.Response.Content.Contains(ParameterName.NewDescription));
        }

        /// <summary>
        /// Checks that the board has a new background.
        /// </summary>
        [Test]
        public void TestChangingBoardBackground()
        {
            TrelloServiceObj trelloServiceObj = TrelloServiceObj.CreateBuilder()
                                                                .SetBoardProperty(ParameterName.Background, ParameterValue.NewBackgroundColor)
                                                                .SetMethod(Method.PUT);
            var background = TrelloServiceObj.GetBoardBackground(trelloServiceObj.Response);
            Assert.IsTrue(background == ParameterValue.NewBackgroundColor);
        }

        /// <summary>
        /// Deletes the board.
        /// </summary>
        [Test]
        public void TestDeletingBoard()
        {
            IRestResponse response = TrelloServiceObj.DeleteBoard();
            Assert.IsFalse(response.Content.Contains(ParameterValue.boardId));
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
