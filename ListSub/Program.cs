using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class LIS
{
    static void Main()
    {
        Citeste("input_lis.txt", "output_lis.txt");
    }

   
    static int[] LIS_Algoritm(int[] a)
    {
        int n = a.Length;
        int[] lung = new int[n];

        
        lung[n - 1] = 1;

        for (int i = n - 2; i >= 0; i--)
        {
            int max = 0;
            for (int j = i + 1; j < n; j++)
            {
                if (a[i] <= a[j] && max < lung[j])
                    max = lung[j];
            }
            lung[i] = max + 1;
        }

        return lung;
    }

    static void Citeste(string inputFile, string outputFile)
    {
        string[] linii = File.ReadAllLines(inputFile);

        int n = int.Parse(linii[0]);
        int[] a = linii[1].Trim().Split(' ').Select(int.Parse).ToArray();

        int[] lung = LIS_Algoritm(a);

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            int max = lung[0];
            for (int i = 1; i < n; i++)
                if (max < lung[i]) max = lung[i];

            writer.WriteLine(max);

            
            int poz = 0;
            for (int i = 0; i < n; i++)
                if (lung[i] == max) { poz = i; break; }

     
            List<int> subsecventa = new List<int>();
            subsecventa.Add(a[poz]);

            int currentPos = poz;
            int currentLength = max;

            for (int i = poz + 1; i < n; i++)
            {
                if (lung[i] == currentLength - 1 && a[currentPos] <= a[i])
                {
                    subsecventa.Add(a[i]);
                    currentPos = i;
                    currentLength--;
                }
            }

            writer.WriteLine(string.Join(" ", subsecventa));
        }
    }
}