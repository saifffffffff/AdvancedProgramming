using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.Coupling.Before;

class EmailService
{
    public string email;
    public void SendEmail(string email )
    {
        Console.WriteLine("email sent");
    }
}

class SmsService
{
    public void Send()
    {
        Console.WriteLine("sms sent");
    }
}

// notification service's job is only to notify but it knows every thing about the services
// it is not testable
// as the services grow the notifications will grow , and the code becomes missy

// why  they are tightly coupled ? 
// the classes instantiated inside it : tight coupling
// the class uses method that only exists on concrente class ( SendEmail ) 
// the class directly accesses to field of the other class 
class NotificationService 
{
    private readonly EmailService _emailService;
    private readonly SmsService _smsService;

    public NotificationService(EmailService emailService , SmsService smsService )
    {
        _emailService = emailService;
        _smsService = smsService;
        _emailService.email = "S@gmail.com";
    }

    public void Notify()
    {
        _emailService.SendEmail("Hello ");
        _smsService.Send();
    }


}
