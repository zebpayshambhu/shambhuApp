using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Interface
{
    public interface IDistributor
    {
        void ShipBook();
    }

    public class EastCoastDistributor : IDistributor
    {
        public void ShipBook()
        {
            Console.WriteLine("East Coast Distributor");
        }
    }

    public class WestCoastDistributor : IDistributor
    {
        public void ShipBook()
        {
            Console.WriteLine("West Coast Distributor");
        }
    }
}
