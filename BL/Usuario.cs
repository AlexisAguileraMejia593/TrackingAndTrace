using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static List<ML.Usuario> GetAll()
        {
            List<ML.Usuario> usuariolist = new List<ML.Usuario>();
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.UsuarioGetAll();
                    if (query != null)
                    {               
                        foreach (var registro in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = registro.IdUsuario;
                            usuario.UserName = registro.UserName;
                            usuario.Password = registro.Password;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = registro.IdRol.Value;
                            usuario.Email = registro.Email;
                            usuario.Nombre = registro.Nombre;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuariolist.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuariolist;
        }
        public static object GetByEmail(string email)
        {
            object result = null;
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.UsuarioGetByEmail(email).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;

                        // Boxing
                        result = (object)usuario;
                    }
                }
            }
            catch
            {
                // Manejo de excepciones
            }
            return result;
        }
    }
}
