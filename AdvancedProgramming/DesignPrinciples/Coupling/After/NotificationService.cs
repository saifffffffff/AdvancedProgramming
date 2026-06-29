using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.Coupling.After;

interface INotificationMode
{
    void Send();
}

class EmailService : INotificationMode
{
    public void Send()
    {
        Console.WriteLine("email sent");
    }
}

class SmsService : INotificationMode
{
    public void Send()
    {
        Console.WriteLine("sms sent");
    }
}

enum NotificationMode { Email, SMS}

class NotificationModeFactory
{
    public static INotificationMode Create(NotificationMode mode)
    {
        switch(mode)
        {
            case NotificationMode.Email:
                return new EmailService();

            case NotificationMode.SMS:
                return new SmsService();

            default:
                throw new ArgumentException();
        }
    }
}

class NotificationService
{
    private readonly INotificationMode _notificationMode;


    public NotificationService(INotificationMode notification)
    {
        _notificationMode = notification;
    }

    public void Notify()
    {
        _notificationMode.Send();
    }
}

