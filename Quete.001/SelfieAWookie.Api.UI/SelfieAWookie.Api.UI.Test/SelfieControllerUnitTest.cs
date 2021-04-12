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
            var controler = new SelfieController();

            //ACT
            var result = controler.TestApi();


            //ASSERT
            Assert.NotNull(result);
            Assert.True(result.GetEnumerator().MoveNext());


        }
        #endregion

    }
}
