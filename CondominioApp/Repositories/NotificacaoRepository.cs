using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CondominioApp.Helpers;
using CondominioApp.Models;

namespace CondominioApp.Repositories
{
    public class NotificacaoRepository
    {
        public List<Notificacao> GetAll()
        {
            var lista = new List<Notificacao>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT n.id_notificacao, n.mensagem, n.data_envio, n.id_condomino,
                           c.nome AS nome_condomino
                    FROM notificacao n
                    INNER JOIN condomino c ON n.id_condomino = c.id_condomino
                    ORDER BY n.data_envio DESC",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Notificacao
                        {
                            IdNotificacao = reader.GetInt32("id_notificacao"),
                            Mensagem = reader.GetString("mensagem"),
                            DataEnvio = reader.GetDateTime("data_envio"),
                            IdCondomino = reader.GetInt32("id_condomino"),
                            Condomino = new Condomino { Nome = reader.GetString("nome_condomino") }
                        });
                    }
                }
            }

            return lista;
        }

        public void Insert(Notificacao n)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO notificacao (mensagem, id_condomino) VALUES (@mensagem, @idCondomino)",
                    conn);

                cmd.Parameters.AddWithValue("@mensagem", n.Mensagem);
                cmd.Parameters.AddWithValue("@idCondomino", n.IdCondomino);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "DELETE FROM notificacao WHERE id_notificacao=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}