using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CondominioApp.Helpers;
using CondominioApp.Models;

namespace CondominioApp.Repositories
{
    public class PagamentoRepository
    {
        private EstadoPagamento ParseEstado(string estado)
        {
            return estado == "pago" ? EstadoPagamento.Pago : EstadoPagamento.EmDivida;
        }

        private string EstadoParaString(EstadoPagamento estado)
        {
            return estado == EstadoPagamento.Pago ? "pago" : "em_divida";
        }

        // ── LISTAR TODOS (com nome do condómino e título do evento) ───
        public List<Pagamento> GetAll()
        {
            var lista = new List<Pagamento>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT p.id_pagamento, p.valor, p.estado, p.data_registo,
                           p.id_condomino, p.id_evento,
                           c.nome AS nome_condomino,
                           e.titulo AS titulo_evento
                    FROM pagamento p
                    INNER JOIN condomino c ON p.id_condomino = c.id_condomino
                    INNER JOIN evento    e ON p.id_evento    = e.id_evento
                    ORDER BY p.data_registo DESC",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Pagamento
                        {
                            IdPagamento = reader.GetInt32("id_pagamento"),
                            Valor = reader.GetDecimal("valor"),
                            Estado = ParseEstado(reader.GetString("estado")),
                            DataRegisto = reader.GetDateTime("data_registo"),
                            IdCondomino = reader.GetInt32("id_condomino"),
                            IdEvento = reader.GetInt32("id_evento"),
                            Condomino = new Condomino { Nome = reader.GetString("nome_condomino") },
                            Evento = new Evento { Titulo = reader.GetString("titulo_evento") }
                        });
                    }
                }
            }

            return lista;
        }

        // ── LISTAR DEVEDORES ──────────────────────────────────────────
        public List<Pagamento> GetDevedores()
        {
            var lista = new List<Pagamento>();

            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
                    SELECT p.id_pagamento, p.valor, p.estado, p.data_registo,
                           p.id_condomino, p.id_evento,
                           c.nome AS nome_condomino,
                           e.titulo AS titulo_evento
                    FROM pagamento p
                    INNER JOIN condomino c ON p.id_condomino = c.id_condomino
                    INNER JOIN evento    e ON p.id_evento    = e.id_evento
                    WHERE p.estado = 'em_divida'
                    ORDER BY p.data_registo DESC",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Pagamento
                        {
                            IdPagamento = reader.GetInt32("id_pagamento"),
                            Valor = reader.GetDecimal("valor"),
                            Estado = ParseEstado(reader.GetString("estado")),
                            DataRegisto = reader.GetDateTime("data_registo"),
                            IdCondomino = reader.GetInt32("id_condomino"),
                            IdEvento = reader.GetInt32("id_evento"),
                            Condomino = new Condomino { Nome = reader.GetString("nome_condomino") },
                            Evento = new Evento { Titulo = reader.GetString("titulo_evento") }
                        });
                    }
                }
            }

            return lista;
        }

        // ── INSERIR ───────────────────────────────────────────────────
        public void Insert(Pagamento p)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "INSERT INTO pagamento (valor, estado, id_condomino, id_evento) VALUES (@valor, @estado, @idCondomino, @idEvento)",
                    conn);

                cmd.Parameters.AddWithValue("@valor", p.Valor);
                cmd.Parameters.AddWithValue("@estado", EstadoParaString(p.Estado));
                cmd.Parameters.AddWithValue("@idCondomino", p.IdCondomino);
                cmd.Parameters.AddWithValue("@idEvento", p.IdEvento);

                cmd.ExecuteNonQuery();
            }
        }

        // ── ATUALIZAR ESTADO (marcar como pago) ───────────────────────
        public void UpdateEstado(int idPagamento, EstadoPagamento estado)
        {
            using (var conn = DatabaseConnection.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(
                    "UPDATE pagamento SET estado=@estado WHERE id_pagamento=@id",
                    conn);

                cmd.Parameters.AddWithValue("@estado", EstadoParaString(estado));
                cmd.Parameters.AddWithValue("@id", idPagamento);

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
                    "DELETE FROM pagamento WHERE id_pagamento=@id",
                    conn);

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}