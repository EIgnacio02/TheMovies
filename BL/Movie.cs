using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Movie
    {
        public static ML.Result LoginMovie(string UserName)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();
            try
            {
                using (DL.EignacioApiMovieContext context = new DL.EignacioApiMovieContext())
                {
                    usuario.IdUsuario = (usuario.IdUsuario == null) ? 0 : usuario.IdUsuario;
                    UserName = (UserName == null) ? "" : UserName;
                    var query = context.Usuarios.FromSqlRaw($"LoginMovie '{UserName}'").AsEnumerable().FirstOrDefault();
                    result.Object = new List<object>();

                    if (query != null)
                    {
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;

                        result.Object = usuario; //Unboxing
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error";
            }

            return result;
        }
    }
}