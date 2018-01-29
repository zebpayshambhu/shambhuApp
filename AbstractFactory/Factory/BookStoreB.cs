using AbstractFactory.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using static AbstractFactory.Utility;

namespace AbstractFactory.Factory
{
    public class BookStoreB : IBookStore
    {
        private CustomerLocation _location;

        public BookStoreB(CustomerLocation location)
        {
            this._location = location;
        }
        public IAdvertiser GetAdvertiser()
        {
            IAdvertiser advertiser = null;
            switch (_location)
            {
                case CustomerLocation.EastCoast:
                    advertiser = new BlueAdvertiser();
                    break;
                case CustomerLocation.WestCoast:
                    advertiser = new RedAdvertiser();
                    break;
            }
            return advertiser;
        }

        public IDistributor GetDistributor()
        {
            IDistributor distributor = null;
            switch (_location)
            {
                case CustomerLocation.EastCoast:
                    distributor = new EastCoastDistributor();
                    break;
                case CustomerLocation.WestCoast:
                    distributor = new WestCoastDistributor();
                    break;
            }
            return distributor;
        }
    }
}
