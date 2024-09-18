using System;
using System.Collections.Generic;
using System.Linq;

namespace BatallaDeCartas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido a la Batalla de Cartas.");

            int numeroJugadores = 0;
            while (numeroJugadores < 2 || numeroJugadores > 5)
            {
                Console.Write("Ingrese el número de jugadores (2-5): ");
                int.TryParse(Console.ReadLine(), out numeroJugadores);
            }

            List<Jugador> jugadores = new List<Jugador>();

            // Crear jugadores automáticamente
            for (int i = 0; i < numeroJugadores; i++)
            {
                string nombre = $"Jugador {i + 1}";
                jugadores.Add(new Jugador(nombre));
            }

            // Crear y barajar la baraja
            Baraja baraja = new Baraja();
            baraja.BarajarCartas();

            // Mostrar el número de cartas en la baraja
            Console.WriteLine($"\nLa baraja tiene {baraja.CartasRestantes()} cartas antes de repartir.");

            // Calcular el número de cartas a repartir a cada jugador
            int cartasPorJugador = baraja.CartasRestantes() / numeroJugadores;
            int totalCartasARepartir = cartasPorJugador * numeroJugadores;

            // Descartar las cartas sobrantes
            int cartasASacar = baraja.CartasRestantes() - totalCartasARepartir;
            for (int i = 0; i < cartasASacar; i++)
            {
                baraja.RobarCarta(); // Descartar la carta
            }

            Console.WriteLine($"Se han repartido {totalCartasARepartir} cartas entre los jugadores.");
            Console.WriteLine($"Se han descartado {cartasASacar} cartas sobrantes.");

            // Repartir las cartas entre los jugadores
            for (int i = 0; i < cartasPorJugador; i++)
            {
                foreach (var jugador in jugadores)
                {
                    jugador.RecibirCarta(baraja.RobarCarta());
                }
            }

            Console.WriteLine("\n¡Comienza el juego!");

            // Contador de rondas
            int contadorRondas = 0;

            // Bucle principal del juego
            while (jugadores.Count(j => j.TieneCartas()) > 1)
            {
                contadorRondas++;
                Console.WriteLine($"\n--- Ronda {contadorRondas} ---");

                // Lista para acumular las cartas en juego (incluyendo las de desempates)
                List<Carta> cartasEnJuego = new List<Carta>();
                // Lista de jugadores que participarán en la ronda actual
                List<Jugador> jugadoresEnRonda = jugadores.Where(j => j.TieneCartas()).ToList();

                bool hayGanadorRonda = false;
                List<Jugador> empatados = new List<Jugador>();

                do
                {
                    List<(Jugador jugador, Carta carta)> cartasJugadas = new List<(Jugador, Carta)>();

                    // Cada jugador en la ronda juega una carta
                    foreach (var jugador in jugadoresEnRonda)
                    {
                        if (jugador.TieneCartas())
                        {
                            Carta cartaJugada = jugador.JugarCarta();
                            cartasJugadas.Add((jugador, cartaJugada));
                            cartasEnJuego.Add(cartaJugada);
                            Console.WriteLine($"{jugador.Nombre} juega {cartaJugada}");
                        }
                        else
                        {
                            Console.WriteLine($"{jugador.Nombre} no tiene más cartas y es eliminado.");
                        }
                    }

                    // Determinar el valor máximo de las cartas jugadas
                    var valorMaximo = cartasJugadas.Max(c => (int)c.carta.Valor);
                    // Obtener los jugadores que empataron con el valor máximo
                    empatados = cartasJugadas.Where(c => (int)c.carta.Valor == valorMaximo).Select(c => c.jugador).ToList();

                    if (empatados.Count == 1)
                    {
                        // Un solo ganador
                        var ganadorRonda = empatados.First();
                        Console.WriteLine($"\n{ganadorRonda.Nombre} gana la ronda y se lleva {cartasEnJuego.Count} cartas.");
                        ganadorRonda.RecibirCartasGanadas(cartasEnJuego);
                        hayGanadorRonda = true;
                    }
                    else
                    {
                        // Empate entre jugadores
                        Console.WriteLine("\nEmpate entre jugadores:");
                        foreach (var jugador in empatados)
                        {
                            Console.WriteLine(jugador.Nombre);
                        }

                        // Verificar si los jugadores empatados tienen suficientes cartas para continuar
                        if (empatados.All(j => j.TieneCartas()))
                        {
                            Console.WriteLine("¡Ronda de desempate!");
                            // Solo los jugadores empatados continúan en la siguiente ronda
                            jugadoresEnRonda = empatados;
                        }
                        else
                        {
                            // Si algún jugador empatado no tiene cartas, el desempate no puede continuar
                            Console.WriteLine("No todos los jugadores empatados tienen cartas para continuar el desempate.");
                            hayGanadorRonda = true; // Finalizar la ronda actual
                        }
                    }

                } while (!hayGanadorRonda);

                // Mostrar el estado de cada jugador
                foreach (var jugador in jugadores)
                {
                    Console.WriteLine(jugador);
                }

                // Eliminar jugadores sin cartas
                jugadores = jugadores.Where(j => j.TieneCartas()).ToList();
            }

            // Anunciar el ganador
            var ganadorFinal = jugadores.FirstOrDefault();
            if (ganadorFinal != null)
            {
                Console.WriteLine($"\n¡{ganadorFinal.Nombre} ha ganado el juego!");
            }
            else
            {
                Console.WriteLine("\nTodos los jugadores han perdido.");
            }

            // Mostrar el número total de rondas jugadas
            Console.WriteLine($"\nEl juego ha terminado después de {contadorRondas} rondas.");

            Console.WriteLine("\nFin del juego. Presiona cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}
