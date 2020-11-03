using System;
using System.Collections.Generic;
using System.Text;
using Amazon.SQS;
using Amazon.SQS.Model;
using EventDataProject1.Handler;
using Newtonsoft.Json;


namespace EventDataProject1
{
    public class EventBusAmazonSQS : IEventBus
    {
        private readonly AmazonSQSClient sqsClient;

        public EventBusAmazonSQS()
        {
            var sqsConfig = new AmazonSQSConfig()
            {
                ServiceURL = "http://sqs.eu-west-2.amazonaws.com"
            };

            sqsClient = new AmazonSQSClient(sqsConfig);

        }

        public async void Publish<T>(T e) where T : EventRequest
        {
            var sendMessageRequest = new SendMessageRequest()
            {
                QueueUrl = "https://sqs.eu-west-2.amazonaws.com/969543936222/EventNotificationQueue.fifo",
                MessageBody = JsonConvert.SerializeObject(e),
                MessageGroupId = "Policy",
                MessageDeduplicationId = e.Policy + e.Event_Time
            };
            SendMessageResponse sendMessageResponse = await sqsClient.SendMessageAsync(sendMessageRequest);
                     
        }


    }


}

