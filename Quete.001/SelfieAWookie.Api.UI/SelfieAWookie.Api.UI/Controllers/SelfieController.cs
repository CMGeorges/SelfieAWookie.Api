using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Selfies.Domain.Models;
using SelfieAWookie.Core.Selfies.Infrastructures.Data;
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

        #region Fields

        private readonly SelfiesContext _context;
        #endregion


        #region Ctor

        public SelfieController(SelfiesContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        [HttpGet]
       public IActionResult TestApi()
        {

            //return Enumerable.Range(1, 10).Select(Item => new Selfie() { Id = Item });

            //var query = from selfie in this._context.Selfies
            //            join wookie in this._context.Wookies 
            //            on selfie.WookieId equals wookie.Id
            //            select wookie;

            var model = this._context.Selfies.Include(item => item.Wookie).ToList();


            return this.Ok(model);
        }
        #endregion
    }
}
