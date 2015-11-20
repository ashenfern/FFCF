using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FFC.Framework.ClientSubscription.Business
{
    public class WebServiceConnector<T>
    {
        readonly static string webUrl = "http://localhost:11737/api/patient";            //web api url
        //readonly static string webUrl = "http://192.168.1.30:8280/patientapi/patient/";     //esb expoed url

        RestClient client = new RestClient(baseURL);

        readonly static string baseURL = "http://localhost:3493/"; //Base URL - Resource based access
        public string Resource { get; set; }

        public List<T> GetResult()
        {
            string uri = "";
            var request = new RestRequest(Resource, Method.GET);
            var queryResult = client.Execute<List<T>>(request).Data;
            return queryResult == null ? new List<T>() : queryResult; ;
        }

        public T GetResultById(int id)
        {
            string uri = Resource + id + "/";
            var request = new RestRequest(uri, Method.GET);
            var queryResult = client.Execute<List<T>>(request).Data;
            queryResult = queryResult == null ? new List<T>() : queryResult;
            return queryResult.FirstOrDefault();
        }

        public void CreateResult(T data)
        {
            var request = new RestRequest(Resource, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            client.Execute(request);
        }

        public void UpdateResult(T data)
        {
            var request = new RestRequest(Resource, Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(data);
            client.Execute(request);
        }

        public void DeleteResult(int id)
        {
            var request = new RestRequest(Resource + id + "/", Method.DELETE);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(id);
            client.Execute(request);
        }

        public T GetData(int id = -1)
        {
            //string uri = webUrl;
            string uri = baseURL + Resource;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                var data = JsonConvert.DeserializeObjectAsync<T>(response.Result).Result;
                return data;
            }
        }

        public List<T> GetDataList(int id = -1)
        {
            //string uri = webUrl;
            string uri = baseURL + Resource;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                var data = JsonConvert.DeserializeObjectAsync<List<T>>(response.Result).Result;
                return data == null ? new List<T>() : data; ;
            }
        }

        public T GetDataById(int id = -1)
        {
            string uri = baseURL + Resource + id;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                var data = JsonConvert.DeserializeObjectAsync<T>(response.Result).Result;
                return data;
            }
        }

        public List<T> GetDataByName(string name)
        {
            //string uri = webUrl;
            string uri = baseURL + Resource + name;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                var data = JsonConvert.DeserializeObjectAsync<List<T>>(response.Result).Result;
                return data == null ? new List<T>() : data; ;
            }
        }

        public List<T> GetDataListById(int id)
        {
            //string uri = webUrl;
            string uri = baseURL + Resource + id;
            using (HttpClient httpClient = new HttpClient())
            {
                Task<String> response = httpClient.GetStringAsync(uri);
                var data = JsonConvert.DeserializeObjectAsync<List<T>>(response.Result).Result;
                return data == null ? new List<T>() : data; ;
            }
        }


        public void PutData(T data)
        {
            string uri = baseURL + Resource;
            HttpClient client = new HttpClient();
            HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8,
            "application/json");

            client.PostAsync(new Uri(uri), contentPost);

            //.ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
        }
    }
}
