using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuvar.Model
{
    public class Utazas
    {
        public int Taxi_id { get; }
        public DateTime Indulas { get; }
        public int Idotartam { get; }
        
        public double Tavolsag { get; }
        public double Viteldij { get; }
        public double Borravalo { get; }
        public string Fizetes_modja { get; }

        public Utazas(int taxi_id, DateTime indulas, int idotartam, double tavolsag, double viteldij, double borravalo, string fizetes_modja)
        {
            Taxi_id = taxi_id;
            Indulas = indulas;
            Idotartam = idotartam;
            Tavolsag = tavolsag;
            Viteldij = viteldij;
            Borravalo = borravalo;
            Fizetes_modja = fizetes_modja;
        }
    }
}
