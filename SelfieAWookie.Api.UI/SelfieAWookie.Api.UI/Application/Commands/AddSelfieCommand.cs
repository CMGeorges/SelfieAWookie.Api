using MediatR;
using SelfieAWookie.Api.UI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Application.Commands
{
    public class AddSelfieCommand:IRequest<SelfieDto>
    {

        #region Properties

        public SelfieDto Item { get; set; }
        #endregion
    }
}
