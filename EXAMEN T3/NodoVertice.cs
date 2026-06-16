using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMEN_T3
{
    internal class NodoVertice
    {
        public NodoVertice()
        {
        }

        public NodoVertice(int id, NodoArista primerArista)
        {
            Id = id;
            PrimerArista = primerArista;
            Siguiente = null;
        }

        public int Id { get; set; }
        public NodoArista PrimerArista { get; set; }
        public NodoVertice Siguiente { get; set; }
    }
}


