using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.Application.DTOs
{
    public class AuthentificateUserDto
    {

        #region Properties
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get;set; }
        #endregion
    }
}
