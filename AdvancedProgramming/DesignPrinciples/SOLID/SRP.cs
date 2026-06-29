using System;
using System.Collections.Generic;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID;

class SRP
{

    // not following SRP
    class NotificationService
    {
        public enum NotificationType { Email, Sms, Fax }

        public void SendMessage(string to, string message, NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Email:
                    SendEmail(to, message);
                    break;

                case NotificationType.Fax:
                    SendFax(to, message);
                    break;

                case NotificationType.Sms:
                    SendSms(to, message);
                    break;
            }

        }

        private void SendSms(string to, string message)
        {
            throw new NotImplementedException();
        }

        private void SendFax(string to, string message)
        {
            throw new NotImplementedException();
        }

        private void SendEmail(string to, string message)
        {
            throw new NotImplementedException();
        }
    }

    // following Single responsibility principle
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


    // not following SRP

    class LoggingService
    {
        public enum LoggingType { ToFile, ToEventLog, ToDataBase }

        public void Log(string message, LoggingType type)
        {
            switch (type)
            {
                case LoggingType.ToFile:
                    LogToFile(message, type);
                    break;
                case LoggingType.ToEventLog:
                    LogToEventLog(message, type);
                    break;
                case LoggingType.ToDataBase:
                    LogToDatabase(message, type);
                    break;
            }
        }

        private void LogToFile(string message, LoggingType type)
        {
            throw new NotImplementedException();
        }

        private void LogToEventLog(string message, LoggingType type)
        {
            throw new NotImplementedException();
        }

        private void LogToDatabase(string message, LoggingType type)
        {
            throw new NotImplementedException();
        }
    }

    // following SRP

    class LoggingService_SRP
    {
        public enum LoggingType { ToFile, ToEventLog, ToDataBase }

        public void Log(string message, LoggingType type)
        {
            switch (type)
            {
                case LoggingType.ToFile:
                    FileLoggerService.Log(message);
                    break;
                case LoggingType.ToEventLog:
                    EventLoggerService.Log(message);
                    break;
                case LoggingType.ToDataBase:
                    DatabaseLoggerService.Log(message);
                    break;
            }
        }

        
    }

    class FileLoggerService
    {
        public static void Log(string message)
        {

        }
    }

    class DatabaseLoggerService
    {
        public static void Log(string message)
        {

        }
    }
    class EventLoggerService
    {
        public static void Log(string message)
        {

        }
    }

}

