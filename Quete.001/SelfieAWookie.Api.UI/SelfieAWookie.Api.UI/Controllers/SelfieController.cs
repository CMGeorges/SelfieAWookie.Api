using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Domain.Models;
using SelfieAWookie.Core.Selfies.Infrastructures.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
       public IActionResult GetAll([FromQuery] int wookieId = 0)
        {

            //return Enumerable.Range(1, 10).Select(Item => new Selfie() { Id = Item });

            //var query = from selfie in this._context.Selfies
            //            join wookie in this._context.Wookies 
            //            on selfie.WookieId equals wookie.Id
            //            select wookie;

            var selfiesList = this._repository.GetAll(wookieId);
            var model = selfiesList.Select(item => new SelfieResumeDto(){ Title = item.Title, WookieId = item.Wookie.Id, NbSelfiesFromWookie = (item.Wookie?.Selfies?.Count).GetValueOrDefault(0) }).ToList();


            return this.Ok(model);
        }

        //[Route("photos")]
        //[HttpPost]
        //public async Task<IActionResult> AddPictureAsync(IFormFile picture)
        //{
        //    using var stream = new StreamReader(this.Request.Body);

        //    var content = await stream.ReadToEndAsync();


        //    return this.Ok();
        //}

        [Route("photos")]
        [HttpPost]
        public IActionResult AddPictureAsync(IFormFile picture)
        {
            


            return this.Ok();
        }





        [HttpPost]
        public IActionResult AddOne(SelfieDto dto)
        {
            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                ImagePath = dto.ImagePath,
                Title = dto.Title
            });

            this._repository.UnitOfWork.SaveChanges();

            if (addSelfie != null)
            {
                dto.Id = addSelfie.Id;

                return this.Ok(dto);
            }

            return this.BadRequest();
           
        }

        #endregion
    }
}
