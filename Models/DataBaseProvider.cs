using MySql.Data.MySqlClient;

namespace WebCore.Models
{
    /// <summary>
    /// Clase proveedora de Base de datos
    /// </summary>
    public class DataBaseProvider
    {
        /// <summary>
        /// propiedades
        /// </summary>
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=test;";
        string query = "SELECT * FROM Heroes";

        /// <summary>
        /// Constructor de acceso a la base de datos con parametro para conectarse a la tabla
        /// </summary>
        /// <param name="connectionString"></param>
        public DataBaseProvider(string connectionString)
        {
            this.connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=" + connectionString + ";";
        }
        /// <summary>
        /// metodo para establecer la conexion a la base de datos
        /// </summary>
        /// <returns></returns>
        public MySqlConnection ConectarBaseDeDatos()
        {
            return new MySqlConnection(connectionString);
        }
        /// <summary>
        /// metodo para realizar la consulta a la base de datos
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public MySqlCommand RealizarConsulta(string query, MySqlConnection connection)
        {
            return new MySqlCommand(query, connection);
        }

    }
}
