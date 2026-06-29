using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID;

class OCP
{

    class NotificationService_SRP
    {
        public enum NotificationType { Email, Sms, Fax }

        public void SendMessage(string to, string message, NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Email:
                    EmailService.SendEmail(to, message);
                    break;

                case NotificationType.Fax:
                    FaxService.SendFax(to, message);
                    break;

                case NotificationType.Sms:
                    SmsService.SendSms(to, message);
                    break;
            }

        }



    }


    class EmailService
    {
        public static void SendEmail(string to, string message)
        {
            throw new NotImplementedException();
        }

    }
    class FaxService
    {
        public static void SendFax(string to, string message)
        {
            throw new NotImplementedException();
        }

    }
    class SmsService
    {

        public static void SendSms(string to, string message)
        {
            throw new NotImplementedException();
        }
    }

    // -------------------- following OCP --------------------
    interface INotification
    {
        void Send(string to, string message);
    }

    class NotificationService
    {
        INotification _notification;
        public NotificationService(INotification notification)
        {
            _notification = notification;
        }

        void SendNotification (string to , string message )
        {
            _notification.Send(to, message);
        }
    }


    class EmailServiceOCP : INotification
    {
        public void Send(string to, string message)
        {
            throw new NotImplementedException();
        }
    }


    class SmsServiceOCP : INotification
    {
        public void Send(string to, string message)
        {
            throw new NotImplementedException();
        }
    }

    class FaxServiceOCP : INotification
    {
        public void Send(string to , string message )
        {
            throw new NotImplementedException();
        }
    }




}
