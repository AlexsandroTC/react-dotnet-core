using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react_dotnet_core.BackEnd.Services
{
    public static class ServicesBus
    {
        private static string _connetionString = "Endpoint=sb://estudodeservicesbus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=KxKb6lcEnW6evtmMaRoiAhsQXCHkpXfE0d4tRX/NeSU=";
        private static string _queueName = "nomes";

        private static List<string> _nomes = new List<string>();

        public static async Task PostMessage(string nome)
        {
            ServiceBusClient client = new ServiceBusClient(_connetionString);

            ServiceBusSender sender = client.CreateSender(_queueName);

            ServiceBusMessage message = new ServiceBusMessage(nome);

            await sender.SendMessageAsync(message);
        }

        public static async Task<List<string>> ReceaveMessageAsync()
        {
            ServiceBusClient client = new ServiceBusClient(_connetionString);

            ServiceBusReceiver receiver = client.CreateReceiver(_queueName);

            var temRegistro = true;
            while (temRegistro)
            {
                ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
                string messge = receivedMessage.Body.ToString();

                _nomes.Add(messge);

                temRegistro = !String.IsNullOrWhiteSpace(messge);
            }

            //ServiceBusProcessor processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
            //processor.ProcessMessageAsync += MessageHandler;
            //processor.ProcessErrorAsync += ErrorHandler;
            //await processor.StartProcessingAsync();
            //await processor.StopProcessingAsync();

            return _nomes;
        }

        // handle received messages
        static async Task MessageHandler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            _nomes.Add(body);

            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
