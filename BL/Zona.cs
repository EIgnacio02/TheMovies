using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Zona
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.EignacioApiMovieContext context= new DL.EignacioApiMovieContext())
                {
                    var query= context.Zonas.FromSqlRaw("ZonaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Zona zona = new ML.Zona();
                            zona.IdZona = (int)obj.IdZona;
                            zona.Descripcion = obj.Descripcion;

                            result.Objects.Add(zona);
                        }
                    }
                }
                result.Correct= true;   
            }
            catch(Exception ex)
            {

            }
            return result;
        }
    }
}
