using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMEN_T3
{
    internal class NodoArista
    {
        public NodoArista(int destino, double distancia, double tiempo)
        {
            Destino = destino;
            Distancia = distancia;
            Tiempo = tiempo;
            Siguiente = null;
        }
        public int Destino { get; set; }
        public double Distancia { get; set; }
        public double Tiempo { get; set; }
        public NodoArista Siguiente { get; set; }
    }
}
