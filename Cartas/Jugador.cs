using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatallaDeCartas
{
    // Clase Jugador
    public class Jugador
    {
        public string Nombre { get; private set; }
        private Queue<Carta> mano;

        // Constructor
        public Jugador(string nombre)
        {
            Nombre = nombre;
            mano = new Queue<Carta>();
        }

        // Método para recibir una carta
        public void RecibirCarta(Carta carta)
        {
            mano.Enqueue(carta);
        }

        // Método para jugar la primera carta de la mano
        public Carta JugarCarta()
        {
            if (mano.Count > 0)
            {
                return mano.Dequeue();
            }
            else
            {
                return null;
            }
        }

        // Método para recibir las cartas ganadas
        public void RecibirCartasGanadas(IEnumerable<Carta> cartas)
        {
            foreach (var carta in cartas)
            {
                mano.Enqueue(carta);
            }
        }

        // Verificar si el jugador tiene cartas
        public bool TieneCartas()
        {
            return mano.Count > 0;
        }

        // Obtener el número de cartas en la mano
        public int NumeroDeCartas()
        {
            return mano.Count;
        }

        // Método ToString() para mostrar el estado del jugador
        public override string ToString()
        {
            return $"{Nombre} tiene {mano.Count} cartas.";
        }
    }
}
