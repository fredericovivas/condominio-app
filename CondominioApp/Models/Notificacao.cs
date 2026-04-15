using System;

namespace CondominioApp.Models
{
    public class Notificacao
    {
        public int IdNotificacao { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataEnvio { get; set; }
        public int IdCondomino { get; set; }

        // Objeto do condómino associado (preenchido quando necessário)
        public Condomino Condomino { get; set; }
    }
}