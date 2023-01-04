using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public MovieController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult GetAll()
        {

            ML.Movie movie = new ML.Movie();

            string urlAPI = _configuration["UrlAPI"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);
                var responseTask = client.GetAsync("account/16837206/favorite/movies?api_key=f6a13801fcdc7aabef91af781584a726&session_id=e2f502eacc190c444b67b7c78a05406467d29ca2&language=es-MX&sort_by=created_at.asc&page=1");
                responseTask.Wait();
                var result= responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask= result.Content.ReadAsStringAsync();
                    dynamic resultJson = JObject.Parse(readTask.Result.ToString());
                    readTask.Wait();

                    movie.MovieList = new List<object>();

                    foreach (var resultItem in resultJson.results)
                    {
                        ML.Movie movieObj = new ML.Movie();

                        movieObj.IdMovie = resultItem.id;
                        movieObj.Nombre = resultItem.original_title;
                        movieObj.Descripcion = resultItem.overview;
                        movieObj.Imagen = "https://www.themoviedb.org/t/p/w1280" + resultItem.poster_path;
                        movieObj.Fecha=  resultItem.release_date;
                        movie.MovieList.Add(movieObj);
                    }
                }
            }
            return View(movie);
        }
    }
}
