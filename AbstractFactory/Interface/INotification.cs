using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactory.Interface
{
    public enum NotificationType
    {
        Email,
        SMS
    }
    public class NotificationFactory
    {
        public static INotification GetInstance(NotificationType notificationType)
        {
            switch (notificationType)
            {
                case NotificationType.Email:
                    return new EmailNotification();
                case NotificationType.SMS:
                    return new SMSNotification();
            }
            return null;
        }
    }

    public interface INotification
    {
        void Send();
    }

    public class EmailNotification : INotification
    {
        public void Send()
        {
        }
    }

    public class SMSNotification : INotification
    {
        public void Send()
        {
        }
    }


}
