using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CineController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Cine cine = new ML.Cine();
            ML.Result result =  BL.Cine.GetAll();

            if (result.Correct)
            {
                cine.CineList = result.Objects;
                return View(cine);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Form(int? IdCine)
        {
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            ML.Result resultZona= BL.Zona.GetAll();
            if (IdCine == null)
            {
                cine.Zona.ZonaList=resultZona.Objects;
                return View(cine);
            }
            else
            {
                ML.Result result = BL.Cine.GetById(IdCine.Value);

                if (result.Correct)
                {
                    cine = (ML.Cine)result.Object;
                    cine.Zona.ZonaList= resultZona.Objects;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el alummno seleccionado";
                }
                return View(cine);
            }
        }

        [HttpPost]
        public IActionResult Form(ML.Cine cine)
        {
            ML.Result result = new ML.Result();
            if (cine.IdCine== 0)
            {
                result =BL.Cine.Add(cine);

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
            }
            else
            {
                result=BL.Cine.Update(cine);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Message = result.Message;
                }
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public IActionResult Delete(int? IdCine)
        {
            ML.Result result = BL.Cine.Delete(IdCine.Value);

            return View();
        }
    }
}
