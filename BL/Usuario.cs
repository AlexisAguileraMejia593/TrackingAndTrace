using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
                        usuario.Rol = new ML.Rol();
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.Rol.IdRol = query.IdRol;
                        usuario.Rol.Tipo = query.Tipo;

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
        public static object GetById(int IdUsuario)
        {
            object result = null;
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Password = query.Password;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        usuario.Email = query.Email;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;

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
        public static bool Add(ML.Usuario usuario)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.UsuarioAdd(
                        usuario.UserName,
                        usuario.Password,
                        usuario.Rol.IdRol,
                        usuario.Email,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno
                        );

                    if (query > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }            
        }
        public static bool Update(ML.Usuario usuario)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.UsuarioUpdate(
                        usuario.IdUsuario,
                        usuario.UserName,
                        usuario.Password,
                        usuario.Rol.IdRol,
                        usuario.Email,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno
                        );

                    if (query > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool Delete(int IdUsuario)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var registro = context.UsuarioDelete(IdUsuario);
                    if (registro > 0)
                    {
                         return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}