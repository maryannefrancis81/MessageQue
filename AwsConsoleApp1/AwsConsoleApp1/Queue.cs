using System;
using System.Collections.Generic;
using Amazon.SQS.Model;
using Amazon.SQS.Util;
using System.Xml;
using Amazon.SQS;

namespace AwsConsoleApp1
{
    public static class Queue
    {
        public static QueueMethod QueueMethod
        {
            get
            {
                var Queuemethod = new QueueMethod();
                return Queuemethod;
            }
        }
    }

    public class QueueMethod
    {        
        public string CreateQueue(AmazonSQSClient client, string QueName, string TimeOut)
        {                        
            try
            {
                CreateQueueRequest createRequest = new CreateQueueRequest()
                {
                    QueueName = QueName,
                    Attributes = new Dictionary<string, string>
                    {
                        {SQSConstants.ATTRIBUTE_VISIBILITY_TIMEOUT, TimeOut}
                    }
                };                
                var queueResponse = client.CreateQueue(createRequest);
                var ResponseUrl = queueResponse.QueueUrl;                
                Console.WriteLine(ResponseUrl + "  Queue created successfully");
                return ResponseUrl;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return null;

            }


            
        }
    }
}
