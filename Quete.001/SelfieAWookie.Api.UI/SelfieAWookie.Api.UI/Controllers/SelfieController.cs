using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SelfieAWookie.Core.Selfies.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SelfieController : ControllerBase
    {
        #region Public methods

        [HttpGet]
       public IEnumerable<Selfie> TestApi()
        {

            return Enumerable.Range(1, 10).Select(Item => new Selfie() { Id = Item });
        }
        #endregion
    }
}
