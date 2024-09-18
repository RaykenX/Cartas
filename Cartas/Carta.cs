using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaDeCartas
{
    // Clase Carta
    public class Carta
    {
        // Enumeración de Palos
        public enum ePalo
        {
            Corazones,
            Diamantes,
            Tréboles,
            Picas
        }

        // Enumeración de Valores de Carta
        public enum ValorCarta
        {
            Dos = 2,
            Tres,
            Cuatro,
            Cinco,
            Seis,
            Siete,
            Ocho,
            Nueve,
            Diez,
            Jota,
            Reina,
            Rey,
            As
        }

        // Propiedades
        public ePalo Palo { get; private set; }
        public ValorCarta Valor { get; private set; }

        // Constructor
        public Carta(ePalo palo, ValorCarta valor)
        {
            Palo = palo;
            Valor = valor;
        }

        // Método ToString() para representar la carta como texto
        public override string ToString()
        {
            return $"{Valor} de {Palo}";
        }
    }
}
