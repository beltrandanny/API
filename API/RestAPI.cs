using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    class Employee
    {
        public string Status { get; set; }
        public Data Data { get; set; }
    }

    class Data
    {
        public string Name { get; set; }
        public string Salary { get; set; }
        public string Age { get; set; }
        public string ID { get; set; }
    }


    class RestAPI
    {
        public static string CreateRecord()
        {
            var request = (HttpWebRequest)WebRequest.Create("https://dummy.restapiexample.com/api/v1/create");
            request.ContentType = "application/json";
            request.Method = "POST";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string jsonData = "{\"name\":\"dannyb\"," +
                                    "\"salary\":\"1234\"," +
                                    "\"age\":\"1234\"}";

                //Data empData = new Data()
                //{
                //    Name = "Danny",
                //    Salary = "$2,000",
                //    Age = "30"
                //};

                //string jsonData = SerializeEmployeeData(empData).ToString();
                streamWriter.Write(jsonData);
                //Console.WriteLine(jsonData);
            }

            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                string json = result.ToString();
                Console.WriteLine(json);
                
                //json deserializer
                string strNewId = DeserializeEmployee(json).Data.ID.ToString();

                //json parser
                //string strNewId = JObject.Parse(JObject.Parse(json)["data"].ToString())["id"].ToString();

                Console.WriteLine("Employee's ID just created: " + strNewId);
                return strNewId;
            }
        }

        internal static string SerializeEmployeeData(Data pEmployeeData)
        {
            string strJson = JsonConvert.SerializeObject(pEmployeeData);
            return strJson;
        }

        internal static Employee DeserializeEmployee(string pstrJson)
        {
            Employee emp = JsonConvert.DeserializeObject<Employee>(pstrJson);
            return emp;
        }

        public static void DeleteRecord(string pstrID)
        {
            string strUrl = "https://dummy.restapiexample.com/api/v1/delete/" + pstrID;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strUrl);
            request.Method = "DELETE";
            WebResponse response = request.GetResponse();
            Stream receiveStream = response.GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            Console.WriteLine(readStream.ReadToEnd());
            Console.WriteLine("ID to delete: " + pstrID);
            response.Close();
            readStream.Close();
        }
    }
}
