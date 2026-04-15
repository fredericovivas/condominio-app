using System;

namespace CondominioApp.Models
{
    // Tipos possíveis de evento — correspondem aos valores no MySQL ENUM
    public enum TipoEvento
    {
        Reuniao,
        Manutencao,
        PrazoPagamento
    }

    public class Evento
    {
        public int IdEvento { get; set; }
        public string Titulo { get; set; }
        public TipoEvento Tipo { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return $"{Data:dd/MM/yyyy} — {Titulo}";
        }
    }
}