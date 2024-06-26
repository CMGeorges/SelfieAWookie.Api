﻿using MediatR;
using SelfieAWookie.Api.UI.Application.DTOs;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Application.Commands
{
    public class AddSelfieHander : IRequestHandler<AddSelfieCommand, SelfieDto>
    {


        #region Fields


        private readonly ISelfieRepository _repository = null;
        #endregion

        #region Ctor

        public AddSelfieHander(ISelfieRepository repository)
        {
            _repository = repository;
        }
        #endregion


        public Task<SelfieDto> Handle(AddSelfieCommand request, CancellationToken cancellationToken)
        {
            SelfieDto result = null;

            Selfie addSelfie = this._repository.AddOne(new Selfie()
            {
                ImagePath = request.Item.ImagePath,
                Title = request.Item.Title
            });

            this._repository.UnitOfWork.SaveChanges();

            if (addSelfie != null)
            {
                request.Item.Id = addSelfie.Id;

                result = request.Item;
            }

            return Task.FromResult(result);
        }
    }
}
