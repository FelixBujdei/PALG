using System;
using System.Collections.Generic;
using System.IO;

class ParcurgereaLabirintului
{
    public struct Locatie
    {
        public int Linie, Coloana;
    }

    public static List<Locatie> DrumMinim_Lee(int[,] labirint, Locatie start, Locatie stop)
    {
        int m = labirint.GetLength(0);
        int n = labirint.GetLength(1);

        int[] dLinie = new int[] { 0, 0, 1, -1 };
        int[] dColoana = new int[] { 1, -1, 0, 0 };

        Queue<Locatie> coada = new Queue<Locatie>();
        coada.Enqueue(start);
        labirint[start.Linie, start.Coloana] = 1;

        while (coada.Count > 0)
        {
            Locatie curent = coada.Dequeue();
            for (int t = 0; t < dLinie.Length; t++)
            {
                int linieVecin = curent.Linie + dLinie[t];
                int coloanaVecin = curent.Coloana + dColoana[t];

                if (linieVecin >= 0 && linieVecin < m &&
                    coloanaVecin >= 0 && coloanaVecin < n &&
                    labirint[linieVecin, coloanaVecin] == 0)
                {
                    labirint[linieVecin, coloanaVecin] = labirint[curent.Linie, curent.Coloana] + 1;
                    coada.Enqueue(new Locatie() { Linie = linieVecin, Coloana = coloanaVecin });
                }
            }
        }

        List<Locatie> drum = new List<Locatie>();
        int lungimeDrum = labirint[stop.Linie, stop.Coloana];

        if (lungimeDrum == 0)
        {
            return null;
        }

        int linie = stop.Linie;
        int coloana = stop.Coloana;

        while (!(linie == start.Linie && coloana == start.Coloana))
        {
            drum.Add(new Locatie() { Linie = linie, Coloana = coloana });

            for (int t = 0; t < dLinie.Length; t++)
            {
                int linieVecin = linie + dLinie[t];
                int coloanaVecin = coloana + dColoana[t];

                if (linieVecin >= 0 && linieVecin < m &&
                    coloanaVecin >= 0 && coloanaVecin < n &&
                    labirint[linieVecin, coloanaVecin] == lungimeDrum - 1)
                {
                    linie = linieVecin;
                    coloana = coloanaVecin;
                    lungimeDrum--;
                    break;
                }
            }
        }

        drum.Add(start);
        drum.Reverse();
        return drum;
    }

    static void Main()
    {
        string fisierIntrare = "labirint.txt";
        string fisierIesire = "out.txt";

        string[] linii = File.ReadAllLines(fisierIntrare);
        int idx = 0;

        string[] dimensiuni = linii[idx++].Split(' ');
        int m = int.Parse(dimensiuni[0]);
        int n = int.Parse(dimensiuni[1]);

        int[,] labirint = new int[m, n];

        for (int i = 0; i < m; i++)
        {
            string linie = linii[idx++];
            int col = 0;

            for (int j = 0; j < linie.Length && col < n; j++)
            {
                if (linie[j] == '-')
                {
                    
                    labirint[i, col] = -1;
                    j++; 
                    col++;
                }
                else if (linie[j] == '0')
                {
                    labirint[i, col] = 0;
                    col++;
                }
                
                else if (linie[j] >= '1' && linie[j] <= '9')
                {
                    labirint[i, col] = 0;
                    col++;
                }
            }
        }

       
        string[] startPos = linii[idx++].Split(' ');
        Locatie start = new Locatie();
        start.Linie = int.Parse(startPos[0]);
        start.Coloana = int.Parse(startPos[1]);

        
        string[] stopPos = linii[idx++].Split(' ');
        Locatie stop = new Locatie();
        stop.Linie = int.Parse(stopPos[0]);
        stop.Coloana = int.Parse(stopPos[1]);

        
        int[,] copie = (int[,])labirint.Clone();
        List<Locatie> drum = DrumMinim_Lee(copie, start, stop);

        
        using (StreamWriter writer = new StreamWriter(fisierIesire))
        {
            if (drum == null)
            {
                writer.WriteLine("Nu exista drum!");
            }
            else
            {
                writer.WriteLine(drum.Count);
                foreach (Locatie loc in drum)
                {
                    writer.WriteLine(loc.Linie + " " + loc.Coloana);
                }
            }
        }

        Console.WriteLine("Gata! Rezultatul este in " + fisierIesire);
    }
}