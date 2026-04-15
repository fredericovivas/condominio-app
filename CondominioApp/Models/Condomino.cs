namespace CondominioApp.Models
{
    public class Condomino
    {
        public int IdCondomino { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int IdApartamento { get; set; }

        // Objeto do apartamento associado (preenchido quando necessário)
        public Apartamento Apartamento { get; set; }

        public override string ToString()
        {
            return Nome;
        }
    }
}