using System;
using System.Collections.Generic;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon;
using Amazon.SQS.Util;
using System.IO;
using System.Text;
using System.Xml;

namespace AwsConsoleApp1
{

    public static class SqsClient
    {
        public static SqsClientMethod SqsClientMethod
        {
            get
            {
                var SqsclientMethod = new SqsClientMethod();
                return SqsclientMethod;
            }
        }
        public static void Main(string[] args)
        {

        }

    }

    public class SqsClientMethod
    {

        public AmazonSQSClient CreateSqsClient()
        {
            var credentials = new BasicAWSCredentials("123", "123");

            var config = new AmazonSQSConfig
            {
                ServiceURL = "http://localhost:9324"
            };

            var sqsClient = new AmazonSQSClient(credentials, config);
            Console.WriteLine("Amazon SQS Client Created");
            return sqsClient;
        }
    }




}