using DL;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class Unidad
    {
        public static bool Add(ML.Unidad unidad)
            {
            try
            {
                //todo lo que ejecute dentro de un using se libera al final
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "INSERT INTO UnidadEntrega(NumeroPlaca, Modelo, Marca, AñoFabricacion, IdEstatusUnidad) VALUES(@NumeroPlaca, @Modelo, @Marca, @AñoFabricacion, @IdEstatusUnidad)" ;
                    SqlCommand cmd = new SqlCommand(query, context);
                    SqlParameter[] collection = new SqlParameter[5];

                    collection[0] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[0].Value = unidad.NumeroPlaca;
                    collection[1] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[1].Value = unidad.Modelo;
                    collection[2] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[2].Value = unidad.Marca;
                    collection[3] = new SqlParameter("@AñoFabricacion", SqlDbType.VarChar);
                    collection[3].Value = unidad.AñoFabricacion;
                    collection[4] = new SqlParameter("@IdEstatusUnidad", SqlDbType.VarChar);
                    collection[4].Value = unidad.IdEstatusUnidad;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
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
        public static List<ML.Unidad> GetAll()
        {
            List<ML.Unidad> unidades = new List<ML.Unidad>();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT IdUnidad, NumeroPlaca, Modelo, Marca, AñoFabricacion, IdEstatusUnidad FROM UnidadEntrega";

                    SqlCommand cmd = new SqlCommand(query, context);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsuario = new DataTable();

                    adapter.Fill(tablaUsuario);
                    if (tablaUsuario.Rows.Count > 0)
                    {
                        foreach (DataRow row in tablaUsuario.Rows)
                        {

                            ML.Unidad unidad = new ML.Unidad();
                            unidad.IdUnidad = int.Parse(row[0].ToString());
                            unidad.NumeroPlaca = row[1].ToString();
                            unidad.Modelo = row[2].ToString();
                            unidad.Marca = row[3].ToString();
                            unidad.AñoFabricacion = row[4].ToString();
                            unidad.IdEstatusUnidad = int.Parse(row[5].ToString());
                            unidades.Add(unidad);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return unidades;
        }

        public static ML.Unidad GetById(int IdUnidad)
        {
            ML.Unidad result = null;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "SELECT IdUnidad, NumeroPlaca, Modelo, Marca, AñoFabricacion, IdEstatusUnidad FROM UnidadEntrega WHERE IdUnidad = @IdUnidad";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;
                    cmd.Parameters.AddRange(collection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tableUsuario = new DataTable();

                    adapter.Fill(tableUsuario);

                    List<ML.Unidad> list = new List<ML.Unidad>();

                    if (tableUsuario.Rows.Count > 0)
                    {
                        DataRow row = tableUsuario.Rows[0];

                        ML.Unidad unidadlist = new ML.Unidad();
                        unidadlist.IdUnidad = int.Parse(row[0].ToString());
                        unidadlist.NumeroPlaca = row[1].ToString();
                        unidadlist.Modelo = row[2].ToString();
                        unidadlist.Marca = row[3].ToString();
                        unidadlist.AñoFabricacion = row[4].ToString();
                        unidadlist.IdEstatusUnidad = int.Parse(row[5].ToString());

                        //boxing

                        object boxedunidad = unidadlist;
                        list.Add(unidadlist);
                    }
                    result = list.FirstOrDefault();
                }
            }
            catch
            {

            }
            return result;
        }
        public static bool Update(ML.Unidad unidad)
        {
            try
            {
                //todo lo que ejecute dentro de un using se libera al final
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "UPDATE UnidadEntrega SET NumeroPlaca = @NumeroPlaca, Modelo = @Modelo, Marca = @Marca, AñoFabricacion = @AñoFabricacion, IdEstatusUnidad = @IdEstatusUnidad WHERE IdUnidad = @IdUnidad;";

                    SqlCommand cmd = new SqlCommand(query, context);
                    SqlParameter[] collection = new SqlParameter[5];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = unidad.IdUnidad;


                    collection[1] = new SqlParameter("@NumeroPlaca", SqlDbType.VarChar);
                    collection[1].Value = unidad.NumeroPlaca;
                    collection[2] = new SqlParameter("@Modelo", SqlDbType.VarChar);
                    collection[2].Value = unidad.Modelo;
                    collection[3] = new SqlParameter("@Marca", SqlDbType.VarChar);
                    collection[3].Value = unidad.Marca;
                    collection[4] = new SqlParameter("@AñoFabricacion", SqlDbType.DateTime);
                    collection[4].Value = unidad.AñoFabricacion;
                    collection[4] = new SqlParameter("@IdEstatusUnidad", SqlDbType.DateTime);
                    collection[4].Value = unidad.IdEstatusUnidad;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
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
        public static bool Delete(int IdUnidad)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "DELETE FROM UnidadEntrega WHERE IdUnidad = @IdUnidad";

                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUnidad", SqlDbType.Int);
                    collection[0].Value = IdUnidad;

                    cmd.Parameters.AddRange(collection);

                    context.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    context.Close();

                    if (rowsAffected > 0)
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