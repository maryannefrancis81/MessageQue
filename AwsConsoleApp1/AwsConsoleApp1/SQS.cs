using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace AwsConsoleApp1
{
    public static class SQS
    {
        public static SQSMethod SQSMethod
        {
            get
            {
                var sqsmethod = new SQSMethod();
                return sqsmethod;
            }
        }
    }

    public class SQSMethod
    {
        public void SQSProcess()
        {
            XmlDocument testdataDoc = new XmlDocument();
            testdataDoc.Load(@"C:\LearnocitySQS\AwsConsoleApp1\AwsConsoleApp1\TestData.xml");
            XmlNode QueueNameNode = testdataDoc.DocumentElement.SelectSingleNode("/testdata/queue/name");
            XmlNode TimeoutNode = testdataDoc.DocumentElement.SelectSingleNode("/testdata/queue/timeout");
            XmlNode sendMessageNode = testdataDoc.DocumentElement.SelectSingleNode("/testdata/sendmessage/body");
            XmlNode delaySecNode = testdataDoc.DocumentElement.SelectSingleNode("/testdata/sendmessage/delaysecond");
            XmlNode visibiityNode =
                testdataDoc.DocumentElement.SelectSingleNode("/testdata/receivemessage/visibilitytimeout");
            XmlNode waitNode = testdataDoc.DocumentElement.SelectSingleNode("/testdata/receivemessage/waittimeout");
            XmlNode msgcountNode = testdataDoc.DocumentElement.SelectSingleNode("/testdata/receivemessage/noofmessage");

            AmazonSQSClient client = SqsClient.SqsClientMethod.CreateSqsClient();

            string queueUrl = Queue.QueueMethod.CreateQueue(client,QueueNameNode.InnerText,TimeoutNode.InnerText);

            Messages.MessageMethod.SendMessages(client, queueUrl,sendMessageNode.InnerText,int.Parse(delaySecNode.InnerText));

           ReceiveMessageResponse response =  Messages.MessageMethod.ReceiveMessages(client,queueUrl,int.Parse(visibiityNode.InnerText),int.Parse(waitNode.InnerText), int.Parse(msgcountNode.InnerText));

            Messages.MessageMethod.DeleteMessages(client, queueUrl, response);
        }
    }
}
