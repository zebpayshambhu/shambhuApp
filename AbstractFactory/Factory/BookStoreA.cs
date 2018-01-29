using AbstractFactory.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using static AbstractFactory.Utility;

namespace AbstractFactory.Factory
{
    public class BookStoreA : IBookStore
    {
        private CustomerLocation _location;

        public BookStoreA(CustomerLocation location)
        {
            this._location = location;
        }
        public IAdvertiser GetAdvertiser()
        {
            IAdvertiser advertiser = null;
            switch (_location)
            {
                case CustomerLocation.EastCoast:
                    advertiser = new RedAdvertiser();
                    break;
                case CustomerLocation.WestCoast:
                    advertiser = new BlueAdvertiser();
                    break;
            }
            return advertiser;
        }

        public IDistributor GetDistributor()
        {
            IDistributor distributor = null;
            switch(_location)
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