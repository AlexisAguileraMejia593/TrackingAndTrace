using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static List<ML.Rol> GetAll()
        {
            List<ML.Rol> rollist = new List<ML.Rol>();
            try
            {
                using (DL.TrackingAndTraceEntities context = new DL.TrackingAndTraceEntities())
                {
                    var query = context.RolGetAll();
                    if (query != null)
                    {
                        foreach (var registro in query)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = registro.IdRol;
                            rol.Tipo = registro.Tipo;
                            rollist.Add(rol);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rollist;
        }
    }
}