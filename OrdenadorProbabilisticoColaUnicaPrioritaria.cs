using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioDeColas
{
    class OrdenadorProbabilisticoColaUnicaPrioritaria
    {
        private readonly Random _rnd = new();

        private readonly Dictionary<int, double> _probabilidadesAvance = new()
        {
            { 1, 0.60 }, // Prioridad alta
            { 2, 0.30 }, // Prioridad media
            { 3, 0.10 }  // Prioridad baja
        };

        public List<Paciente> Reordenar(List<Paciente> cola)
        {
            // Evitar modificar la lista original directamente
            var copiaCola = new List<Paciente>(cola);
            bool huboCambio = false;


            for (int i = 1; i < copiaCola.Count; i++)
            {
                var paciente = copiaCola[i];
                double chance = _probabilidadesAvance[(int)paciente.PesoPrioridad];

                if (_rnd.NextDouble() < chance)
                {
                    int nuevaPos = i - 1;
                    Console.WriteLine($"Paciente {paciente.Nombres} {paciente.Apellidos} (Prioridad {paciente.PesoPrioridad}) avanza a la posición {nuevaPos}");
                    // No adelanta a personas de prioridad superior
                    if (nuevaPos >= 0 && copiaCola[nuevaPos].PesoPrioridad >= paciente.PesoPrioridad)
                    {
                        copiaCola.RemoveAt(i);
                        copiaCola.Insert(nuevaPos, paciente);
                        huboCambio = true;
                    }
                }
                else
                {
                    Console.WriteLine($"Paciente {paciente.Nombres} {paciente.Apellidos} (Prioridad {paciente.PesoPrioridad}) no avanza");
                }

                // Simulamos envejecimiento
                paciente.CiclosCola++;
            }

            if (huboCambio)
            {
                Console.WriteLine(" Cola original:");
                for (int i = 0; i < cola.Count; i++)
                    Console.WriteLine($"Pos {i}: {cola[i]}");

                Console.WriteLine("\n Cola modificada:");
                for (int i = 0; i < copiaCola.Count; i++)
                    Console.WriteLine($"Pos {i}: {copiaCola[i]}");

                Console.WriteLine("\n---\n");
            }
            else
            {
                Console.WriteLine("No hubo cambios en la cola.");
            }

            return copiaCola;
        }

        public void AjustarProbabilidad(int prioridad, double nuevaProbabilidad)
        {
            if (_probabilidadesAvance.ContainsKey(prioridad))
            {
                _probabilidadesAvance[prioridad] = nuevaProbabilidad;
            }
        }

        public Dictionary<int, double> VerProbabilidades()
        {
            return new Dictionary<int, double>(_probabilidadesAvance);
        }
    }
}
