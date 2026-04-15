using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CondominioApp.Helpers;
using CondominioApp.Models;

namespace CondominioApp.Repositories
{
    public class ApartamentoRepository
    {
        // ── LISTAR TODOS ──────────────────────────────────────────────
        public List<Apartamento> GetAll()
        {
            var lista = new List<Apartamento>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "SELECT id_apartamento, bloco, piso, numero FROM apartamento ORDER BY bloco, numero",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Apartamento
                        {
                            IdApartamento = reader.GetInt32("id_apartamento"),
                            Bloco = reader.GetString("bloco"),
                            Piso = reader.GetInt32("piso"),
                            Numero = reader.GetString("numero")
                        });
                    }
                }
            }

            return lista;
        }

        // ── INSERIR ───────────────────────────────────────────────────
        public void Insert(Apartamento a)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO apartamento (bloco, piso, numero) VALUES (@bloco, @piso, @numero)",
                    conn);

                cmd.Parameters.AddWithValue("@bloco", a.Bloco);
                cmd.Parameters.AddWithValue("@piso", a.Piso);
                cmd.Parameters.AddWithValue("@numero", a.Numero);

                cmd.ExecuteNonQuery();
            }
        }

        // ── ATUALIZAR ─────────────────────────────────────────────────
        public void Update(Apartamento a)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "UPDATE apartamento SET bloco=@bloco, piso=@piso, numero=@numero WHERE id_apartamento=@id",
                    conn);

                cmd.Parameters.AddWithValue("@bloco", a.Bloco);
                cmd.Parameters.AddWithValue("@piso", a.Piso);
                cmd.Parameters.AddWithValue("@numero", a.Numero);
                cmd.Parameters.AddWithValue("@id", a.IdApartamento);

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
                    "DELETE FROM apartamento WHERE id_apartamento=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}