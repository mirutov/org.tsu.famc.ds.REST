using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProductStoreClient
{
    public class Book
    {
        public string Author { get; set; }
        public string Title { get; set; }
    }

    public class Array
    {
        public int[] data;
    }

    class Program
    {
        static void Main()
        {
            ArraySampleTest().Wait();
            BookSampleGetTest().Wait();
            BookSamplePostTest().Wait();
            Console.ReadLine();
        }

        static async Task ArraySampleTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:12345/");
            Array arg = new Array();
            arg.data = new int[] { 1, 2, 3, 4, 5 };
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Sample/ArraySample", arg);
            if (response.IsSuccessStatusCode)
            {
                Array ret = response.Content.ReadAsAsync<Array>().Result;
                foreach (int i in ret.data)
                {
                    Console.Write(i + " ");
                }
            }
            Console.WriteLine();
        }

        static async Task BookSampleGetTest()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:12345/");
            HttpResponseMessage response = await client.GetAsync("api/Sample/BookSampleGet?author=Дуглас Адамс&title=Автостопом по галактике");
            if (response.IsSuccessStatusCode)
            {
                Book ret = response.Content.ReadAsAsync<Book>().Result;
                Console.WriteLine(ret.Author + ": " + ret.Title);
            }
        }

        static async Task BookSamplePostTest()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:12345/");
            Book arg = new Book();
            arg.Author = "Михаил Булгаков";
            arg.Title = "Мастер и Маргарита";
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Sample/BookSamplePost", arg);
            if (response.IsSuccessStatusCode)
            {
                Book ret = response.Content.ReadAsAsync<Book>().Result;
                Console.WriteLine(ret.Author + ": " + ret.Title);
            }
            Console.WriteLine();
        }
    }
}