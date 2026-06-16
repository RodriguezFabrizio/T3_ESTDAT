using EXAMEN_T3;
using System;

internal class Program
{
    
    static Carro[] flota = new Carro[100];
    static int numCarros = 0;
    static GrafoNDP grafo = new GrafoNDP();

    static void Main()
    {
        

        bool salir = false;
        while (!salir)
        {
            Console.WriteLine("\n=== MENÚ DE OPCIONES ===");
            Console.WriteLine("1 - Agrega Carro");
            Console.WriteLine("2 - Modifica Tipo Carro");
            Console.WriteLine("3 - Agrega Arista");
            Console.WriteLine("4 - Asignar Ubicaciones a Taxis");
            Console.WriteLine("5 - Busca Taxis Cercanos");
            Console.WriteLine("6 - Distancia entre 2 puntos");
            Console.WriteLine("7 - Tomar Taxi");
            Console.WriteLine("9 - Fin");
            Console.Write("Elija una opción: ");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1": AgregaCarro(); break;
                case "2": ModificaTipoCarro(); break;
                case "3": AgregaArista(); break;
                case "4": AsignarUbicaciones(); break;
                case "5": BuscaTaxisCercanos(); break;
                case "6": DistanciaEntre2Puntos(); break;
                case "7": TomarTaxi(); break;
                case "9": salir = true; break;
                default: Console.WriteLine("Ingrese un número ( 1 - 9 ) vuelva a intentarlo."); break;
            }
        }
    }

    static void AgregaCarro()
    {
        if (numCarros >= flota.Length)
        {
            Console.WriteLine("Lleno.");
            return;
        }

        Carro c = new Carro();
        Console.Write("Placa: "); c.Placa = Console.ReadLine();
        Console.Write("Color: "); c.Color = Console.ReadLine();

        Console.Write("Tipo (1.- para Particular, 2.- para Taxi): ");
        string t = Console.ReadLine();

        if (t == "2")
        {
            c.Tipo = "Taxi";
            Console.Write("Soles por minuto: ");
            c.SolesXMin = Convert.ToDouble(Console.ReadLine());
            c.Vertice = grafo.ObtenerVerticeAlAzar();
            if (c.Vertice == -1) Console.WriteLine("No hay vertices.");
        }
        else
        {
            c.Tipo = "Particular";
            c.SolesXMin = 0;
            c.Vertice = -1; 
        }

        flota[numCarros] = c;
        numCarros++;
        Console.WriteLine("Se agrego el Carro");
    }

    static void ModificaTipoCarro()
    {
        Console.Write("Ingrese la placa del carro que desea cambiar: ");
        string placa = Console.ReadLine();

        for (int i = 0; i < numCarros; i++)
        {
            if (flota[i].Placa == placa)
            {
                Console.WriteLine($"Actual es: {flota[i].Tipo}");
                Console.Write("Nuevo Tipo (1 para Particular, 2 para Taxi): ");
                string t = Console.ReadLine();

                if (t == "2")
                {
                    flota[i].Tipo = "Taxi";
                    Console.Write("Soles por minuto: ");
                    flota[i].SolesXMin = Convert.ToDouble(Console.ReadLine());
                    flota[i].Vertice = grafo.ObtenerVerticeAlAzar();
                }
                else
                {
                    flota[i].Tipo = "Particular";
                    flota[i].SolesXMin = 0;
                    flota[i].Vertice = -1;
                }
                Console.WriteLine("Se han hecho los cambios.");
                return;
            }
        }
        Console.WriteLine("No se encontro el Carro.");
    }

    static void AgregaArista()
    {
        Console.Write("Vértice Número 1: "); int u = Convert.ToInt32(Console.ReadLine());
        Console.Write("Vértice Numero 2: "); int v = Convert.ToInt32(Console.ReadLine());
        Console.Write("Distancia (km): "); double d = Convert.ToDouble(Console.ReadLine());
        Console.Write("Tiempo (min): "); double t = Convert.ToDouble(Console.ReadLine());

        grafo.AgregarArista(u, v, d, t);
    }

    static void AsignarUbicaciones()
    {
        int asignados = 0;
        for (int i = 0; i < numCarros; i++)
        {
            if (flota[i].Tipo == "Taxi")
            {
                flota[i].Vertice = grafo.ObtenerVerticeAlAzar();
                asignados++;
            }
        }
        Console.WriteLine($"Se asignaron ubicaciones a {asignados} taxis.");
    }

    static void BuscaTaxisCercanos()
    {
        Console.Write("Ingrese un vértice actual: ");
        int miVertice = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Los Taxis cercanos (hasta 2 niveles):");
        bool hayTaxis = false;

        for (int i = 0; i < numCarros; i++)
        {
            if (flota[i].Tipo == "Taxi" && flota[i].Vertice != -1)
            {
                double d = -1, t = -1;
                int[] visitados = new int[10];
                bool conectado = grafo.BuscarCamino(miVertice, flota[i].Vertice, 0, 2, 0, 0, ref d, ref t, visitados, 0);

                if (conectado)
                {
                    Console.WriteLine($"- Taxi Placa: {flota[i].Placa}, Ubicado en: {flota[i].Vertice}");
                    hayTaxis = true;
                }
            }
        }
        if (!hayTaxis) Console.WriteLine("No se encontraron taxis cerca.");
    }

    static void DistanciaEntre2Puntos()
    {
        Console.Write("Vértice A: "); int a = Convert.ToInt32(Console.ReadLine());
        Console.Write("Vértice B: "); int b = Convert.ToInt32(Console.ReadLine());

        double distMejor = -1, tiempoMejor = -1;
        int[] visitados = new int[20];

       
        bool conectado = grafo.BuscarCamino(a, b, 0, 3, 0, 0, ref distMejor, ref tiempoMejor, visitados, 0);

        if (conectado)
        {
            Console.WriteLine($"Distancia total: {distMejor} km. Tiempo: {tiempoMejor} min.");
        }
        else
        {
            Console.WriteLine("No hay conexión entre estos puntos (hasta 3 niveles de distancia).");
        }
    }

    static void TomarTaxi()
    {
        Console.Write("Vértice Origen: "); int origen = Convert.ToInt32(Console.ReadLine());
        Console.Write("Vértice Destino: "); int destino = Convert.ToInt32(Console.ReadLine());

        double distCarrera = -1, tiempoCarrera = -1;
        int[] vCarrera = new int[20];

        bool carreraViable = grafo.BuscarCamino(origen, destino, 0, 3, 0, 0, ref distCarrera, ref tiempoCarrera, vCarrera, 0);

        if (!carreraViable)
        {
            Console.WriteLine("Se cancelo. El destino está a más de 3 niveles de distancia.");
            return;
        }

        Console.WriteLine("\n--- Taxis Disponibles (hasta 2 niveles de tu origen) ---");
        int countTaxis = 0;
        int[] indexDisponibles = new int[100]; 

        for (int i = 0; i < numCarros; i++)
        {
            if (flota[i].Tipo == "Taxi" && flota[i].Vertice != -1)
            {
                double dTaxiAOrigen = -1, tTaxiAOrigen = -1;
                int[] vTaxi = new int[10];
                bool taxiCerca = grafo.BuscarCamino(flota[i].Vertice, origen, 0, 2, 0, 0, ref dTaxiAOrigen, ref tTaxiAOrigen, vTaxi, 0);

                if (taxiCerca)
                {
                    double costo = tiempoCarrera * flota[i].SolesXMin;
                    Console.WriteLine($"[{i}] Placa: {flota[i].Placa} / Tardará en llegar al origen: {tTaxiAOrigen} min / Llegada al destino: {tiempoCarrera} min / Costo: S/. {costo}");
                    indexDisponibles[countTaxis] = i;
                    countTaxis++;
                }
            }
        }

        if (countTaxis == 0)
        {
            Console.WriteLine("No hay taxis cercanos. Intente de nuevo");
            return;
        }

        Console.Write("\nIngrese el número de la opción elegida (entre corchetes): ");
        int eleccion = Convert.ToInt32(Console.ReadLine());

        
        bool valido = false;
        for (int j = 0; j < countTaxis; j++)
        {
            if (indexDisponibles[j] == eleccion) valido = true;
        }

        if (valido)
        {
            double costoFinal = tiempoCarrera * flota[eleccion].SolesXMin;
            Console.WriteLine($"\nTaxi {flota[eleccion].Placa} en camino");
            Console.WriteLine($"Yapear monto total: S/. {costoFinal} al llegar al destino");
            flota[eleccion].Vertice = destino;
        }
        else
        {
            Console.WriteLine("Error.");
        }
    }
}