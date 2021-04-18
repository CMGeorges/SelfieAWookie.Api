using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Domain
{
   public interface ISelfieRepository : IRepository
    {
        ICollection<Selfie> GetAll();
    }
}
