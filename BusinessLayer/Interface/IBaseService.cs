using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UtilityLayer;

namespace BusinessLayer.Interface
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        TEntity GetDataByPK(long Id);
        List<TEntity> GetData(TEntity filterModel, int currentPageIndex, out int Total);
        List<TEntity> GetData(Nullable<EnumUtility.DBStatus> Status = null);
    }
}
