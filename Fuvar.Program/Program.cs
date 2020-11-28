using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Fuvar.Model;

namespace Fuvar.Program
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 2. feladat
            string forras = @"..\..\src\fuvar.csv";
            List<Utazas> fuvarLista = FajlBeolvas(forras);
            #endregion

            #region 3. feladat
            Console.Write("3. feladat: ");
            Console.WriteLine(fuvarLista.Count() + " fuvar");
            #endregion

            #region 4. feladat
            Console.Write("4. feladat: ");
            var fuvarokSzama = fuvarLista
                .Where(x => x.Taxi_id == 6185)
                .Count();
            var bevetel = fuvarLista
                .Where(x => x.Taxi_id == 6185)
                .Sum(x => x.Viteldij + x.Borravalo);
            Console.WriteLine($"{fuvarokSzama} fuvar alatt: {bevetel}$");
            #endregion

            #region 5. feladat
            Console.WriteLine("5. feladat: ");
            var fizetesiModok = fuvarLista
                .GroupBy(x => x.Fizetes_modja)
                .ToList();
            fizetesiModok.ForEach(x => Console.WriteLine($"\t{x.Key}: {x.Count()} fuvar"));
            #endregion

            #region 6. feladat
            Console.Write("6. feladat: ");
            var osszesUtKm = fuvarLista
                .Sum(x => x.Tavolsag * 1.6);
            double osszesUtKmKerekitve = Math.Round(osszesUtKm, 2);
            Console.WriteLine($"{osszesUtKmKerekitve} km");
            #endregion

            #region 7. feladat
            Console.WriteLine("7. feladat: Leghosszabb fuvar:");
            var leghosszabb = fuvarLista
                .OrderByDescending(x => x.Idotartam)
                .First();
            Console.WriteLine($"\tFuvar hossza: {leghosszabb.Idotartam} másodperc");
            Console.WriteLine($"\tTaxi azonosító: {leghosszabb.Taxi_id}");
            Console.WriteLine($"\tMegtett távolság: {leghosszabb.Tavolsag * 1.6} km");
            Console.WriteLine($"\tViteldíj: {leghosszabb.Viteldij} $");
            #endregion

            #region 8. feladat
            Console.Write("8. feladat: ");
            string fajl = "hibak.txt";
            HibakKiir(fuvarLista, fajl);
            Console.WriteLine(fajl);
            #endregion


            Console.ReadKey();
        }

        private static List<Utazas> FajlBeolvas(string forras)
        {
            List<Utazas> fuvarok = new List<Utazas>();
            using (StreamReader sr = new StreamReader(forras, Encoding.UTF8))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    var sor = sr.ReadLine().Split(';');
                    fuvarok.Add(new Utazas(
                        Convert.ToInt32(sor[0]),
                        Convert.ToDateTime(sor[1]),
                        Convert.ToInt32(sor[2]),
                        Convert.ToDouble(sor[3]),
                        Convert.ToDouble(sor[4]),
                        Convert.ToDouble(sor[5]),
                        sor[6]
                        ));
                }
            }

            return fuvarok;
        }

        private static void HibakKiir(List<Utazas> lista, string fajl)
        {
            var hibasak = lista
                .Where(x => x.Idotartam > 0 && x.Viteldij > 0 && x.Tavolsag == 0)
                .OrderBy(x => x.Indulas)
                .ToList();

            using (StreamWriter sw = new StreamWriter(new FileStream(fajl, FileMode.Create), Encoding.UTF8))
            {
                sw.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");
                hibasak.ForEach(x => sw.WriteLine($"{x.Taxi_id};" +
                    $"{x.Indulas:yyyy-MM-dd HH:mm:ss};" +
                    $"{x.Idotartam};" +
                    $"{x.Tavolsag};" +
                    $"{x.Viteldij};" +
                    $"{x.Borravalo};" +
                    $"{x.Fizetes_modja}")
                );

            }
        }
    }
}
