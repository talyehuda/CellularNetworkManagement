using System;
using BlWebApiCRM.Controllers;
using Common.Model;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;
using DalMain.Interface;
using DalMain.Manager;
using BlCRM;
using System.Collections.Generic;

namespace ServerUnitTests.UnitTest_WebApi
{
    [TestClass]
    public class UTClientController
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

        //AddClient
        [TestMethod]
        public void AddClient_clientDead_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerEdit
                .Setup(s => s.AddClient(It.IsAny<Client>()))
                .Returns(() => { return RequestStatus.Succeeded; });

            cRMManagerRead
                .Setup(s => s.GetClientDeadOrAlive(It.IsAny<int>()))
                .Returns(() => { return null; });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.AddClient(client);

            // ASSERT
            var result = (response as NegotiatedContentResult<RequestStatus>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, RequestStatus.Succeeded);
        }
        [TestMethod]
        public void AddClient_clientNull_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.AddClient(null);

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }
        [TestMethod]
        public void AddClient_clientAlive_ReturnHttpStatusCodeOk_API()
        {
            // ARRANGE
            cRMManagerEdit
                .Setup(s => s.EditClient(It.IsAny<Client>()))
                .Returns(() => { return RequestStatus.Succeeded; });

            cRMManagerRead
                .Setup(s => s.GetClientDeadOrAlive(It.IsAny<int>()))
                .Returns(() => { return client; });

            cRMManagerEdit
    .Setup(s => s.ReturningCustomer(It.IsAny<int>()))
    .Returns(() => { return RequestStatus.Succeeded; });


            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.AddClient(client);

            // ASSERT
            var result = (response as NegotiatedContentResult<RequestStatus>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, RequestStatus.Succeeded);
        }
        [TestMethod]
        public void AddClient_ReturnException_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            var controller = new ClientController(BlClient);
            cRMManagerEdit
                .Setup(s => s.AddClient(It.IsAny<Client>()))
                .Returns(() => { throw new Exception(); });

            cRMManagerRead
                .Setup(s => s.GetClientDeadOrAlive(It.IsAny<int>()))
                .Returns(() => { return null; });

            // ACT
            var response = controller.AddClient(null);

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }

        //EditClient
        [TestMethod]
        public void EditClient_client_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerEdit
                .Setup(s => s.EditClient(It.IsAny<Client>()))
                .Returns(() => { return RequestStatus.Succeeded; });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.EditClient(client);

            // ASSERT
            var result = (response as NegotiatedContentResult<RequestStatus>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, RequestStatus.Succeeded);
        }
        [TestMethod]
        public void EditClient_clientNull_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.EditClient(null);

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }
        [TestMethod]
        public void EditClient_ReturnException_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            cRMManagerEdit
                .Setup(s => s.EditClient(It.IsAny<Client>()))
                .Returns(() => { throw new Exception(); });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.EditClient(client);

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }

        //DeleteClient
        [TestMethod]
        public void DeleteClient_3_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerEdit
                .Setup(s => s.RemoveLines(It.IsAny<int>()))
                .Returns(() => { return RequestStatus.Succeeded; });

            cRMManagerEdit
    .Setup(s => s.RemoveClient(It.IsAny<int>()))
    .Returns(() => { return RequestStatus.Succeeded; });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.RemoveClient(3);

            // ASSERT
            var result = (response as NegotiatedContentResult<RequestStatus>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, RequestStatus.Succeeded);
        }
        [TestMethod]
        public void DeleteClient_ReturnException_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            cRMManagerEdit
    .Setup(s => s.RemoveLines(It.IsAny<int>()))
    .Returns(() => {throw new Exception(); });

            cRMManagerEdit
    .Setup(s => s.RemoveClient(It.IsAny<int>()))
    .Returns(() => { return RequestStatus.Succeeded; });
            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.RemoveClient(3);

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }

        //GetListClientType
        [TestMethod]
        public void GetListClientType_ReturnException_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            cRMManagerRead
.Setup(s => s.GetListClientType())
.Returns(() => { throw new Exception(); });
            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetListClientType();

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }
        [TestMethod]
        public void GetListClientType_ReturnNull_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerRead
.Setup(s => s.GetListClientType())
.Returns(() => { return null; });
            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetListClientType();

            // ASSERT
            var result = (response as NegotiatedContentResult<List<ClientType>>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, null);
        }

        //GetListClientType
        [TestMethod]
        public void GetListClientsIdNumber_ReturnException_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            cRMManagerRead
.Setup(s => s.GetListClientsIdNumber())
.Returns(() => { throw new Exception(); });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetListClientsIdNumber();

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }
        [TestMethod]
        public void GetListClientsIdNumber_ReturnNull_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerRead
.Setup(s => s.GetListClientsIdNumber())
.Returns(() => { return null; });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetListClientsIdNumber();

            // ASSERT
            var result = (response as NegotiatedContentResult<List<int>>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, null);
        }

        //GetClientById
        [TestMethod]
        public void GetClientById_3_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerRead
    .Setup(s => s.GetClientById(It.IsAny<int>()))
    .Returns(() => { return client; });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetClientById(3);

            // ASSERT
            var result = (response as NegotiatedContentResult<Client>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, client);
        }
        [TestMethod]
        public void GetClientById_ReturnException_ReturnHttpStatusCodeInternalServerError_API()
        {
            // ARRANGE
            cRMManagerRead
    .Setup(s => s.GetClientById(It.IsAny<int>()))
    .Returns(() => { throw new Exception(); });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetClientById(3);

            // ASSERT
            var result = (response as NegotiatedContentResult<string>);

            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.MethodNotAllowed);
        }
        [TestMethod]
        public void GetClientById_ReturnNull_ReturnHttpStatusCodeOK_API()
        {
            // ARRANGE
            cRMManagerRead
    .Setup(s => s.GetClientById(It.IsAny<int>()))
    .Returns(() => { return null; });

            var controller = new ClientController(BlClient);

            // ACT
            var response = controller.GetClientById(3);

            // ASSERT
            var result = (response as NegotiatedContentResult<Client>);
            PlayAssertAreEqual(result.StatusCode, System.Net.HttpStatusCode.OK);
            PlayAssertAreEqual(result.Content, null);
        }


        public void PlayAssertAreEqual<T>(T result,T expectedRequest)
        {
            Assert.AreEqual(result, expectedRequest, "RequestStatus: " + "expected " + expectedRequest + ", bet result is" + result);
        }
    }
}
