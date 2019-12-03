using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace OrderRead
{
    public interface IServicebusReceiver
    {
        void ReceiveMessage();
    }
    public class ServicebusReceiver : IServicebusReceiver
    {
        private string _connectionString;
        private string _topicName;
        private string _subscriptionName;
        private SubscriptionClient client;
        public ServicebusReceiver(string connectionString, string topicName, string subscriptionName)
        {
            _connectionString = connectionString;
            _topicName = topicName;
            _subscriptionName = subscriptionName;
            client = new SubscriptionClient(_connectionString, _topicName, _subscriptionName);
        }
        public void ReceiveMessage()
        {
            MessageHandlerOptions options = new MessageHandlerOptions(exceptionhandler) { 
                 AutoComplete = true,
                 MaxConcurrentCalls =1
            };

            client.RegisterMessageHandler(getmessages, options);
        }

        private Task getmessages(Message arg1, CancellationToken arg2)
        {
            string inmsg = System.Text.Encoding.ASCII.GetString(arg1.Body);
            dynamic obj = JsonConvert.DeserializeObject<dynamic>(inmsg);
            switch (obj.EventType.ToString()) {
                case "OrderCreated":
                    OrderCreated obj1 = JsonConvert.DeserializeObject<OrderCreated>(inmsg);
                     client.CompleteAsync(arg1.SystemProperties.LockToken).GetAwaiter().GetResult();
                    break;
            }
            return Task.CompletedTask;
        }

        private Task exceptionhandler(ExceptionReceivedEventArgs arg)
        {
            var aa = 1;
            return Task.CompletedTask;
        }
    }
}
