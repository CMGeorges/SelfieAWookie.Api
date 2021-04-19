using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Application.DTOs
{
    public class SelfieDto
    {

        #region Properties
        public int Id { get; set; }
        public string ImagePath { get; internal set; }
        public string Title { get; internal set; }
        #endregion
    }
}
