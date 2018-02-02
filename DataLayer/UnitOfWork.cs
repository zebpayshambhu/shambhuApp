using EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        private DbEntities _dbContext;
        private bool _disposed = false;
        private DbHelper<tblAccount> _tblAccount;
        private DbHelper<tblAccount_Banks> _tblAccount_Banks;

        public UnitOfWork()
        {
            _dbContext = new DbEntities();
        }

        public DbHelper<tblAccount> AccountRepository
        {
            get
            {
                if (this._tblAccount == null)
                    this._tblAccount = new DbHelper<tblAccount>(_dbContext);
                return _tblAccount;
            }
        }

        public DbHelper<tblAccount_Banks> AccountBankRepository
        {
            get
            {
                if (this._tblAccount_Banks == null)
                    this._tblAccount_Banks = new DbHelper<tblAccount_Banks>(_dbContext);
                return _tblAccount_Banks;
            }
        }


        /// <summary>
        /// Save method to save context object
        /// </summary>
        public void Save()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var outputLines = new List<string>();
                foreach (var eve in ex.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);
                throw ex;
            }
        }

        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
