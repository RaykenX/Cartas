using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaDeCartas
{
    // Clase Baraja
    public class Baraja
    {
        private List<Carta> cartas;
        private Random random;

        // Constructor que crea y llena la baraja
        public Baraja()
        {
            cartas = new List<Carta>();
            random = new Random();

            // Agregar las 52 cartas estándar
            foreach (Carta.ePalo palo in Enum.GetValues(typeof(Carta.ePalo)))
            {
                foreach (Carta.ValorCarta valor in Enum.GetValues(typeof(Carta.ValorCarta)))
                {
                    cartas.Add(new Carta(palo, valor));
                }
            }
        }

        // Método para barajar las cartas
        public void BarajarCartas()
        {
            cartas = cartas.OrderBy(c => random.Next()).ToList();
        }

        // Método para robar una carta de la baraja
        public Carta RobarCarta()
        {
            if (cartas.Count > 0)
            {
                Carta carta = cartas[0];
                cartas.RemoveAt(0);
                return carta;
            }
            else
            {
                return null;
            }
        }

        // Verificar si la baraja está vacía
        public bool EstaVacia()
        {
            return cartas.Count == 0;
        }

        // Obtener el número de cartas restantes
        public int CartasRestantes()
        {
            return cartas.Count;
        }

        // Método ToString() para mostrar todas las cartas en la baraja
        public override string ToString()
        {
            return string.Join(", ", cartas);
        }
    }
}
