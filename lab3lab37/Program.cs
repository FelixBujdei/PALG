using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string caleFisier = @"D:\BujdeiFelix\lab3lab37\cartezian.txt";

        if (!File.Exists(caleFisier))
        {
            Console.WriteLine("Fișierul nu există!");
            return;
        }

        (int n, int[] dimensiuni, List<List<int>> multimi) = CitesteDateDinFisier(caleFisier);

        Console.WriteLine("Produsul cartezian:");
        Console.WriteLine("====================");

        ProdusCartezian(n, dimensiuni, multimi);
    }

    static (int, int[], List<List<int>>) CitesteDateDinFisier(string numeFisier)
    {
        string[] linii = File.ReadAllLines(numeFisier);
        int index = 0;

        int n = int.Parse(linii[index++].Trim());

        int[] dimensiuni = new int[n];
        List<List<int>> multimi = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            int mi = int.Parse(linii[index++].Trim());
            dimensiuni[i] = mi;

            string[] elemente = linii[index++].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            List<int> multime = new List<int>();

            for (int j = 0; j < mi; j++)
            {
                multime.Add(int.Parse(elemente[j]));
            }

            multimi.Add(multime);
        }

        return (n, dimensiuni, multimi);
    }

    static void ProdusCartezian(int n, int[] m, List<List<int>> multimi)
    {
        int[] stiva = new int[n];
        int k = 0;
        stiva[k] = -1;

        while (k >= 0)
        {
            if (stiva[k] < m[k] - 1)
            {
                stiva[k]++;

                if (k == n - 1)
                {
                    for (int i = 0; i < n; i++)
                    {
                        Console.Write(multimi[i][stiva[i]] + " ");
                    }
                    Console.WriteLine();
                }
                else
                {
                    k++;
                    stiva[k] = -1;
                }
            }
            else
            {
                k--;
            }
        }
    }
}