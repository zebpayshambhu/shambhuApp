using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityLayer;

namespace BusinessLayer.Interface
{
    public interface IAccount : IBaseService<tblAccount>
    {
        tblAccount InsertUpdate(tblAccount model, EnumUtility.DBAction action);
        bool UpdateStatus(long Id);
    }
}
