// Create Apache Pulsar client
using System.Text;
using DotPulsar;
using DotPulsar.Extensions;
using EmailService.db;
using Microsoft.EntityFrameworkCore;

var client = PulsarClient.Builder().Build();

// Create Apache Pulsar consumer
var consumer = client.NewConsumer().SubscriptionName("emailService").Topic("persistent://public/default/emails").Create();

// Listen on the 'emails' topic
await foreach (var item in consumer.Messages())
{
    var email = Encoding.ASCII.GetString(item.Data);
    using (var db = new EmailSvcDbContext())
    {
        var pm = await db.PropertyManagers.FirstOrDefaultAsync(pm => pm.Email == email);
        if (pm != null)
        {
            Console.WriteLine($"Sending email to: {pm.Email}");
            await consumer.Acknowledge(item);
        } else {
            Console.WriteLine($"Could not find a property manager with email: {email}");
        }
    }
}