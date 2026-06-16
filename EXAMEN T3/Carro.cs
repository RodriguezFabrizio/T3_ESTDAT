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
        public string Placa { get; set; }
        public string Color {  get; set; }
        public string Tipo {  get; set; }
        public double SolesXMin {  get; set; }
        public int Vertice {  get; set; }
    }
}
