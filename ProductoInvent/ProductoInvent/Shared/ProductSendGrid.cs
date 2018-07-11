using ProductoInvent.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProductoInvent
{
    public class SendEmailLogic
    {
        private const string apiKey = "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";

        public async Task SendMail(ProductCollectionModel collection)
        {
            //var proxy = new WebProxy()
            //{
            //    Address = new Uri($"http://gbips-i-ss50.int.dir.willis.com:8080"),
            //    BypassProxyOnLocal = false,
            //    UseDefaultCredentials = true,
            //    Credentials = new NetworkCredential("BMSri", "June2018&", "INT")
            //};

            //var client = new SendGridClient(proxy, apiKey);
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("Sridhar.bm@Willistowerswatson.com", "Products Admin Team"),
                Subject = "Product Inventory - New Product",
                PlainTextContent = "Congratulations!!!!!!!!!!!" + "You have succesfully added" + " " + collection.ProductName + ":)"
            };
            msg.AddTo(new EmailAddress(collection.CustomerEmail,"Customer"));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

    }
}




