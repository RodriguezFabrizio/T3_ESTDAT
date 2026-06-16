using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXAMEN_T3
{
   

    internal class Carro
    {
       
        public Carro()
        {
        }

        
        public Carro(string placa, string color, string tipo, double solesXMin, int vertice)
        {
            Placa = placa;
            Color = color;
            Tipo = tipo;
            SolesXMin = solesXMin;
            Vertice = vertice;
        }

        public string Placa;
        public string Color;
        public string Tipo; 
        public double SolesXMin;
        public int Vertice;
    }
}
