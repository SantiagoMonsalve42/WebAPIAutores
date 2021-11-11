using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPIAutores.Controllers.V1;
using WebAPIAutores.Tests.PruebasUnitarias.mocks;

namespace WebAPIAutores.Tests.PruebasUnitarias
{
    [TestClass]
    public class RootControllerTests
    {
        [TestMethod]
        public async Task SiEsAdmin_Obtener4Link()
        {
            //given
       
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Result = AuthorizationResult.Success();
            var rootController = new RootController(authorizationService);
            rootController.Url = new UrlHelperMock();

            //when
            var resultado = await rootController.Get();


            //then
            Assert.AreEqual(4,resultado.Value.Count());

        }
        [TestMethod]
        public async Task SiNoEsAdmin_Obtener2Link()
        {
            //given
            var authorizationService = new AuthorizationServiceMock();
            authorizationService.Result = AuthorizationResult.Failed();
            var rootController = new RootController(authorizationService);
            rootController.Url = new UrlHelperMock();

            //when
            var resultado = await rootController.Get();


            //then
            Assert.AreEqual(2, resultado.Value.Count());

        }

        [TestMethod]
        public async Task SiNoEsAdmin_Obtener2Link_UsandoMoq()
        {
            //given
            var mockAuth = new Mock<IAuthorizationService>();
            mockAuth.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<IEnumerable<IAuthorizationRequirement>>()
                )).Returns(
                Task.FromResult(AuthorizationResult.Failed()));
            mockAuth.Setup(x => x.AuthorizeAsync(
                It.IsAny<ClaimsPrincipal>(),
                It.IsAny<object>(),
                It.IsAny<string>()
                )).Returns(
                Task.FromResult(AuthorizationResult.Failed()));
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x =>
                x.Link(It.IsAny<string>(),
                It.IsAny<object>()))
                .Returns(String.Empty);
            var rootController = new RootController(mockAuth.Object);
            rootController.Url = mockUrlHelper.Object;

            //when
            var resultado = await rootController.Get();


            //then
            Assert.AreEqual(2, resultado.Value.Count());

        }
    }
}
