using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebCore.Models
{
    /// <summary>
    /// clase heroe
    /// </summary>
    public class Heroe
    {
        /// <summary>
        /// propiedades
        /// </summary>
        public int? id { get; set; }
        public string nombre { get; set; }
        public string img { get; set; }
        public DateTime aparicion { get; set; } = DateTime.Today.Date;
        public string? casaheroe { get; set; }

        /// <summary>
        /// Metodo para obtener todos los heroes dependiendo de la casa
        /// </summary>
        /// <param name="casaheroe"></param>
        /// <returns></returns>
        public static async Task<List<Heroe>> GetAllHeroesFromHouse(string casaheroe)
        {
            List<Heroe> heroes = new List<Heroe>();
            if (casaheroe != "todas")
            {

                var db = new DataBaseProvider(casaheroe);
                var connection = db.ConectarBaseDeDatos();
                string query = "SELECT * FROM Heroes";
                MySqlCommand command = db.RealizarConsulta(query, connection);
                command.CommandTimeout = 60;
                MySqlDataReader reader;

                try
                {

                    await connection.OpenAsync();

                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Heroe heroe = new Heroe();

                            heroe.id = int.Parse(reader.GetString(0));
                            heroe.nombre = reader.GetString(1);
                            heroe.img = reader.GetString(2);
                            heroe.aparicion = Convert.ToDateTime(reader.GetString(3));
                            heroe.casaheroe = casaheroe.Equals("BBDD_DC") ? "DC" : "Marvel";
                            heroes.Add(heroe);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    connection.Close();
                }

            }
            else
            {
                string[] databases = { "BBDD_DC", "BBDD_Marvel" };
                for (int i = 0; i < databases.Length; i++)
                {
                    string database = databases[i];
                    var db = new DataBaseProvider(database);
                    var connection = db.ConectarBaseDeDatos();
                    string query = "SELECT * FROM Heroes";
                    MySqlCommand command = db.RealizarConsulta(query, connection);
                    command.CommandTimeout = 60;
                    MySqlDataReader reader;

                    try
                    {

                        await connection.OpenAsync();

                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Heroe heroe = new Heroe();

                                heroe.id = int.Parse(reader.GetString(0));
                                heroe.nombre = reader.GetString(1);
                                heroe.img = reader.GetString(2);
                                heroe.aparicion = Convert.ToDateTime(reader.GetString(3));
                                heroe.casaheroe = database.Equals("BBDD_DC") ? "DC" : "Marvel";
                                heroes.Add(heroe);
                            }
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }
            return heroes;
        }
        /// <summary>
        /// Metodo para obtener un heroe de una base de datos segun su id
        /// </summary>
        /// <param name="baseDatos"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Heroe> GetHeroeById(string baseDatos, int id)
        {
            Heroe heroe = new Heroe();


            var db = new DataBaseProvider(baseDatos);
            var connection = db.ConectarBaseDeDatos();
            string query = $"SELECT * FROM Heroes where id={id}";
            MySqlCommand command = db.RealizarConsulta(query, connection);
            command.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {

                await connection.OpenAsync();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        heroe.id = int.Parse(reader.GetString(0));
                        heroe.nombre = reader.GetString(1);
                        heroe.img = reader.GetString(2);
                        heroe.aparicion = Convert.ToDateTime(reader.GetString(3));
                        heroe.casaheroe = baseDatos.Equals("BBDD_DC") ? "DC" : "Marvel";

                    }
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return heroe;
        }
        /// <summary>
        /// Metodo para actualizar un heroe de una base de datos segun su id
        /// </summary>
        /// <param name="baseDatos"></param>
        /// <param name="id"></param>
        /// <param name="heroe"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateHeroeInDB(string baseDatos, int id, Heroe heroe)
        {

            var db = new DataBaseProvider(baseDatos);
            var connection = db.ConectarBaseDeDatos();

            string query = "UPDATE Heroes SET id=@id, nombre=@nombre, img=@img, aparicion=@aparicion where id=@id";
            MySqlCommand command = db.RealizarConsulta(query, connection);
            command.CommandTimeout = 60;

            try
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", heroe.nombre);
                command.Parameters.AddWithValue("@img", heroe.img);
                command.Parameters.AddWithValue("@aparicion", heroe.aparicion);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }

        }
        /// <summary>
        /// Metodo para borrar un heroe de una base de datos segun su id
        /// </summary>
        /// <param name="baseDatos"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteHeroeInDB(string baseDatos, int id)
        {

            var db = new DataBaseProvider(baseDatos);
            var connection = db.ConectarBaseDeDatos();

            string query = "DELETE FROM Heroes where id=@id";
            MySqlCommand command = db.RealizarConsulta(query, connection);
            command.CommandTimeout = 60;

            try
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }

        }
        /// <summary>
        /// Metodo para añadir un heroe a una base de datos
        /// </summary>
        /// <param name="baseDatos"></param>
        /// <param name="heroe"></param>
        /// <returns></returns>
        public static async Task<bool> AddHeroeToDB(string baseDatos, Heroe heroe)
        {
            var db = new DataBaseProvider(baseDatos);
            var connection = db.ConectarBaseDeDatos();

            string query = "INSERT INTO Heroes(nombre, img, aparicion) VALUES (@nombre, @img,@aparicion)";
            MySqlCommand command = db.RealizarConsulta(query, connection);
            command.CommandTimeout = 60;

            try
            {
                await connection.OpenAsync();
                command.Parameters.AddWithValue("@nombre", heroe.nombre);
                command.Parameters.AddWithValue("@img", heroe.img);
                command.Parameters.AddWithValue("@aparicion", heroe.aparicion);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
