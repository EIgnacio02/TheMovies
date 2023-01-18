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


        public IActionResult Grafica()
        {
            ML.Cine cine = new ML.Cine();
            cine.Zona = new ML.Zona();
            ML.Result result = new ML.Result();

            cine.Total = 0;
            cine.Norte = 0;
            cine.Sur = 0;
            cine.Este = 0;
            cine.Oeste = 0;

            result = BL.Cine.GetAll();
            if (result.Correct)
            {
                cine.CineList = result.Objects;
                if (cine.CineList.Count > 0)
                    foreach (ML.Cine cines in cine.CineList)
                    {
                        if (cines.Zona.IdZona == 1)
                        {
                            cine.Norte += cines.Venta;
                        }
                        if (cines.Zona.IdZona == 2)
                        {
                            cine.Sur += cines.Venta;
                        }
                        if (cines.Zona.IdZona == 3)
                        {
                            cine.Este += cines.Venta;
                        }
                        if (cines.Zona.IdZona == 4)
                        {
                            cine.Oeste += cines.Venta;
                        }
                        cine.Total += cines.Venta;
                    }
                    cine.Norte = (cine.Norte * 100) / cine.Total;
                    cine.Sur = (cine.Sur * 100) / cine.Total;
                    cine.Este = (cine.Este * 100) / cine.Total;
                    cine.Oeste = (cine.Oeste * 100) / cine.Total;
            }
            return View(cine);
        }
    }
}
