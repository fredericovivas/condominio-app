using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CondominioApp.Helpers;
using CondominioApp.Models;

namespace CondominioApp.Repositories
{
    public class EventoRepository
    {
        // Converte o texto do MySQL para o enum C#
        private TipoEvento ParseTipo(string tipo)
        {
            switch (tipo)
            {
                case "manutencao": return TipoEvento.Manutencao;
                case "prazo_pagamento": return TipoEvento.PrazoPagamento;
                default: return TipoEvento.Reuniao;
            }
        }

        // Converte o enum C# para o texto que o MySQL espera
        private string TipoParaString(TipoEvento tipo)
        {
            switch (tipo)
            {
                case TipoEvento.Manutencao: return "manutencao";
                case TipoEvento.PrazoPagamento: return "prazo_pagamento";
                default: return "reuniao";
            }
        }

        // ── LISTAR TODOS ──────────────────────────────────────────────
        public List<Evento> GetAll()
        {
            var lista = new List<Evento>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "SELECT id_evento, titulo, tipo, data, descricao FROM evento ORDER BY data",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Evento
                        {
                            IdEvento = reader.GetInt32("id_evento"),
                            Titulo = reader.GetString("titulo"),
                            Tipo = ParseTipo(reader.GetString("tipo")),
                            Data = reader.GetDateTime("data"),
                            Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString("descricao")
                        });
                    }
                }
            }

            return lista;
        }

        // ── INSERIR ───────────────────────────────────────────────────
        public void Insert(Evento e)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO evento (titulo, tipo, data, descricao) VALUES (@titulo, @tipo, @data, @descricao)",
                    conn);

                cmd.Parameters.AddWithValue("@titulo", e.Titulo);
                cmd.Parameters.AddWithValue("@tipo", TipoParaString(e.Tipo));
                cmd.Parameters.AddWithValue("@data", e.Data);
                cmd.Parameters.AddWithValue("@descricao", e.Descricao);

                cmd.ExecuteNonQuery();
            }
        }

        // ── ATUALIZAR ─────────────────────────────────────────────────
        public void Update(Evento e)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "UPDATE evento SET titulo=@titulo, tipo=@tipo, data=@data, descricao=@descricao WHERE id_evento=@id",
                    conn);

                cmd.Parameters.AddWithValue("@titulo", e.Titulo);
                cmd.Parameters.AddWithValue("@tipo", TipoParaString(e.Tipo));
                cmd.Parameters.AddWithValue("@data", e.Data);
                cmd.Parameters.AddWithValue("@descricao", e.Descricao);
                cmd.Parameters.AddWithValue("@id", e.IdEvento);

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
                    "DELETE FROM evento WHERE id_evento=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }

        // ── LISTAR APENAS EVENTOS DE PRAZO DE PAGAMENTO ───────────────
        public List<Evento> GetPrazosPagamento()
        {
            var lista = new List<Evento>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "SELECT id_evento, titulo, tipo, data, descricao FROM evento WHERE tipo = 'prazo_pagamento' ORDER BY data",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Evento
                        {
                            IdEvento = reader.GetInt32("id_evento"),
                            Titulo = reader.GetString("titulo"),
                            Tipo = TipoEvento.PrazoPagamento,
                            Data = reader.GetDateTime("data"),
                            Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? "" : reader.GetString("descricao")
                        });
                    }
                }
            }

            return lista;
        }
    }
}