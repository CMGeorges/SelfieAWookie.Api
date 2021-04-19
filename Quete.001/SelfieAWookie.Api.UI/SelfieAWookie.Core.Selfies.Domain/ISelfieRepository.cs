using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Domain
{
    /// <summary>
    /// Repository to manage selfies
    /// </summary>
   public interface ISelfieRepository : IRepository
    {


        /// <summary>
        /// Get all selfies
        /// </summary>
        /// <returns></returns>
        ICollection<Selfie> GetAll(int wookieId);

        /// <summary>
        /// Additon of one selfie in DataBase
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Selfie AddOne(Selfie item);
    }
}
