using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace WebCore.Models
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// parametros de la clase
        /// </summary>
        public string nombre { get; set; }
        public string password { get; set; }
        /// <summary>
        /// Metodo para comprobar si existe el usuario en la base de datos
        /// </summary>
        /// <param name="correo"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static async Task<bool> LoginUsuario(string correo, string pass)
        {
            var db = new DataBaseProvider("BBDD_Usuarios");
            var connection = db.ConectarBaseDeDatos();
            string query = "Select usuario, password from usuarios where usuario like '" + correo + "';";
            MySqlCommand command = db.RealizarConsulta(query, connection);
            command.CommandTimeout = 60;
            MySqlDataReader reader;
            bool existe = false;

            try
            {

                await connection.OpenAsync();

                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] row = { reader.GetString(0), reader.GetString(1) };
                        if (row[1].Equals(pass))
                        {
                            existe = true;
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No se encontraron datos.");
                }

                connection.Close();
            }
            catch (Exception ex)
            {

            }
            return existe;
        }
    }
}
