using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Interface
{
    public interface IBookStore
    {
        IDistributor GetDistributor();
        IAdvertiser GetAdvertiser();
    }
    
}
