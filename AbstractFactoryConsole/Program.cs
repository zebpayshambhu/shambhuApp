using AbstractFactory.Factory;
using AbstractFactory.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AbstractFactory.Utility;

namespace AbstractFactoryConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookStore bookStoreA = new BookStoreA(CustomerLocation.EastCoast);
            ShipBook(bookStoreA);
            Advertise(bookStoreA);

            IBookStore bookStoreB = new BookStoreB(CustomerLocation.WestCoast);
            ShipBook(bookStoreB);
            Advertise(bookStoreB);

            Console.ReadKey();

            INotification notification = NotificationFactory.GetInstance(NotificationType.Email);
            notification.Send();
            INotification notification1 = NotificationFactory.GetInstance(NotificationType.SMS);
            notification1.Send();

            RechargeAbstract obj2 = new UtilityRecharge(new UtilityRechargeModel());
            obj2.InitiateRequest();

            RechargeAbstract obj3 = new WalletRecharge(new WalletRechargeModel());
            obj3.InitiateRequest();
        }

        private static void ShipBook(IBookStore bookStore)
        {
            IDistributor obj = bookStore.GetDistributor();
            obj.ShipBook();
        }

        private static void Advertise(IBookStore bookStore)
        {
            IAdvertiser obj = bookStore.GetAdvertiser();
            obj.Advertise();
        }
    }
}
