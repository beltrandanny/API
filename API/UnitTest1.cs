using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace API
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetMethod()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://dummy.restapiexample.com/api/v1/employees");
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            Console.WriteLine(readStream.ReadToEnd());
            response.Close();
            readStream.Close();
        }
        [TestMethod]
        public void TestInsertMethod()
        {
            Console.WriteLine(RestAPI.CreateRecord());        
        }
        [TestMethod]
        public void TestDeleteMethod()
        {
            RestAPI.DeleteRecord("2");
        }

        [TestMethod]
        public void TestCreateAndDeleteRecord()
        {
            RestAPI.DeleteRecord(RestAPI.CreateRecord());
        }
    }
}
