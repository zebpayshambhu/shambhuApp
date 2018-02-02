using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BusinessLayer.Interface;
using DataLayer;
using EntityLayer;
using UtilityLayer;

namespace BusinessLayer
{
    public class AccountBL : IAccount
    {
        private readonly UnitOfWork _UnitOfWork;
        private readonly DbHelper<tblAccount> _repository;
        public AccountBL()
        {
            _UnitOfWork = new UnitOfWork();
            _repository = _UnitOfWork.AccountRepository;
        }

        public tblAccount GetDataByPK(long Id)
        {
            var result = _repository.GetFirst((x => x.Id == Id));
            return result;
        }

        public List<tblAccount> GetData(tblAccount filterModel, int currentPageIndex, out int Total)
        {
            IEnumerable<tblAccount> result = null;
            bool isAllPropEmpty = true;
            if (filterModel != null)
                isAllPropEmpty = filterModel.GetType().GetProperties().All(p => string.IsNullOrWhiteSpace(Convert.ToString(p.GetValue(filterModel))));

            if (isAllPropEmpty)
            {
                result = _repository.GetPagedList(null, _order => _order.Id, currentPageIndex, out Total);
            }
            else
            {
                //var predicate = PredicateBuilder.False<_Account>();
                //if (!string.IsNullOrEmpty(filterModel.FirstName))
                //    predicate = predicate.Or<_Account>(x => x.FirstName.Contains(filterModel.FirstName));
                //if (!string.IsNullOrEmpty(filterModel.LastName))
                //    predicate = predicate.Or<_Account>(x => x.LastName.Contains(filterModel.LastName));
                //predicate.Compile();
                result = _repository.GetPagedList(null, _order => _order.Id, currentPageIndex, out Total);
            }
            return result.ToList();
        }

        public List<tblAccount> GetData(EnumUtility.DBStatus? Status = null)
        {
            IEnumerable<tblAccount> result = null;
            if (Status.HasValue)
            {
                int recordStatus = (int)Status.Value;
                result = _repository.GetMany((x => x.Status == recordStatus));
            }
            else
                result = _repository.GetMany(null);
            return result.ToList();
        }

        public tblAccount InsertUpdate(tblAccount model, EnumUtility.DBAction action)
        {
            using (var scope = new TransactionScope())
            {
                if (action == EnumUtility.DBAction.Update)
                {
                    string[] NoUpdateProperty = new string[] { "CreateDate", "CreatedBy" };
                    model.ModifyDate = DateTime.Now;
                    _repository.UpdateEntity(model, NoUpdateProperty);
                }
                else
                {
                    model.CreateDate = DateTime.Now;
                    _repository.InsertEntity(model);
                }

                _UnitOfWork.Save();
                scope.Complete();
                return model;
            }
        }

        public bool UpdateStatus(long Id)
        {
            using (var scope = new TransactionScope())
            {
                tblAccount result = _repository.GetFirst(x => x.Id == Id);
                if (result != null)
                {
                    if (result.Status == (int)EnumUtility.DBStatus.Active)
                        result.Status = (int)EnumUtility.DBStatus.Inactive;
                    else
                        result.Status = (int)EnumUtility.DBStatus.Active;

                    _UnitOfWork.Save();
                    scope.Complete();
                }
                return true;
            }
        }
    }
}
