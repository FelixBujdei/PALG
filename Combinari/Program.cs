using System;
using System.IO;

class Combinari
{
    static void Main()
    {
        Citeste("input_combinari.txt", "output_combinari.txt");
    }

    // Varianta 1: 
    static long Combinari_1(int n, int k)
    {
        if (k == 0 || k == n) return 1;
        return Factorial(n) / (Factorial(k) * Factorial(n - k));
    }

    static long Factorial(int n)
    {
        long p = 1;
        for (int i = 1; i <= n; i++)
            p *= i;
        return p;
    }

    // Varianta 2: 
    static int Combinari_2(int n, int k)
    {
        if (k == 0 || n == k) return 1;
        return Combinari_2(n - 1, k) + Combinari_2(n - 1, k - 1);
    }

    // Varianta 3: 
    static int Combinari_3(int n, int k)
    {
        if (k == 0 || k == n) return 1;
        if (k == n - 1) return n;

        int[,] c = new int[n + 1, n + 1];
        c[0, 0] = 1;
        c[1, 0] = 1;
        c[1, 1] = 1;

        for (int i = 2; i <= n; i++)
        {
            c[i, 0] = 1;
            c[i, i] = 1;
            for (int j = 1; j < i; j++)
            {
                c[i, j] = c[i - 1, j] + c[i - 1, j - 1];
            }
        }
        return c[n, k];
    }

    // Varianta 4
    static int Combinari_4(int n, int k)
    {
        if (k == 0 || k == n) return 1;
        if (k == n - 1) return n;

        int[] ant = new int[k + 1];
        int[] curent = new int[k + 1];

        ant[0] = 1;
        ant[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            int limit = Math.Min(i - 1, k);
            curent[0] = 1;

            for (int j = 1; j <= limit; j++)
            {
                curent[j] = ant[j] + ant[j - 1];
            }

            if (i <= k)
                curent[i] = 1;

            Array.Copy(curent, ant, curent.Length);
        }

        return curent[k];
    }

    static void Citeste(string inputFile, string outputFile)
    {
        string[] linii = File.ReadAllLines(inputFile);

        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (string linie in linii)
            {
                if (string.IsNullOrWhiteSpace(linie)) continue;

                string[] valori = linie.Trim().Split(' ');
                int n = int.Parse(valori[0]);
                int k = int.Parse(valori[1]);

                long v1 = Combinari_1(n, k);
                int v2 = Combinari_2(n, k);
                int v3 = Combinari_3(n, k);
                int v4 = Combinari_4(n, k);

                writer.WriteLine($"C({n},{k}) = {v1} (v1) | {v2} (v2) | {v3} (v3) | {v4} (v4)");
            }
        }
    }
}