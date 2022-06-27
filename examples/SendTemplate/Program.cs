using System;
using System.Collections.Generic;
using SendTemplate;
using SparkPost;


var fromAddr = "from-csharp@yourdomain.com";
var toAddr = "to@you.com";
var apikey = "YOUR_API_KEY";

var trans = new Transmission();

var to = new Recipient
{
    Address = new Address
    {
        Email = toAddr
    },
    SubstitutionData = new Dictionary<string, object>
    {
        {"firstName", "Jane"}
    }
};

trans.Recipients.Add(to);
trans.SubstitutionData["title"] = "Dr";
trans.SubstitutionData["firstName"] = "Rick";
trans.SubstitutionData["lastName"] = "Sanchez";
trans.SubstitutionData["orders"] = new List<Order>
{
    new (101, "Tomatoes", 5),
    new(271, "Entropy", 314)
};

trans.Content.From.Email = fromAddr;
trans.Content.TemplateId = "orderSummary";

Console.Write("Sending mail...");

var client = new Client(apikey);

var response = await client.Transmissions.Send(trans);

Console.WriteLine("done");
