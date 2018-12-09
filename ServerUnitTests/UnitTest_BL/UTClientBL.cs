using System;
using BlCRM;
using Common.Model;
using DalMain.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ServerUnitTests.UnitTest_BL
{
    [TestClass]
    public class UTClientBL
    {
        Mock<IDalCRMWrite> cRMManagerEdit;
        Mock<IDalCRMRead> cRMManagerRead;
        Client client;
        BlClient BlClient;
        [TestInitialize]
        public void UTClientController_ctorTestInitialize()
        {

            client = new Client().NewClient(12545658, "sanakkd", "kkfff", 1, "das", "1231", 1);
            cRMManagerEdit = new Mock<IDalCRMWrite>(MockBehavior.Strict);
            cRMManagerRead = new Mock<IDalCRMRead>(MockBehavior.Strict);
            BlClient = new BlClient(cRMManagerRead.Object, cRMManagerEdit.Object);

        }
        [TestMethod]
        public void TestMethod1()
        {

        }
        [TestMethod]
        public void AddClient_ReturnRequestStatusSucceeded_BL()
        {
            // ARRANGE
            cRMManagerEdit
                .Setup(s => s.AddClient(It.IsAny<Client>()))
                .Returns(() => { return RequestStatus.Succeeded; });

            cRMManagerRead
                .Setup(s => s.GetClientDeadOrAlive(It.IsAny<int>()))
                .Returns(() => { return null; });

            // ACT
            var response = BlClient.AddClient(client);

            // ASSERT
            var expected = RequestStatus.Succeeded;
            var result = response;

            Assert.AreEqual(expected, result, "expected " + expected + ", bet result is" + result);
            cRMManagerEdit.Verify(v => v.AddClient(It.IsAny<Client>()), Times.Once);

        }
    }
}
