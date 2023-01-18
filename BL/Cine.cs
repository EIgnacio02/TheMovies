using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cine
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    var query= context.Cines.FromSqlRaw("CineGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.IdCine= (int)obj.IdCine;
                            cine.Nombre = obj.Nombre;
                            cine.Direccion = obj.Direccion;
                            cine.Venta = (int)obj.Venta;
                            cine.Zona= new ML.Zona();
                            cine.Zona.IdZona = (int)obj.IdZona;
                            cine.Zona.Descripcion = obj.Descripcion;
                            result.Objects.Add(cine);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }


        public static ML.Result GetById(int IdCine)
        {
            ML.Result result= new ML.Result();


            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    var query= context.Cines.FromSqlRaw($"CineGetById {IdCine}").AsEnumerable().FirstOrDefault();
                    result.Objects= new List<object> ();

                    if (query != null)
                    {
                        ML.Cine cine = new ML.Cine();

                        cine.IdCine = (int)query.IdCine;
                        cine.Nombre = query.Nombre;
                        cine.Direccion = query.Direccion;
                        cine.Venta = (int)query.Venta;
                        cine.Zona = new ML.Zona();
                        cine.Zona.IdZona = (int)query.IdZona;
                        cine.Zona.Descripcion = query.Descripcion;

                        result.Object = cine;
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }


        public static ML.Result Add(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineAdd '{cine.Nombre}','{cine.Direccion}',{cine.Venta},{cine.Zona.IdZona}");
                    result.Objects= new List<object> ();

                    if (query>0)
                    {
                        result.Message = "Los datos de registraron correctamente";
                    }

                }
                result.Correct= true;
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }

        public static ML.Result Update(ML.Cine cine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"CineUpdate {cine.IdCine}, '{cine.Nombre}','{cine.Direccion}',{cine.Venta},{cine.Zona.IdZona}");
                    result.Objects= new List<object> ();    

                    if (query>0)
                    {
                        result.Message = "Se actualizaron los datos correctamente";
                    }
                }
                result.Correct=true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static ML.Result Delete(int IdCine)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"CineZonaDelete {IdCine}");
                    result.Objects= new List<object> ();

                    if (query>0)
                    {
                        result.Message = "Se elimino correctamente";
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public static ML.Result GetAllVentas()
        {
            ML.Result result = new ML.Result();

            ML.Cine cine1 = new ML.Cine();
            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    //cine1.IdCine = (cine1.IdCine == null) ? 0 : cine1.IdCine;
					var query = context.Cines.FromSqlRaw("VentasGetAll").ToList();
                    result.Objects= new List<object> ();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Cine cine = new ML.Cine();

                            cine.Nombre = obj.Nombre;
                            cine.Venta = (int)obj.Venta;

                            cine.Zona = new ML.Zona();
                            cine.Zona.Descripcion = obj.Descripcion;

                            result.Objects.Add(cine);
                        }
                    }
				}
                result.Correct= true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
