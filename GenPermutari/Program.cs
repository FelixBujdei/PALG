using System;
using System.IO;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string caleFisier = @"D:\BujdeiFelix\GenPermutari\dateintrare.txt";
        int n = CitesteDateDinFisier(caleFisier);

        Stopwatch sw = Stopwatch.StartNew();
        GenereazaPermutari(n);
        sw.Stop();

        Console.WriteLine($"\nTimp de executie: {sw.Elapsed.TotalMilliseconds} ms");
    }

    static int CitesteDateDinFisier(string numeFisier)
    {
        string continut = File.ReadAllText(numeFisier).Trim();
        return int.Parse(continut);
    }

    static void GenereazaPermutari(int n)
    {
        int[] stiva = new int[n];
        int k = 0;
        stiva[k] = 0;

        while (k >= 0)
        {
            bool amSuccesor = false;
            bool esteValid = false;

            do
            {
                if (stiva[k] < n)
                {
                    amSuccesor = true;
                    stiva[k]++;

                    esteValid = true;
                    for (int i = 0; i < k; i++)
                    {
                        if (stiva[i] == stiva[k])
                        {
                            esteValid = false;
                            break;
                        }
                    }
                }
                else
                {
                    amSuccesor = false;
                }
            } while (amSuccesor && !esteValid);

            if (amSuccesor)
            {
                if (k == n - 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(stiva[i]);
                        if (i < n - 1) Console.Write(",");
                    }
                    Console.WriteLine();
                }
                else
                {
                    k++;
                    stiva[k] = 0;
                }
            }
            else
            {
                k--;
            }
        }
    }
}