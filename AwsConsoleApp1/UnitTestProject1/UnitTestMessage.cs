using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AwsConsoleApp1;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestMessage
    {
        [TestMethod]
        public void Send_New_Message()
        {                     
            SQSMethod messages = new SQSMethod();
            messages.SQSProcess();
        }
    }
}
