using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CondominioApp.Helpers;
using CondominioApp.Models;

namespace CondominioApp.Repositories
{
    public class IntervencaoRepository
    {
        public List<Intervencao> GetAll()
        {
            var lista = new List<Intervencao>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT i.id_intervencao, i.descricao, i.custo, i.data, i.id_evento,
                           e.titulo AS titulo_evento
                    FROM intervencao i
                    INNER JOIN evento e ON i.id_evento = e.id_evento
                    ORDER BY i.data DESC",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Intervencao
                        {
                            IdIntervencao = reader.GetInt32("id_intervencao"),
                            Descricao = reader.GetString("descricao"),
                            Custo = reader.IsDBNull(reader.GetOrdinal("custo")) ? (decimal?)null : reader.GetDecimal("custo"),
                            Data = reader.GetDateTime("data"),
                            IdEvento = reader.GetInt32("id_evento"),
                            Evento = new Evento { Titulo = reader.GetString("titulo_evento") }
                        });
                    }
                }
            }

            return lista;
        }

        public void Insert(Intervencao i)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO intervencao (descricao, custo, data, id_evento) VALUES (@descricao, @custo, @data, @idEvento)",
                    conn);

                cmd.Parameters.AddWithValue("@descricao", i.Descricao);
                cmd.Parameters.AddWithValue("@custo", (object)i.Custo ?? System.DBNull.Value);
                cmd.Parameters.AddWithValue("@data", i.Data);
                cmd.Parameters.AddWithValue("@idEvento", i.IdEvento);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "DELETE FROM intervencao WHERE id_intervencao=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}