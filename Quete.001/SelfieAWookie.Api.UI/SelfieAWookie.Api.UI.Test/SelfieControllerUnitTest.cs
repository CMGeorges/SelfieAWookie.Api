using Microsoft.AspNetCore.Mvc;
using Moq;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Api.UI.Controllers;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace SelfieAWookie.Api.UI.Test
{
    /// <summary>
    /// TDD
    /// </summary>
    public class SelfieControllerUnitTest
    {

        #region Public methods

        [Fact]
        public void ShouldAddOneSelfie()
        {
            //ARRANGE 
            Selfie selfie = new Selfie();
            var repositoryMock = new Mock<ISelfieRepository>();


            //ACT
            var controler = new SelfieController(repositoryMock.Object);
            var result = controler.AddOne(selfie);

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var addedSelfie = (result as OkObjectResult).Value as SelfieDto;
            Assert.NotNull(addedSelfie);
            Assert.True(addedSelfie.Id > 0);



        }


        [Fact]
        public void ShouldReturnLisOfSelfies()
        {
            //ARRANGE
            var expectedList = new List<Selfie>()
            {
                new Selfie(){ Wookie = new Wookie() },
                new Selfie(){Wookie = new Wookie()}
            };
            var repositoryMock = new Mock<ISelfieRepository>();

            repositoryMock.Setup(item => item.GetAll()).Returns(expectedList);

            var controler = new SelfieController(repositoryMock.Object);

            //ACT
            var result = controler.TestApi();


            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult objectResult = result as OkObjectResult;

            Assert.NotNull(objectResult.Value);
            Assert.IsType<List<SelfieResumeDto>>(objectResult.Value);

            List<SelfieResumeDto> list = objectResult.Value as List<SelfieResumeDto>;
            Assert.True(list.Count == expectedList.Count);
            


        }
        #endregion

    }
}
