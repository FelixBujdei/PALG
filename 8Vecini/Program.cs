using System;
using System.Collections.Generic;
using System.IO;

class LabirintOptVecini
{
    public struct Locatie
    {
        public int Linie, Coloana;

        public Locatie(int linie, int coloana)
        {
            Linie = linie;
            Coloana = coloana;
        }

        public override string ToString()
        {
            return Linie + " " + Coloana;
        }
    }

    static void Main(string[] args)
    {
        string fisierIntrare = "labirint8.txt";
        string fisierIesire = "out.txt";

        if (args.Length >= 1) fisierIntrare = args[0];
        if (args.Length >= 2) fisierIesire = args[1];

        try
        {
            string[] linii = File.ReadAllLines(fisierIntrare);
            int idx = 0;

            
            string[] dimensiuni = linii[idx++].Split(' ');
            int m = int.Parse(dimensiuni[0]);
            int n = int.Parse(dimensiuni[1]);

           
            int[,] labirint = new int[m, n];

            
            for (int i = 0; i < m; i++)
            {
                string linie = linii[idx++].Trim();
                int col = 0;

                for (int j = 0; j < linie.Length && col < n; j++)
                {
                    char c = linie[j];

                    if (c == '-')
                    {
                        
                        if (j + 1 < linie.Length && linie[j + 1] == '1')
                        {
                            labirint[i, col] = -1;  
                            j++; 
                            col++;
                        }
                        else
                        {
                            
                            labirint[i, col] = -1;
                            j++;
                            col++;
                        }
                    }
                    else if (c == '0')
                    {
                        labirint[i, col] = 0; 
                        col++;
                    }
                    else if (c == '1')
                    {
                        
                        labirint[i, col] = 0;
                        col++;
                    }
                    else if (c == '2')
                    {
                        
                        labirint[i, col] = 0;
                        col++;
                    }
                    else
                    {
                      
                    }
                }
            }

            
            string[] startPos = linii[idx++].Split(' ');
            Locatie start = new Locatie(int.Parse(startPos[0]), int.Parse(startPos[1]));

           
            string[] stopPos = linii[idx++].Split(' ');
            Locatie stop = new Locatie(int.Parse(stopPos[0]), int.Parse(stopPos[1]));   
            List<Locatie> drum = DrumMinim8Vecini(labirint, start, stop, m, n);

          
            using (StreamWriter writer = new StreamWriter(fisierIesire))
            {
                if (drum == null || drum.Count == 0)
                {
                    writer.WriteLine("Nu exista drum!");
                    Console.WriteLine("Nu exista drum!");
                }
                else
                {
                    writer.WriteLine(drum.Count);
                    foreach (Locatie loc in drum)
                    {
                        writer.WriteLine(loc);
                    }
                    Console.WriteLine($"Drum gasit! Lungime: {drum.Count}");

                    Console.WriteLine("\nDrumul:");
                    foreach (Locatie loc in drum)
                    {
                        Console.WriteLine($"({loc.Linie},{loc.Coloana})");
                    }
                }
            }

            Console.WriteLine($"\nRezultat scris in {fisierIesire}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare: " + ex.Message);
            Console.WriteLine("Stack trace: " + ex.StackTrace);
        }
    }

    static List<Locatie> DrumMinim8Vecini(int[,] labirint, Locatie start, Locatie stop, int m, int n)
    {
     
        int[] di = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dj = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };

      
        if (labirint[start.Linie, start.Coloana] == -1)
        {
            Console.WriteLine("Eroare: Startul este pe perete!");
            return null;
        }


        if (labirint[stop.Linie, stop.Coloana] == -1)
        {
            Console.WriteLine("Eroare: Destinatia este pe perete!");
            return null;
        }

  
        int[,] distante = (int[,])labirint.Clone();

       
        distante[start.Linie, start.Coloana] = 1;
        Queue<Locatie> coada = new Queue<Locatie>();
        coada.Enqueue(start);

        while (coada.Count > 0)
        {
            Locatie curent = coada.Dequeue();

            for (int t = 0; t < 8; t++)
            {
                int iv = curent.Linie + di[t];
                int jv = curent.Coloana + dj[t];

                if (iv >= 0 && iv < m && jv >= 0 && jv < n && distante[iv, jv] == 0)
                {
                    distante[iv, jv] = distante[curent.Linie, curent.Coloana] + 1;
                    coada.Enqueue(new Locatie(iv, jv));
                }
            }
        }

        if (distante[stop.Linie, stop.Coloana] == 0)
        {
            Console.WriteLine("Nu s-a putut ajunge la destinatie!");
            return null;
        }

        
        List<Locatie> drum = new List<Locatie>();
        int valCurenta = distante[stop.Linie, stop.Coloana];
        int linie = stop.Linie;
        int coloana = stop.Coloana;

        while (!(linie == start.Linie && coloana == start.Coloana))
        {
            drum.Add(new Locatie(linie, coloana));

            for (int t = 0; t < 8; t++)
            {
                int iv = linie + di[t];
                int jv = coloana + dj[t];

                if (iv >= 0 && iv < m && jv >= 0 && jv < n &&
                    distante[iv, jv] == valCurenta - 1)
                {
                    linie = iv;
                    coloana = jv;
                    valCurenta--;
                    break;
                }
            }
        }

        drum.Add(start);
        drum.Reverse();
        return drum;
    }
}