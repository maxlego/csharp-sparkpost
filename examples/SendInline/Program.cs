using System;
using System.Collections.Generic;
using SparkPost;

var apikey = "YOUR_API_KEY";
var fromAddr = "from-csharp@yourdomain.com";
var toAddr = "to@you.com";

var trans = new Transmission();

var to = new Recipient
{
    Address = new Address { Email = toAddr },
    SubstitutionData = new Dictionary<string, object> { { "firstName", "Jane" } }
};

trans.Recipients.Add(to);

trans.SubstitutionData["firstName"] = "Oh Ye Of Little Name";

trans.Content.From.Email = fromAddr;
trans.Content.Subject = "SparkPost online content example";
trans.Content.Text = "Greetings {{firstName or 'recipient'}}\nHello from C# land.";
trans.Content.Html = "<html><body><h2>Greetings {{firstName or 'recipient'}}</h2><p>Hello from C# land.</p></body></html>";

Console.Write("Sending mail...");

var client = new Client(apikey);

var response = await client.Transmissions.Send(trans);

Console.WriteLine("done");
