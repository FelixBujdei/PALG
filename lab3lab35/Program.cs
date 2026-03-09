using System;
using System.IO;

class Program
{
    static void Main()
    {
        string caleFisier = @"D:\BujdeiFelix\lab3lab35\date.txt";

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

        SorteazaBule(a, n);

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

    static void SorteazaBule(int[] a, int n)
    {
        bool sortat;

        do
        {
            sortat = true;

            for (int i = 0; i < n - 1; i++)
            {
                if (a[i] > a[i + 1])
                {
                    int temp = a[i];
                    a[i] = a[i + 1];
                    a[i + 1] = temp;

                    sortat = false;
                }
            }
        } while (!sortat);
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