using Microsoft.EntityFrameworkCore;
using SelfieAWookie.Core.Framework;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Domain.Models;
using SelfieAWookie.Core.Selfies.Infrastructures.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfieAWookie.Core.Selfies.Infrastructures.Repository
{
    public class DefaultSelfieRepository : ISelfieRepository
    {

        #region Fields
        private readonly SelfiesContext _context;

        #endregion

        #region Ctor
        public DefaultSelfieRepository(SelfiesContext context)
        {
            this._context = context;
        }

        #endregion

        #region Public methods
        public ICollection<Selfie> GetAll()
        {
            return this._context.Selfies.Include(item => item.Wookie).ToList();
        }
        #endregion


        #region Properties
        public IUnitOfWork UnitOfWork => this._context;

        #endregion

    }
}
