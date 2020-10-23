using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GatoRaton
{
    public class Program 
    {

        private static void ValidarNumerosJugables(Cls_Jugador jugador)
        {
            string dato;
            bool estadoformatoingreso;
            string sentenciaregex = @"^[0-9]+$";

            ObtenerNumero(jugador, out dato, out estadoformatoingreso, sentenciaregex);

            while (!jugador.VerificarNumero() || !jugador.VerificarListanumeroUsuado() || !estadoformatoingreso)
            {
                ObtenerNumero(jugador, out dato, out estadoformatoingreso, sentenciaregex);
            }
        }
        private static void ObtenerNumero(Cls_Jugador jugador, out string dato, out bool estadoformatoingreso, string sentenciaregex)
        {
            Console.WriteLine(string.Format("Ingrese un numero de las siguientes opciones {0}.", string.Join(", ", jugador.ListaNumeroDisponible)));
            VerficarFormatoIngreso(sentenciaregex, out dato, out estadoformatoingreso);
            if (estadoformatoingreso)
            {
                jugador.Numero = Convert.ToInt32(dato);
            }
        }
        private static void VerficarFormatoIngreso(string sentenciaregex, out string dato, out bool estadoformatoingreso)
        {
            Regex validaringresonumero = new Regex(sentenciaregex);
            dato = Console.ReadLine();
            estadoformatoingreso = validaringresonumero.IsMatch(dato);
        }
        private static void ComenzarJuego(Cls_Jugador jugador)
        {
            Console.WriteLine($"El jugador {jugador.TipoJugador} turno actual {jugador.Posicion + 1} tiene un puntaje de {jugador.PuntajeAcumulado()}");
            ValidarNumerosJugables(jugador);
        }
        private static void IniciarPrimerMovimiento(Cls_Jugador jugador)
        {
            jugador.OperacionMatematica();
            DarMensajeActual(jugador);
        }
        private static void JugarOperacionMatematica(Cls_Jugador jugador)
        {
            if (jugador.Posicion == 0)
            {
                List<string> nombreoperacion = new List<string>(2) { "Suma", "Resta" };
                Console.WriteLine($"Su operacion inicial fue una {nombreoperacion[jugador.TipoOperacion - 1]}");
                IniciarPrimerMovimiento(jugador);
            }
            else
            {
                jugador.TipoOperacion = ValidarOperacionSeleccionada(jugador);
                jugador.Mensaje = jugador.OperacionMatematica();
                LimitarPuntaje(jugador);
                Console.WriteLine(jugador.Mensaje);
                DarMensajeActual(jugador);
            }
        }
        private static void DarMensajeActual(Cls_Jugador jugador)
        {
            Console.WriteLine($"{jugador.TipoJugador} su puntaje actual es {jugador.PuntajeAcumulado()}");
        }
        private static void LimitarPuntaje(Cls_Jugador jugador)
        {
            while (!jugador.VerificarPuntajeAcumulado())
            {
                Console.WriteLine("No puede superar los limites del juego que no puede ser mayor a 30 ni menor a 0");
                ValidarNumerosJugables(jugador);

                jugador.TipoOperacion = ValidarOperacionSeleccionada(jugador);
                jugador.Mensaje = jugador.OperacionMatematica();
            }
        }
        private static int ValidarOperacionSeleccionada(Cls_Jugador jugador)
        {
            string dato;
            bool estadoformatoingreso;
            string sentenciaregex = @"^[1-2]+$";
            ValidarOpcionOperacion(out dato, out estadoformatoingreso, sentenciaregex);

            while (!estadoformatoingreso || !jugador.VerificarComando())
            {
                ValidarOpcionOperacion(out dato, out estadoformatoingreso, sentenciaregex);
            }
            jugador.TipoOperacion = Convert.ToInt32(dato);
            return jugador.TipoOperacion;
        }
        private static void ValidarOpcionOperacion(out string dato, out bool estadoformatoingreso, string sentenciaregex)
        {
            Console.WriteLine("Debe Ingresar 1 para suma o 2 para resta");
            VerficarFormatoIngreso(sentenciaregex, out dato, out estadoformatoingreso);
        }
        private static void DarMensajeGanador(Cls_Jugador jugador)
        {
            Console.WriteLine($"El {jugador.TipoJugador} gano la partida, en el turno {jugador.Posicion + 1}");
        }

        static void Main(string[] args)
        {
            string dato;
            bool estadoformatoingreso;
            string sentenciaregex = @"^[1-2]+$";
            int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] numerosusados = { };


            Console.WriteLine("Debe Ingresar 1 para Empezar el juego o 2 para Salir");
            VerficarFormatoIngreso(sentenciaregex, out dato, out estadoformatoingreso);

            if (dato.Contains("1") && estadoformatoingreso)
            {
                while (estadoformatoingreso)
                {
                    int accion = Convert.ToInt32(dato);
                    while (accion == 1)
                    {
                        Cls_Jugador jugador1 = new Cls_Jugador();
                        jugador1.TipoJugador = "Ratón";
                        jugador1.ListaNumeroDisponible = new List<int>(numeros);
                        jugador1.ListaNumeroUsado = new List<int>(numerosusados);
                        jugador1.Posicion = 0;
                        jugador1.TipoOperacion = 2;
                        jugador1.Mensaje = string.Empty;
                        jugador1.Puntaje = 30;

                        Cls_Jugador jugador2 = new Cls_Jugador();
                        jugador2.TipoJugador = "Gato";
                        jugador2.ListaNumeroDisponible = new List<int>(numeros);
                        jugador2.ListaNumeroUsado = new List<int>(numerosusados);
                        jugador2.Posicion = 0;
                        jugador2.TipoOperacion = 1;
                        jugador2.Mensaje = string.Empty;
                        jugador2.Puntaje = 1;

                        while (jugador1.Posicion < 9 && jugador2.Posicion < 9)
                        {
                            #region Jugador 1
                            ComenzarJuego(jugador1);
                            JugarOperacionMatematica(jugador1);
                            if (jugador1.PuntajeAcumulado() == 0)
                            {
                                DarMensajeGanador(jugador1);
                                break;
                            }
                            #endregion

                            #region Jugador 2
                            ComenzarJuego(jugador2);
                            JugarOperacionMatematica(jugador2);
                            if (jugador1.PuntajeAcumulado() == jugador2.PuntajeAcumulado())
                            {
                                DarMensajeGanador(jugador2);
                                break;
                            }
                            #endregion

                            if (jugador1.Posicion == 8 && jugador2.Posicion == 8)
                            {
                                Console.WriteLine("El juego quedo en empate");
                                break;
                            }
                            jugador1.Posicion += 1;
                            jugador2.Posicion += 1;
                        }
                        break;
                    }


                    Console.WriteLine("Debe Ingresar 1 para Empezar el juego o 2 para Salir");
                    VerficarFormatoIngreso(sentenciaregex, out dato, out estadoformatoingreso);

                    if (estadoformatoingreso)
                    {
                        accion = Convert.ToInt32(dato);
                        if (accion == 2)
                        {
                            break;
                        }
                    }
                }
            }
            Console.WriteLine("Termino el juego, presione cualquier tecla para cerrar...");
            Console.ReadLine();
        }
    }
}
