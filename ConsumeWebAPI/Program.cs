using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsumeWebAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://reqres.in/");

                    #region GET Request
                    var getresponseTask = client.GetAsync("api/users/2");
                    getresponseTask.Wait();
                    var getresult = getresponseTask.Result;

                    if (getresult.IsSuccessStatusCode)
                    {
                        var getreadTask = getresult.Content.ReadAsStringAsync();
                        getreadTask.Wait();

                        var deserialized = JsonConvert.DeserializeObject<UserResult>(getreadTask.Result);

                        Console.WriteLine(deserialized.data);
                    }
                    else
                    {
                        Console.WriteLine(getresult.StatusCode);
                        Console.WriteLine(getresult.RequestMessage);
                    }
                    #endregion

                    #region POST Request

                    var postuser = new User() { first_name = "Ajay", last_name = "Pawar", email = "aj@pw.com", avatar = "" };

                    var postcontent = new StringContent(JsonConvert.SerializeObject(postuser), UnicodeEncoding.UTF8, "application/json");
                    var postTask = client.PostAsync("api/users", postcontent);
                    postTask.Wait();

                    var postResult = postTask.Result;
                    if (postResult.IsSuccessStatusCode)
                    {
                        var postread = postResult.Content.ReadAsStringAsync();
                        postread.Wait();
                    }
                    #endregion

                    #region PUT Request
                    var putuser = new User { name="Ajit",job="Pawar"};
                    var putcontent = new StringContent(JsonConvert.SerializeObject(putuser), UnicodeEncoding.UTF8, "application/json");
                    var puttask = client.PutAsync("api/users/2", putcontent);
                    puttask.Wait();

                    var putresult = puttask.Result;
                    if (putresult.IsSuccessStatusCode)
                    {
                        var putread = putresult.Content.ReadAsStringAsync();
                        putread.Wait();
                    }

                    #endregion

                    #region DELETE Request
                    var deleteresponseTask = client.DeleteAsync("api/users/2");
                    deleteresponseTask.Wait();
                    var deleteresult = getresponseTask.Result;

                    if (deleteresult.IsSuccessStatusCode)
                    {
                        var getreadTask = deleteresult.Content.ReadAsStringAsync();
                        getreadTask.Wait();

                        var deserialized = JsonConvert.DeserializeObject<UserResult>(getreadTask.Result);

                        Console.WriteLine(deserialized.data);
                    }
                    else
                    {
                        Console.WriteLine(deleteresult.StatusCode);
                        Console.WriteLine(deleteresult.RequestMessage);
                    }
                    #endregion
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.InnerException);
                }
                Console.ReadKey();
            }
        }

        public class User
        {
            public int id { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string avatar { get; set; }
            public string name { get; set; }
            public string job { get; set; }

        }

        public class UserResult
        {
            public User data { get; set; }

        }
    }
}
