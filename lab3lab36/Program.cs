using System;
using System.IO;

class Program
{
    static void Main()
    {
        string caleFisier = @"D:\BujdeiFelix\lab3lab36\date.txt";

        if (!File.Exists(caleFisier))
        {
            Console.WriteLine("Fișierul nu există!");
            return;
        }

        (int n, int[] a) = CitesteDateDinFisier(caleFisier);

        Console.WriteLine("Șirul inițial:");
        for (int i = 0; i < n; i++)
        {
            Console.Write(a[i] + " ");
        }
        Console.WriteLine();

        SorteazaSelectieMin(a, n);

        Console.WriteLine("Șirul sortat:");
        for (int i = 0; i < n; i++)
        {
            Console.Write(a[i] + " ");
        }
        Console.WriteLine();

        ScrieRezultatInFisier("iesire.txt", a, n);
    }

    static (int, int[]) CitesteDateDinFisier(string numeFisier)
    {
        string[] linii = File.ReadAllLines(numeFisier);
        int n = int.Parse(linii[0].Trim());

        string[] elemente = linii[1].Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] a = new int[n];

        for (int i = 0; i < n; i++)
        {
            a[i] = int.Parse(elemente[i]);
        }

        return (n, a);
    }

    static int CautaMin(int[] a, int left, int right)
    {
        int indexMin = left;

        for (int i = left + 1; i <= right; i++)
        {
            if (a[i] < a[indexMin])
            {
                indexMin = i;
            }
        }

        return indexMin;
    }

    static void SorteazaSelectieMin(int[] a, int n)
    {
        for (int i = 0; i < n - 1; i++)
        {
            int indexMin = CautaMin(a, i, n - 1);

            if (i != indexMin)
            {
                int temp = a[i];
                a[i] = a[indexMin];
                a[indexMin] = temp;
            }
        }
    }

    static void ScrieRezultatInFisier(string numeFisier, int[] a, int n)
    {
        using (StreamWriter sw = new StreamWriter(numeFisier))
        {
            for (int i = 0; i < n; i++)
            {
                sw.Write(a[i] + " ");
            }
        }
    }
}