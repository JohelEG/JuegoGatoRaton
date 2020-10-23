using System.Collections.Generic;

namespace GatoRaton
{
    public class Cls_Jugador
    {
        #region Atributos
        private int puntaje;
        private int posicion;
        private int numero;
        private int tipooperacion;
        private string mensaje;
        private string tipoJugador;
        private List<int> listaNumeroDisponible;
        private List<int> listaNumeroUsado;
        #endregion

        #region Propiedades
        public int Puntaje {
            get { return puntaje; }
            set { this.puntaje = value;  }
        }
        public int Posicion
        {
            get { return posicion; }
            set { this.posicion = value; }
        }
        public int Numero
        {
            get { return numero; }
            set { this.numero = value; }
        }
        public int TipoOperacion
        {
            get { return tipooperacion; }
            set { this.tipooperacion = value; }
        }
        public string TipoJugador
        {
            get { return tipoJugador; }
            set { this.tipoJugador = value; }
        }
        public string Mensaje
        {
            get { return mensaje; }
            set { this.mensaje = value; }
        }
        public List<int> ListaNumeroDisponible
        {
            get { return listaNumeroDisponible; }
            set { this.listaNumeroDisponible = value; }
        }
        public List<int> ListaNumeroUsado
        {
            get { return listaNumeroUsado; }
            set { this.listaNumeroUsado = value; }
        }
        #endregion

        #region Metodos
        public int PuntajeAcumulado()
        {
            return puntaje;
        }
        private void SumarPuntaje()
        {
            puntaje = puntaje + numero;
        }
        private void RestarPuntaje()
        {
            puntaje = puntaje - numero;
        }
        public bool VerificarNumero()
        {
            if(numero > 0 && numero < 10)
            {
                return true;
            }
            return false;
        }
        public bool VerificarListanumeroUsuado()
        {
            if (!listaNumeroUsado.Contains(numero))
            {
                listaNumeroUsado.Add(numero);
                listaNumeroDisponible.Remove(numero);
                return true;
            }
            return false;
        }
        public bool VerificarComando()
        {
            if(tipooperacion > 0 && tipooperacion < 3)
            {
                return true;
            }
            return false;
        }
        public bool VerificarPuntajeAcumulado()
        {
            if(puntaje < 0)
            {
                puntaje = puntaje + numero;
                listaNumeroDisponible.Add(numero);
                listaNumeroDisponible.Sort();
                listaNumeroUsado.Remove(numero);
                listaNumeroUsado.Sort();
                return false;
            }
            else if (puntaje   > 30)
            {
                puntaje = puntaje - numero;
                listaNumeroDisponible.Add(numero);
                listaNumeroDisponible.Sort();
                listaNumeroUsado.Remove(numero);
                listaNumeroUsado.Sort();
                return false;
            }
             return true;
        }
        public string OperacionMatematica()
        {
            if(tipooperacion is 1)
            {
                SumarPuntaje();
                return "Su operacion fue una suma";
            }
            else
            {
                RestarPuntaje();
                return "Su operacion fue una resta";
            }
        }
        #endregion
    }
}
