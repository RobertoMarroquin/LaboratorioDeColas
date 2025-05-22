using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioDeColas
{
    class Paciente
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public string Sexo { get; set; }
        public string TipoDeSangre { get; set; }
        public DateTime FechaDeIngreso { get; set; }
        public double PesoPrioridad { get; set; } // prioridad del paciente entre 1,2 o 3
                                                  // Poosteriorimente se hara a traves de pesos
                                                  // entre cero y 1 y se ordenaran a partir de
                                                  // un numero de caracteristicas que sumaran pesos
                                                  // por el momento solo se hara con base en la edad

        public int CiclosCola { get; set; } // ciclos en la cola 

        public List <Servicio> Servicios { get; set; } // servicio al que se asigna el paciente
        public Paciente(string nombres, string apellidos, int edad, string sexo, string tipoDeSangre)
        {
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.Edad = edad;
            this.Sexo = sexo;
            this.TipoDeSangre = tipoDeSangre;
            this.FechaDeIngreso = DateTime.Now;
            this.CiclosCola = 0;

            if (this.Edad > 0) 
            {
                if (this.Edad < 18)
                {
                    this.PesoPrioridad = 2;
                }
                else if (this.Edad >= 18 && this.Edad <= 59)
                {
                    this.PesoPrioridad = 3;
                }
                else
                {
                    this.PesoPrioridad = 1;
                }
            }
            else
            {
                this.PesoPrioridad = 0;
            }

        }
    }
}
