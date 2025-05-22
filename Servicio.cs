using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LaboratorioDeColas
{
    class Servicio
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Costo { get; set; }
        public int TiempoEstimado { get; set; } // en minutos
        public int TiempoReal { get; set; } // en minutos
        public bool Estado { get; set; } // true = activo, false = inactivo
        public double Peso { get; set; } // prioridad del servicio entre 0 y 1

        public Servicio(int id, string nombre, string descripcion, double costo, int tiempoEstimado, double peso)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Descripcion = descripcion;
            this.Costo = costo;
            this.TiempoEstimado = tiempoEstimado;
            this.TiempoReal = 0;
            this.Estado = true;
            this.Peso = peso;

        }
        public Servicio(string nombre, double peso)
        {
            this.Nombre = nombre;
            this.Peso = peso;
            this.Estado = true;
            this.TiempoReal = 0;
            this.TiempoEstimado = 0;
            this.Costo = 0;
        }
    }
}
