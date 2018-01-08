using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Amazon.SQS.Model;
using Amazon.SQS;

namespace AwsConsoleApp1
{
    public static class Messages
    {
        public static MessageMethod MessageMethod
        {
            get
            {
                var messagemethod = new MessageMethod();
                return messagemethod;
            }
        }
    }

    public class MessageMethod
    {        
        public void SendMessages(AmazonSQSClient client, string url, string sendMessage, int delaySecond)
        {

            var message = new SendMessageRequest()
            {
                MessageBody = sendMessage,
                DelaySeconds = delaySecond,
                QueueUrl = url,
                MessageAttributes = new Dictionary<string, MessageAttributeValue>
                {
                     { "DayAttribute", new MessageAttributeValue { DataType = "String", StringValue = "Monday" } },
                     { "TempretureAttribute", new MessageAttributeValue { DataType = "String", StringValue = "Hot 33" } },
                     { "WeatherAttribute", new MessageAttributeValue { DataType = "String", StringValue = "Cloudy with channce of Thunderstorm" } },
                }                
            };
            
            var response = client.SendMessage(message);            
            Console.WriteLine("---Sent Message attributes---- "+ response.MD5OfMessageAttributes);
            Console.WriteLine("---Sent Message Body---- " + response.MD5OfMessageBody);
            Console.WriteLine("---Sent Message Id---- " + response.MessageId);                        
        }

        public ReceiveMessageResponse ReceiveMessages(AmazonSQSClient client, string Url, int visibilityTime, int waittime, int maxofMsg)
        {
            var request = new ReceiveMessageRequest()
            {
                QueueUrl = Url,
                VisibilityTimeout = visibilityTime,
                WaitTimeSeconds =  waittime,
                MaxNumberOfMessages = maxofMsg,
                AttributeNames =  new List<string>() { "All" }
            }; 
            var response = client.ReceiveMessage(request);
            if (response.Messages.Count > 0)
            {
                Console.WriteLine("Message recieved successfully");
            }
            else
            {
                Console.WriteLine("No message recieved");
            }
            return response;
            
                        
        }

        public void DeleteMessages(AmazonSQSClient client, string Url, ReceiveMessageResponse response)
        {            
            if (response.Messages.Count > 0)
            {
                foreach (var responseMsg in response.Messages)
                {
                    Console.WriteLine("---Recieved Message ID ---- " + responseMsg.MessageId);
                    Console.WriteLine("---Recieved Message body ---- " + responseMsg.Body);
                    Console.WriteLine("---Recieved receipt handle---- " + responseMsg.ReceiptHandle);
                    Console.WriteLine("---Recieved message attributes---- " + responseMsg.MD5OfMessageAttributes);
                    foreach (var attr in responseMsg.Attributes)
                    {
                        Console.WriteLine("  Attribute  " + attr.Key + ": " + attr.Value);
                    }
                    var deleteReg = new DeleteMessageRequest
                    {
                        QueueUrl = Url,
                        ReceiptHandle = responseMsg.ReceiptHandle
                    };
                    var VerifytDel = client.DeleteMessage(deleteReg);
                    Console.WriteLine("Deleted Message successfully " + VerifytDel.HttpStatusCode.ToString());
                }
            }
            else
            {
                Console.WriteLine("No message recieved");
            }
        }
    }
}
