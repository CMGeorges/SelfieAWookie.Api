using MediatR;
using SelfieAWookie.Api.UI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Application.Queries
{
    /// <summary>
    /// Query to slect all selfies(with dto class)
    /// </summary>
    public class SelectAllSelfiesQuery:IRequest<List<SelfieResumeDto>>
    {
        #region Properties
        public int WookieId { get; set; }
        #endregion
    }
}
