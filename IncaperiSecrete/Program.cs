using System;
using System.Collections.Generic;
using System.IO;

class IncaperiSecrete
{
    public struct Locatie
    {
        public int Linie, Coloana;

        public Locatie(int linie, int coloana)
        {
            Linie = linie;
            Coloana = coloana;
        }
    }

    static void Main(string[] args)
    {
        string fisierIntrare = "incaperi.txt";
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

            
            int[,] a = new int[m, n];

            
            for (int i = 0; i < m; i++)
            {
                
                string linie = linii[idx++].Trim();
                int col = 0;

                for (int j = 0; j < linie.Length && col < n; j++)
                {
                    if (linie[j] == '-')
                    {
                        
                        a[i, col] = -1;
                        j++; 
                        col++;
                    }
                    else if (linie[j] == '0')
                    {
                        a[i, col] = 0;  
                        col++;
                    }
                    else if (linie[j] >= '1' && linie[j] <= '9')
                    {
                        
                        col++;
                    }
                }
            }
            int nrIncaperi = NumaraIncaperiSecrete(a, m, n);
            using (StreamWriter writer = new StreamWriter(fisierIesire))
            {
                writer.WriteLine(nrIncaperi);
            }

            Console.WriteLine($"\nNumarul de incaperi secrete: {nrIncaperi}");
            Console.WriteLine($"Rezultatul a fost scris in fisierul: {fisierIesire}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Eroare: " + ex.Message);
        }
    }

   
    static void Lee_Incaperi(int[,] a, int m, int n, int iStart, int jStart, int nr)
    {
       
        int[] di = new int[] { 0, 0, 1, -1 };
        int[] dj = new int[] { 1, -1, 0, 0 };

       
        a[iStart, jStart] = nr;

        
        Queue<Locatie> coada = new Queue<Locatie>();
        coada.Enqueue(new Locatie(iStart, jStart));

       
        while (coada.Count > 0)
        {
            Locatie curent = coada.Dequeue();
            int i = curent.Linie;
            int j = curent.Coloana;

           
            for (int t = 0; t < 4; t++)
            {
                int iv = i + di[t];
                int jv = j + dj[t];

              
                if (iv >= 0 && iv < m && jv >= 0 && jv < n && a[iv, jv] == 0)
                {
                    a[iv, jv] = nr;  
                    coada.Enqueue(new Locatie(iv, jv));
                }
            }
        }
    }

    
    static int NumaraIncaperiSecrete(int[,] a, int m, int n)
    {
        int nr = 0;

        
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
               
                if (a[i, j] == 0)
                {
                    nr++;  
                    Lee_Incaperi(a, m, n, i, j, nr);  
                }
            }
        }

        return nr;
    }
}