using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Api.UI.Controllers;
using System;
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
        public void ShouldReturnLisOfSelfies()
        {
            //ARRANGE
            var controler = new SelfieController(null);

            //ACT
            var result = controler.TestApi();


            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            OkObjectResult objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult.Value);


        }
        #endregion

    }
}
