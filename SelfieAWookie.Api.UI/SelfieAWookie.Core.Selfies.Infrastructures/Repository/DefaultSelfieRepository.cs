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
        public ICollection<Selfie> GetAll(int wookieId)
        {

            var query = this._context.Selfies.Include(item => item.Wookie).AsQueryable();

            if (wookieId > 0)
            {
                query = query.Where(item => item.WookieId == wookieId);
            }

            return query.ToList();
        }

        public Selfie AddOne(Selfie item)
        {
            return this._context.Selfies.Add(item).Entity;
        }

        public Picture AddOnePicture(string url)
        {
            return this._context.Pictures.Add(new Picture()
            {
                Url = url
            }).Entity;
        }
        #endregion


        #region Properties
        public IUnitOfWork UnitOfWork => this._context;

        #endregion

    }
}
