using System;
using System.Collections.Generic;
using System.Text;
namespace AdvancedProgramming.DesignPrinciples.SOLID;


class ISP
{
    class Example_1_Before
    {
        interface IPrinter
        {
            void Print();
            void Fax();
            void Scan();
        }



        class BasicPrinter : IPrinter
        {
            public void Print()
            {
                Console.WriteLine("Printing .... ");
            }

            public void Fax()
            {
                throw new NotImplementedException();
            }

            public void Scan()
            {
                throw new NotImplementedException();
            }
        }

        class AdvancedPrinter : IPrinter
        {
            public void Fax()
            {
                Console.WriteLine("Faxing ... ");
            }

            public void Print()
            {
                Console.WriteLine("Printing...");
            }

            public void Scan()
            {
                Console.WriteLine("Scanning ... ");
            }
        }

    }
    
    class Example_1_After
    {
        interface IPrinter
        {
            void Print();
        } 
        interface IFax
        {
            void Fax();
        }
        interface IScan
        {
            void Scan();
        }



        class BasicPrinter : IPrinter
        {
            public void Print()
            {
                Console.WriteLine("Printing");
            }
        }

        class AdvancedPrinter : IPrinter, IFax, IScan
        {
            public void Fax()
            {
                Console.WriteLine("Faxing...");
            }

            public void Print()
            {
                Console.WriteLine("Printing...");
            }

            public void Scan()
            {
                Console.WriteLine("Scanning");
            }
        }
    }

    
    class Example_2_Before
    {
        interface IPayment
        {
            void PayWithCreditCard();
            void PayWithPayPal();
            void PayWithBitcoin();
        }


        public class CreditCardPayment : IPayment
        {
            public void PayWithBitcoin()
            {
                throw new NotImplementedException();
            }

            public void PayWithCreditCard()
            {
                Console.WriteLine("Payment with credit card");
            }

            public void PayWithPayPal()
            {
                throw new NotImplementedException();
            }
        }
    }

    class Example_2_After
    {
        interface ICreditCardPayment
        {
            void PayWithCreditCard();
        }
        
        interface IPaypalPayment
        {
            void PayWithPayPal();
        }
        
        interface IBitcoinPayment
        {
            void PayWithBitcoin();
        }

        public class CreditCardPayment : ICreditCardPayment
        {

            public void PayWithCreditCard()
            {
                Console.WriteLine("Credit card");
            }
        }
    }

    class Example_3_Before
    {
        interface IDevice
        {
            void MakeCall();
            void TakePhoto();
            void SendEmail();
            void UseGPS();
        }

        class Smartphone : IDevice
        {
            public void MakeCall()
            {
                Console.WriteLine("calling");
            }

            public void SendEmail()
            {
                Console.WriteLine("Send Email");
            }

            public void TakePhoto()
            {
                Console.WriteLine("Take Photo");
            }

            public void UseGPS()
            {
                Console.WriteLine("Using GPS");
            }
        }

        class Computer : IDevice
        {
            public void MakeCall()
            {
                throw new NotImplementedException();
            }

            public void SendEmail()
            {
                Console.WriteLine("Sending email..");
            }

            public void TakePhoto()
            {
                throw new NotImplementedException();
            }

            public void UseGPS()
            {
                throw new NotImplementedException();
            }

        }
    }

    class Example_3_After
    {
        
        interface ICall
        {
            void MakeCall();

        }
        interface IPhoto
        {
            void TakePhoto();

        }
        interface IEmail
        {

            void SendEmail();
        }
        interface IGPS
        {
            void UseGPS();

        }

        class Smartphone : ICall, IPhoto , IEmail , IGPS
        {
            public void MakeCall()
            {
                Console.WriteLine("calling");
            }

            public void SendEmail()
            {
                Console.WriteLine("Send Email");
            }

            public void TakePhoto()
            {
                Console.WriteLine("Take Photo");
            }

            public void UseGPS()
            {
                Console.WriteLine("Using GPS");
            }
        }

        class Computer : IEmail
        {

            public void SendEmail()
            {
                Console.WriteLine("Sending email..");
            }


        }
    }
}
