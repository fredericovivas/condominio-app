using System;

namespace CondominioApp.Models
{
    public enum EstadoPagamento
    {
        Pago,
        EmDivida
    }

    public class Pagamento
    {
        public int IdPagamento { get; set; }
        public decimal Valor { get; set; }
        public EstadoPagamento Estado { get; set; }
        public DateTime DataRegisto { get; set; }
        public int IdCondomino { get; set; }
        public int IdEvento { get; set; }

        // Objetos associados (preenchidos quando necessário)
        public Condomino Condomino { get; set; }
        public Evento Evento { get; set; }
    }
}