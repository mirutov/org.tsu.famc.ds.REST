using System;
using System.Collections;
using System.Web.Http;
using System.Web.Http.Description;
using Owin;
using Microsoft.Owin.Hosting;
using System.Runtime.Serialization;
using System.Web.Http.Cors;
using System.Net.Http.Formatting;

namespace org.tsu.famc.ds.REST
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

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class SampleController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ArraySample(Array arg)
        {
            for (int i = 0; i < arg.data.Length/2; i++)
            {
                arg.data[i] ^= arg.data[arg.data.Length - i - 1];
                arg.data[arg.data.Length - i - 1] ^= arg.data[i];
                arg.data[i] ^= arg.data[arg.data.Length - i - 1];
            }
            return Json<Array>(arg);
        }

        [HttpGet]
        public IHttpActionResult BookSampleGet(string author, string title)
        {
            Book book = new Book();
            book.Author = author;
            book.Title = title;
            return Json<Book>(book);
        }

        [HttpPost]
        public IHttpActionResult BookSamplePost(Book book)
        {
            Book ret = new Book();
            ret.Author= book.Author + " (copy)";
            ret.Title = book.Title + " (copy)";
            return Json<Book>(ret);
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.EnableCors();
            config.Routes.MapHttpRoute(
              "Api", "api/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional }
            );
            app.UseWebApi(config);
        }
    }

    public class RESTServer
    {
        static void Main(string[] args)
        {
            string baseUrl = "http://localhost:12345/";

            try
            {
                WebApp.Start<Startup>(baseUrl);
                Console.WriteLine("Started, Press any key to stop.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }

        }
    }
}
