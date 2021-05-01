using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Api.UI.ExtensionMethods;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [EnableCors(SecurityMethods.DEFAULT_POLICY)]
    public class SelfieController : ControllerBase
    {

        #region Fields

        private readonly ISelfieRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion


        #region Ctor

        public SelfieController(ISelfieRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> AddPictureAsync(IFormFile picture)
        {
            string filePath = Path.Combine(this._webHostEnvironment.ContentRootPath, @"images\selfies");

            if (!Directory.Exists(filePath)) 
            {
                Directory.CreateDirectory(filePath);
            }
            filePath = Path.Combine(filePath, picture.FileName);

            using var stream = new FileStream(filePath,FileMode.OpenOrCreate);
            await picture.CopyToAsync(stream);

            var itemFile = this._repository.AddOnePicture(filePath);

            this._repository.UnitOfWork.SaveChanges();

            return this.Ok(itemFile);
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
