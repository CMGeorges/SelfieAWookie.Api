using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SelfieAWookie.Api.UI.Application.Commands;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Api.UI.Application.Queries;
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
        private readonly IMediator _mediator;
        private readonly ISelfieRepository _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<SelfieController> _logger;
        #endregion


        #region Ctor

        public SelfieController(IMediator mediator,ISelfieRepository repository, IWebHostEnvironment webHostEnvironment, ILogger<SelfieController> logger)
        {
            this._mediator = mediator;
            this._repository = repository;
            this._webHostEnvironment = webHostEnvironment;
            this._logger = logger;
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


            var model = this._mediator.Send(new SelectAllSelfiesQuery() { WookieId = wookieId });

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
        public async Task<IActionResult> AddOneAsync(SelfieDto dto)
        {
            IActionResult result = this.BadRequest();

         

            var resultItem =  await this._mediator.Send(new AddSelfieCommand() { Item = dto });

            if (resultItem != null )
            {
                result = Ok(resultItem);
            }

            return result;
           
        }

        #endregion
    }
}
