using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Core.Selfies.Domain;
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

        private readonly ISelfieRepository _repository;
        #endregion


        #region Ctor

        public SelfieController(ISelfieRepository repository)
        {
            this._repository = repository;
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

            var selfiesList = this._repository.GetAll();
            var model = selfiesList.Select(item => new SelfieResumeDto(){ Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();


            return this.Ok(model);
        }


        [HttpPost]
        public IActionResult AddOne(SelfieDto selfie)
        {


            return this.Ok(new SelfieDto() 
            {
                Id = 1
            });
        }

        #endregion
    }
}
