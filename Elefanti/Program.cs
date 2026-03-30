using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Elefanti
{
    class Elefant
    {
        public int Index { get; set; }
        public int Greutate { get; set; }
        public int Inteligenta { get; set; }

        public Elefant(int index, int g, int i)
        {
            Index = index;
            Greutate = g;
            Inteligenta = i;
        }
    }

    static void Main()
    {
        Citeste("input_elefanti.txt", "output_elefanti.txt");
    }

    static int RezolvaTestElefanti(List<Elefant> elefanti)
    {
        int n = elefanti.Count;

        
        elefanti = elefanti.OrderBy(e => e.Greutate)
                           .ThenByDescending(e => e.Inteligenta)
                           .ToList();

        int[] lung = new int[n];

        
        for (int i = n - 1; i >= 0; i--)
        {
            lung[i] = 1;
            for (int j = i + 1; j < n; j++)
            {
                
                if (elefanti[i].Inteligenta >= elefanti[j].Inteligenta && lung[i] < lung[j] + 1)
                {
                    lung[i] = lung[j] + 1;
                }
            }
        }

       
        int max = 0;
        for (int i = 0; i < n; i++)
        {
            if (lung[i] > max)
                max = lung[i];
        }

        return max;
    }

    static void Citeste(string inputFile, string outputFile)
    {
        string[] linii = File.ReadAllLines(inputFile);

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            int T = int.Parse(linii[0]);
            int lineIndex = 1;

            for (int testIndex = 0; testIndex < T; testIndex++)
            {
                int n = int.Parse(linii[lineIndex++]);
                List<Elefant> elefanti = new List<Elefant>();

                for (int i = 0; i < n; i++)
                {
                    string[] valori = linii[lineIndex++].Trim().Split(' ');
                    int greutate = int.Parse(valori[0]);
                    int inteligenta = int.Parse(valori[1]);

                    elefanti.Add(new Elefant(i + 1, greutate, inteligenta));
                }

                int rezultat = RezolvaTestElefanti(elefanti);
                writer.WriteLine(rezultat);
            }
        }
    }
}