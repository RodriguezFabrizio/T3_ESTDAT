using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMEN_T3
{
    internal class GrafoNDP
    {
        public NodoVertice Primero { get; set; }
        private Random rnd = new Random();

        // Obtiene un vértice si existe, sino lo crea
        public NodoVertice ObtenerOAgregarVertice(int id)
        {
            if (Primero == null)
            {
                Primero = new NodoVertice { Id = id };
                return Primero;
            }

            NodoVertice actual = Primero;
            while (actual != null)
            {
                if (actual.Id == id) return actual;
                if (actual.Siguiente == null) break;
                actual = actual.Siguiente;
            }
            NodoVertice nuevo = new NodoVertice { Id = id };
            actual.Siguiente = nuevo;
            return nuevo;
        }

        public NodoVertice ObtenerVertice(int id)
        {
            NodoVertice actual = Primero;
            while (actual != null)
            {
                if (actual.Id == id) return actual;
                actual = actual.Siguiente;
            }
            return null;
        }

        public bool ExisteArista(NodoVertice origen, int destinoId)
        {
            NodoArista actual = origen.PrimerArista;
            while (actual != null)
            {
                if (actual.Destino == destinoId) return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        public void AgregarArista(int ari, int ver, double distancia, double tiempo)
        {
            NodoVertice Nari = ObtenerOAgregarVertice(ari);
            NodoVertice Nver = ObtenerOAgregarVertice(ver);

            // Validación: Solo agregar si no están conectados
            if (!ExisteArista(Nari, ver))
            {
                AgregarAristaInterna(Nari, ver, distancia, tiempo);
                AgregarAristaInterna(Nver, ari, distancia, tiempo); // No dirigido
                Console.WriteLine($"Arista creada entre {ari} y {ver}.");
            }
            else
            {
                Console.WriteLine("Error: Estos vértices ya están conectados.");
            }
        }

        private void AgregarAristaInterna(NodoVertice origen, int Destino, double distancia, double tiempo)
        {
            NodoArista nueva = new NodoArista(Destino, distancia, tiempo);
            if (origen.PrimerArista == null)
            {
                origen.PrimerArista = nueva;
            }
            else
            {
                NodoArista actual = origen.PrimerArista;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nueva;
            }
        }

        public int ObtenerVerticeAlAzar()
        {
            if (Primero == null) return -1;

            int contador = 0;
            NodoVertice actual = Primero;
            while (actual != null)
            {
                contador++;
                actual = actual.Siguiente;
            }

            int indiceElegido = rnd.Next(0, contador);
            actual = Primero;
            for (int i = 0; i < indiceElegido; i++)
            {
                actual = actual.Siguiente;
            }
            return actual.Id;
        }

        public bool BuscarCamino(int actual, int destino, int nivelActual, int nivelMax, double distAcumulada, double tiempoAcumulado, ref double mejorDistancia, ref double mejorTiempo, int[] visitados, int numVisitados)
        {
            if (actual == destino)
            {
                if (mejorDistancia == -1 || distAcumulada < mejorDistancia)
                {
                    mejorDistancia = distAcumulada;
                    mejorTiempo = tiempoAcumulado;
                }
                return true;
            }

            if (nivelActual == nivelMax) return false;

            visitados[numVisitados] = actual;
            numVisitados++;

            NodoVertice vNode = ObtenerVertice(actual);
            if (vNode == null) return false;

            bool encontrado = false;
            NodoArista aNode = vNode.PrimerArista;

            while (aNode != null)
            {
                // Verificar si el destino de la arista ya fue visitado en este camino
                bool yaVisitado = false;
                for (int i = 0; i < numVisitados; i++)
                {
                    if (visitados[i] == aNode.Destino) yaVisitado = true;
                }

                if (!yaVisitado)
                {
                    bool exito = BuscarCamino(aNode.Destino, destino, nivelActual + 1, nivelMax, distAcumulada + aNode.Distancia, tiempoAcumulado + aNode.Tiempo, ref mejorDistancia, ref mejorTiempo, visitados, numVisitados);
                    if (exito) encontrado = true;
                }
                aNode = aNode.Siguiente;
            }
            return encontrado;
        }
    }
}

