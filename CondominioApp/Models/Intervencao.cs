using System;

namespace CondominioApp.Models
{
    public class Intervencao
    {
        public int IdIntervencao { get; set; }
        public string Descricao { get; set; }
        public decimal? Custo { get; set; }  // nullable — pode não ter custo definido
        public DateTime Data { get; set; }
        public int IdEvento { get; set; }

        // Objeto do evento associado (preenchido quando necessário)
        public Evento Evento { get; set; }
    }
}