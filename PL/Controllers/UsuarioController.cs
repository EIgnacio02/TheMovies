using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string userName, string? password)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            //ML.Result result = BL.Movie.LoginMovie(userName);

            try
            {
                string urlAPI = _configuration["UrlAPILogin"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("Login/" + userName);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Usuario resultItemList = new ML.Usuario();

                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;


                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            usuario = (ML.Usuario)result.Object;
                                                            
            if (result.Correct)
            {

                if (usuario.UserName == userName && usuario.Password == password)
                {
                    //ViewBag.Message = result.Message;
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ViewBag.Message = "email o password incorrectos ";
                    return PartialView("ModalLogin");

                }
            }
            else
            {
                ViewBag.Message = "email o password incorrectos ";
                return PartialView("ModalLogin");
            }

        }
    }
}
