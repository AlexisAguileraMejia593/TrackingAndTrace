using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Usuario GetAll()
        {
            ML.Usuario usuarioObj = new ML.Usuario();
            usuarioObj.Usuarios = new List<ML.Usuario>();
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
                            usuario.Password = registro.Password.ToString();
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = registro.IdRol.Value;
                            usuario.Email = registro.Email;
                            usuario.Nombre = registro.Nombre;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuarioObj.Usuarios.Add(usuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return usuarioObj;
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
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Rol = new ML.Rol();
                        usuario.Email = query.Email;
                        usuario.Password = query.Password.ToString();
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
        public static ML.Usuario GetById(int IdUsuario)
        {
            ML.Usuario result = null;
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
                        usuario.Password = query.Password.ToString();
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol;
                        usuario.Email = query.Email;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Rol.Tipo = query.Tipo;

                        result = usuario;
                    }
                }
            }
            catch
            {
                // Manejo de excepciones
            }
            return result;
        }
        public static string Add(ML.Usuario usuario)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    string hashedPassword = EncryptPassword(usuario.Password); // Aplica la función de hash a la contraseña
                    var query = context.UsuarioAdd(
                        usuario.UserName,
                        hashedPassword,
                        usuario.Rol.IdRol,
                        usuario.Email,
                        usuario.Nombre,
                        usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno
                        );

                    if (query > 0)
                    {
                        return "true";
                    }
                    else
                    {
                        return "false";
                    }
                }
            }
            catch (Exception ex)
            {
                return "false";
            }
        }




        public static bool Update(ML.Usuario usuario)
        {
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    string hashedPassword = EncryptPassword(usuario.Password);
                    var query = context.UsuarioUpdate(
                        usuario.IdUsuario,
                        usuario.UserName,
                        hashedPassword,
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
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}