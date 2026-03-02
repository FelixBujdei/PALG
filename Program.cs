using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;

namespace lab2
{
    class RezultatMasurare
    {
        public string Dimensiune { get; set; }
        public string TipStructura { get; set; }
        public string Operatie { get; set; }
        public double TimpExecutieMs { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] dimensiuni = { 1000, 10000, 100000, 1000000 };
            List<RezultatMasurare> toateRezultatele = new List<RezultatMasurare>();

            Console.WriteLine("Începe generarea fișierelor și măsurarea timpilor...");
            Console.WriteLine(new string('-', 70));

            foreach (int n in dimensiuni)
            {
                Console.WriteLine($"\nProcesăm dimensiunea n = {n}");

                // Măsurăm timpul pentru generarea listei
                Stopwatch swGenerare = new Stopwatch();
                swGenerare.Start();

                List<int> lista = GenereazaLista(n);

                swGenerare.Stop();

                toateRezultatele.Add(new RezultatMasurare
                {
                    Dimensiune = n.ToString(),
                    TipStructura = "Generare date",
                    Operatie = "Generare numere aleatoare",
                    TimpExecutieMs = swGenerare.Elapsed.TotalMilliseconds
                });

                Console.WriteLine($"  Timp generare: {swGenerare.Elapsed.TotalMilliseconds:F3} ms");

                // Măsurăm timpul pentru salvare Listă
                Stopwatch swLista = new Stopwatch();
                swLista.Start();

                string fisierLista = $"lista_{n}.txt";
                SalveazaInFisier(lista, fisierLista);

                swLista.Stop();

                toateRezultatele.Add(new RezultatMasurare
                {
                    Dimensiune = n.ToString(),
                    TipStructura = "Listă",
                    Operatie = "Salvare în fișier",
                    TimpExecutieMs = swLista.Elapsed.TotalMilliseconds
                });

                Console.WriteLine($"  Timp salvare listă: {swLista.Elapsed.TotalMilliseconds:F3} ms");

                // Măsurăm timpul pentru creare și salvare Stivă
                Stopwatch swStiva = new Stopwatch();
                swStiva.Start();

                Stack<int> stiva = new Stack<int>(lista);
                string fisierStiva = $"stiva_{n}.txt";
                SalveazaStackInFisier(stiva, fisierStiva);

                swStiva.Stop();

                toateRezultatele.Add(new RezultatMasurare
                {
                    Dimensiune = n.ToString(),
                    TipStructura = "Stivă",
                    Operatie = "Creare stivă și salvare",
                    TimpExecutieMs = swStiva.Elapsed.TotalMilliseconds
                });

                Console.WriteLine($"  Timp stivă: {swStiva.Elapsed.TotalMilliseconds:F3} ms");

                // Măsurăm timpul pentru creare și salvare Coadă
                Stopwatch swCoada = new Stopwatch();
                swCoada.Start();

                Queue<int> coada = new Queue<int>(lista);
                string fisierCoada = $"coada_{n}.txt";
                SalveazaQueueInFisier(coada, fisierCoada);

                swCoada.Stop();

                toateRezultatele.Add(new RezultatMasurare
                {
                    Dimensiune = n.ToString(),
                    TipStructura = "Coadă",
                    Operatie = "Creare coadă și salvare",
                    TimpExecutieMs = swCoada.Elapsed.TotalMilliseconds
                });

                Console.WriteLine($"  Timp coadă: {swCoada.Elapsed.TotalMilliseconds:F3} ms");

              
                Console.WriteLine($"  Determinare minim:");

            
                Stopwatch swMinIterativ = new Stopwatch();
                swMinIterativ.Start();

                int minIterativ = MinimIterativ(lista);

                swMinIterativ.Stop();

                toateRezultatele.Add(new RezultatMasurare
                {
                    Dimensiune = n.ToString(),
                    TipStructura = "Minim",
                    Operatie = "Algoritm iterativ",
                    TimpExecutieMs = swMinIterativ.Elapsed.TotalMilliseconds
                });

                Console.WriteLine($"    Iterativ: {swMinIterativ.Elapsed.TotalMilliseconds:F3} ms (min={minIterativ})");

               
                Stopwatch swMinRecursiv = new Stopwatch();
                swMinRecursiv.Start();

                int minRecursiv = MinimRecursiv(lista, 0, lista.Count - 1);

                swMinRecursiv.Stop();

                toateRezultatele.Add(new RezultatMasurare
                {
                    Dimensiune = n.ToString(),
                    TipStructura = "Minim",
                    Operatie = "Algoritm recursiv",
                    TimpExecutieMs = swMinRecursiv.Elapsed.TotalMilliseconds
                });

                Console.WriteLine($"    Recursiv: {swMinRecursiv.Elapsed.TotalMilliseconds:F3} ms (min={minRecursiv})");

                
                double timpTotal = swGenerare.Elapsed.TotalMilliseconds +
                                  swLista.Elapsed.TotalMilliseconds +
                                  swStiva.Elapsed.TotalMilliseconds +
                                  swCoada.Elapsed.TotalMilliseconds +
                                  swMinIterativ.Elapsed.TotalMilliseconds +
                                  swMinRecursiv.Elapsed.TotalMilliseconds;

                Console.WriteLine($"  TIMP TOTAL pentru n={n}: {timpTotal:F3} ms");
            }

            
            AfiseazaRezultate(toateRezultatele);

           
            SalveazaRezultateInFisier(toateRezultatele, "raport_complet.txt");

       
            GenereazaRezumat(toateRezultatele);

            Console.WriteLine("\n" + new string('=', 70));
            Console.WriteLine("GENERARE COMPLETĂ!");
            Console.WriteLine($"Au fost create 12 fișiere cu date și 2 fișiere cu rapoarte:");
            Console.WriteLine("- lista_*.txt, stiva_*.txt, coada_*.txt (12 fișiere)");
            Console.WriteLine("- raport_complet.txt (toate măsurătorile)");
            Console.WriteLine("- rezumat_generare.txt (rezumat pe dimensiuni)");
            Console.WriteLine(new string('=', 70));
        }

        static List<int> GenereazaLista(int n)
        {
            Random rand = new Random();
            List<int> lista = new List<int>(n);

            for (int i = 0; i < n; i++)
            {
                lista.Add(rand.Next(1, 1000001));
            }

            return lista;
        }

        static void SalveazaInFisier(List<int> lista, string numeFisier)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                foreach (int val in lista)
                {
                    sw.Write(val + " ");
                }
            }
        }

        static void SalveazaStackInFisier(Stack<int> stiva, string numeFisier)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                int[] array = stiva.ToArray();
                Array.Reverse(array);
                foreach (int val in array)
                {
                    sw.Write(val + " ");
                }
            }
        }

        static void SalveazaQueueInFisier(Queue<int> coada, string numeFisier)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                foreach (int val in coada)
                {
                    sw.Write(val + " ");
                }
            }
        }

        static int MinimIterativ(List<int> lista)
        {
           
            if (lista == null)
                throw new ArgumentNullException(nameof(lista), "Lista nu poate fi null");

            if (lista.Count == 0)
                throw new ArgumentException("Lista nu poate fi goală", nameof(lista));

            int min = lista[0];
            for (int i = 1; i < lista.Count; i++)
            {
                if (lista[i] < min)
                    min = lista[i];
            }
            return min;
        }

        static int MinimRecursiv(List<int> lista, int stanga, int dreapta)
        {
           
            if (lista == null)
                throw new ArgumentNullException(nameof(lista), "Lista nu poate fi null");

            if (lista.Count == 0)
                throw new ArgumentException("Lista nu poate fi goală", nameof(lista));

            
            if (stanga < 0 || stanga >= lista.Count)
                throw new ArgumentOutOfRangeException(nameof(stanga), "Indexul stânga este invalid");

            if (dreapta < 0 || dreapta >= lista.Count)
                throw new ArgumentOutOfRangeException(nameof(dreapta), "Indexul dreapta este invalid");

            if (stanga > dreapta)
                throw new ArgumentException("Indexul stânga nu poate fi mai mare decât indexul dreapta");

         
            if (stanga == dreapta)
                return lista[stanga];

            if (dreapta - stanga == 1)
            {
                return lista[stanga] < lista[dreapta] ? lista[stanga] : lista[dreapta];
            }

           
            int mijloc = stanga + (dreapta - stanga) / 2;

           
            int minStanga = MinimRecursiv(lista, stanga, mijloc);
            int minDreapta = MinimRecursiv(lista, mijloc + 1, dreapta);

            return minStanga < minDreapta ? minStanga : minDreapta;
        }

        static void AfiseazaRezultate(List<RezultatMasurare> rezultate)
        {
            Console.WriteLine("\n" + new string('=', 90));
            Console.WriteLine("REZULTATE MĂSURĂTORI TIMP DE EXECUȚIE");
            Console.WriteLine(new string('=', 90));

            Console.WriteLine($"{"Dimensiune",-12} {"Tip Structură",-20} {"Operație",-35} {"Timp (ms)",-15}");
            Console.WriteLine(new string('-', 82));

            foreach (var rez in rezultate)
            {
                Console.WriteLine($"{rez.Dimensiune,-12} {rez.TipStructura,-20} {rez.Operatie,-35} {rez.TimpExecutieMs,15:F3}");
            }

            Console.WriteLine(new string('=', 90));
        }

        static void SalveazaRezultateInFisier(List<RezultatMasurare> rezultate, string numeFisier)
        {
            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                sw.WriteLine($"{"Dimensiune",-12} {"Tip Structură",-20} {"Operație",-35} {"Timp (ms)",-15}");
                sw.WriteLine(new string('-', 82));

                foreach (var rez in rezultate)
                {
                    sw.WriteLine($"{rez.Dimensiune,-12} {rez.TipStructura,-20} {rez.Operatie,-35} {rez.TimpExecutieMs,15:F3}");
                }
            }
        }

        static void GenereazaRezumat(List<RezultatMasurare> rezultate)
        {
            string numeFisier = "rezumat_generare.txt";

            using (StreamWriter sw = new StreamWriter(numeFisier))
            {
                sw.WriteLine("REZUMAT GENERARE FIȘIERE ȘI DETERMINARE MINIM");
                sw.WriteLine(new string('=', 70));
                sw.WriteLine();

                var grupuri = rezultate.GroupBy(r => r.Dimensiune);

                foreach (var grup in grupuri.OrderBy(g => int.Parse(g.Key)))
                {
                    sw.WriteLine($"DIMENSIUNE n = {grup.Key}");
                    sw.WriteLine(new string('-', 50));

                    foreach (var rez in grup)
                    {
                        sw.WriteLine($"  {rez.Operatie,-40} {rez.TimpExecutieMs,12:F3} ms");
                    }

                    double total = grup.Sum(r => r.TimpExecutieMs);
                    sw.WriteLine($"  {' ',40} ----------");
                    sw.WriteLine($"  TOTAL{' ',36} {total,12:F3} ms");
                    sw.WriteLine();
                }

                sw.WriteLine(new string('=', 70));
                sw.WriteLine($"Data generării: {DateTime.Now}");
                sw.WriteLine($"Total măsurători: {rezultate.Count}");
            }

            Console.WriteLine($"\nRezumatul a fost salvat în: {numeFisier}");
        }
    }
}