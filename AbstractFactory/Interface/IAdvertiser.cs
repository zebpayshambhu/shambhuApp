using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Interface
{
    public interface IAdvertiser
    {
        void Advertise();
    }

    public class RedAdvertiser : IAdvertiser
    {
        public void Advertise()
        {
            Console.WriteLine("Advertised by Red Advertiser");
        }
    }

    public class BlueAdvertiser : IAdvertiser
    {
        public void Advertise()
        {
            Console.WriteLine("Advertised by Blue Advertiser");
        }
    }
}
