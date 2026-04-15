using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CondominioApp.Helpers;
using CondominioApp.Models;

namespace CondominioApp.Repositories
{
    public class CondominoRepository
    {
        // ── LISTAR TODOS (com dados do apartamento incluídos) ─────────
        public List<Condomino> GetAll()
        {
            var lista = new List<Condomino>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT c.id_condomino, c.nome, c.email, c.telefone, c.id_apartamento,
                           a.bloco, a.piso, a.numero
                    FROM condomino c
                    INNER JOIN apartamento a ON c.id_apartamento = a.id_apartamento
                    ORDER BY c.nome",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Condomino
                        {
                            IdCondomino = reader.GetInt32("id_condomino"),
                            Nome = reader.GetString("nome"),
                            Email = reader.IsDBNull(reader.GetOrdinal("email")) ? "" : reader.GetString("email"),
                            Telefone = reader.IsDBNull(reader.GetOrdinal("telefone")) ? "" : reader.GetString("telefone"),
                            IdApartamento = reader.GetInt32("id_apartamento"),
                            Apartamento = new Apartamento
                            {
                                IdApartamento = reader.GetInt32("id_apartamento"),
                                Bloco = reader.GetString("bloco"),
                                Piso = reader.GetInt32("piso"),
                                Numero = reader.GetString("numero")
                            }
                        });
                    }
                }
            }

            return lista;
        }

        // ── INSERIR ───────────────────────────────────────────────────
        public void Insert(Condomino c)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO condomino (nome, email, telefone, id_apartamento) VALUES (@nome, @email, @telefone, @idApartamento)",
                    conn);

                cmd.Parameters.AddWithValue("@nome", c.Nome);
                cmd.Parameters.AddWithValue("@email", c.Email);
                cmd.Parameters.AddWithValue("@telefone", c.Telefone);
                cmd.Parameters.AddWithValue("@idApartamento", c.IdApartamento);

                cmd.ExecuteNonQuery();
            }
        }

        // ── ATUALIZAR ─────────────────────────────────────────────────
        public void Update(Condomino c)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "UPDATE condomino SET nome=@nome, email=@email, telefone=@telefone, id_apartamento=@idApartamento WHERE id_condomino=@id",
                    conn);

                cmd.Parameters.AddWithValue("@nome", c.Nome);
                cmd.Parameters.AddWithValue("@email", c.Email);
                cmd.Parameters.AddWithValue("@telefone", c.Telefone);
                cmd.Parameters.AddWithValue("@idApartamento", c.IdApartamento);
                cmd.Parameters.AddWithValue("@id", c.IdCondomino);

                cmd.ExecuteNonQuery();
            }
        }

        // ── APAGAR ────────────────────────────────────────────────────
        public void Delete(int id)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "DELETE FROM condomino WHERE id_condomino=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}