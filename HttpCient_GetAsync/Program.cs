using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using HttpCient_GetAsync;
using Newtonsoft.Json;

Console.WriteLine("----------------------------------------------------------------");
using (var httpClient = new HttpClient())
{
    //var result = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
    //var json = await result.Content.ReadAsStringAsync();
    //Console.WriteLine(result.Headers);
    //Console.WriteLine("----------------------------------------------------------------");
    //Console.WriteLine(result.Content);
    //Console.WriteLine("----------------------------------------------------------------");
    //var posts = JsonConvert.DeserializeObject<List<Post>>(json);
    //Console.WriteLine(json);

    var result = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
    var json = await result.Content.ReadAsStringAsync();

    var posts = JsonConvert.DeserializeObject<List<Post>>(json);

    var selectedPost = posts.First(p => p.Id == 1);
    Console.WriteLine("-------------------------------- Title --------------------------------");
    Console.WriteLine(selectedPost.Title);
    Console.WriteLine("-------------------------------- Body --------------------------------");
    Console.WriteLine(selectedPost.Body);

    selectedPost.Title = "test title";
    selectedPost.Body = "test body";

    Console.WriteLine(selectedPost.Title);
    Console.WriteLine(selectedPost.Body);

    var postJsonContent = new StringContent(JsonConvert.SerializeObject(selectedPost));

    var postResult = await httpClient
        .PostAsync("https://jsonplaceholder.typicode.com/posts", postJsonContent);


    using (var postRequestMessage =
        new HttpRequestMessage(HttpMethod.Post, "https://jsonplaceholder.typicode.com/posts"))
    {

        //postRequestMessage.Headers.Add("someheader", "somevalue");
        postRequestMessage.Content = postJsonContent;

        var post2Result = await httpClient.SendAsync(postRequestMessage);

    }

    var queryParams = HttpUtility.ParseQueryString(string.Empty);
    queryParams["postId"] = "1";
    queryParams["someParam"] = "someValue";

    var formattedParams = queryParams.ToString();

    Console.WriteLine("---------------------------- Formatted params: ------------------------------------");
    Console.WriteLine(formattedParams);
}