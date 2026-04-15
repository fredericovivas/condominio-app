using System.Configuration;
using MySql.Data.MySqlClient;

namespace CondominioApp.Helpers
{
    public static class DatabaseConnection
    {
        // Lê a connection string do App.config
        private static readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["CondominioDb"].ConnectionString;

        // Devolve uma ligação pronta a usar
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}