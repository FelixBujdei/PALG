using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("ALGORITMI PENTRU PROBLEMA 'ELEMENTUL LIPSĂ'");
        Console.WriteLine("=============================================\n");

        string caleFisier = @"D:\BujdeiFelix\LAb3lab12\dateintrare.txt";
        (int n, int[] A) = CitesteDateDinFisier(caleFisier);

        Console.WriteLine($"n = {n}");
        Console.Write("Elementele: ");
        for (int i = 0; i < n - 1; i++)
        {
            Console.Write(A[i] + " ");
        }
        Console.WriteLine("\n");

        Console.WriteLine("Rezultate:");
        Console.WriteLine(new string('-', 30));

        int rez1 = ValoareLipsă_v1(A, n);
        Console.WriteLine($"ValoareLipsă_v1: {rez1}");

        int rez2 = ValoareLipsă_v2(A, n);
        Console.WriteLine($"ValoareLipsă_v2: {rez2}");

        int rez3 = ValoareLipsă_v3(A, n);
        Console.WriteLine($"ValoareLipsă_v3: {rez3}");
    }

    static (int, int[]) CitesteDateDinFisier(string numeFisier)
    {
        string[] linii = File.ReadAllLines(numeFisier);
        int n = int.Parse(linii[0]);

        string[] elemente = linii[1].Split(' ');
        int[] A = new int[n - 1];

        for (int i = 0; i < n - 1; i++)
        {
            A[i] = int.Parse(elemente[i]);
        }

        return (n, A);
    }

    static int ValoareLipsă_v1(int[] A, int n)
    {
        for (int v = 1; v <= n; v++)
        {
            bool amGăsit = false;
            for (int i = 0; i < n - 1; i++)
            {
                if (A[i] == v)
                {
                    amGăsit = true;
                    break;
                }
            }
            if (amGăsit == false)
            {
                return v;
            }
        }
        return -1;
    }

    static int ValoareLipsă_v2(int[] A, int n)
    {
        int[] prezent = new int[n];

        for (int i = 0; i < n; i++)
            prezent[i] = 0;

        for (int i = 0; i < n - 1; i++)
            prezent[A[i] - 1] = 1;

        for (int i = 0; i < n; i++)
            if (prezent[i] == 0)
                return i + 1;

        return -1;
    }

    static int ValoareLipsă_v3(int[] A, int n)
    {
        int sumaTotală = n * (n + 1) / 2;
        int sumaMulțime = 0;

        for (int i = 0; i < n - 1; i++)
            sumaMulțime += A[i];

        return sumaTotală - sumaMulțime;
    }
}