using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SignUp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignUp.Infraestructure.Events
{
    public class EnrollmentsEventService : IEnrollmentsEventService
    {
        const string ServiceBusConnectionString = "Endpoint=sb://sigupeventbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=KaLMmG2D0Af3Sbnp8x69bOwKiO7dJWA2edjngmb2X9w=";
        const string QueueName = "signupqueue";
        static IQueueClient queueClient;

        public async Task EnrollASync(Enrollment enrollment)
        {
            try
            {
                queueClient = new QueueClient(ServiceBusConnectionString, QueueName);
                var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(enrollment)));
                // Send the message to the queue.
                await queueClient.SendAsync(message);
            }
            catch(Exception ex)
            {
                //Logger.LogError(ex, "An error occurred sending the sigup petition");
            }

        }


    }
}
