using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SignUp.Domain.Models;

namespace SignUpBackGroundFunction
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([ServiceBusTrigger("signupqueue", Connection = "Endpoint=sb://sigupeventbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=KaLMmG2D0Af3Sbnp8x69bOwKiO7dJWA2edjngmb2X9w=")]string myQueueItem, ILogger log)
        {
            Enrollment enrollment = JsonConvert.DeserializeObject<Enrollment>(myQueueItem);



            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
