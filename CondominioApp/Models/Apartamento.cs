namespace CondominioApp.Models
{
    public class Apartamento
    {
        public int IdApartamento { get; set; }
        public string Bloco { get; set; }
        public int Piso { get; set; }
        public string Numero { get; set; }

        public string Display => $"Bloco {Bloco} — Piso {Piso} — Nº {Numero}";

        // Representação legível — aparece automaticamente em ComboBoxes e listas
        public override string ToString()
        {
            return Display;
        }
    }
}