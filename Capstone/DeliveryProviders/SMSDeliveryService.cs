//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;


//namespace Capstone.DeliveryProviders
//{
//    public class SMSDeliveryService : IDeliveryService
//    {
//        public void Send(string recipient, string message)
//        {
//            #region Credentials
//            // Your Account SID from twilio.com/console
//            var accountSid = "";
//            // Your Auth Token from twilio.com/console
//            var authToken = "";
//            #endregion

//            TwilioClient.Init(accountSid, authToken);

//            MessageResource textMessage = MessageResource.Create(
//                to: new PhoneNumber(recipient), //4193439594
//                from: new PhoneNumber("2162424805"),
//                body: message);

//            Console.WriteLine("Message sent.");

//        }
//    }
//}