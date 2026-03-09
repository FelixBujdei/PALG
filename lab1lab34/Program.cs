using System;
using System.IO;

class Program
{
    static void Main()
    {
        string caleFisier = @"D:\BujdeiFelix\lab1lab34\numere.txt";

        if (!File.Exists(caleFisier))
        {
            Console.WriteLine("Fișierul nu există!");
            return;
        }

        string[] linii = File.ReadAllLines(caleFisier);

        using (StreamWriter sw = new StreamWriter("iesire.txt"))
        {
            foreach (string linie in linii)
            {
                if (string.IsNullOrWhiteSpace(linie))
                    continue;

                int n = int.Parse(linie.Trim());
                string rezultat = PRIM(n);

                Console.WriteLine($"{n} -> {rezultat}");
                sw.WriteLine(rezultat);
            }
        }

        Console.WriteLine("\nRezultatele au fost scrise în fișierul iesire.txt");
    }

    static string PRIM(int n)
    {
        if (n == 2)
            return "PRIM";

        if (n % 2 == 0)
            return "COMPUS";

        int r = (int)Math.Sqrt(n);

        for (int i = 3; i <= r; i += 2)
        {
            if (n % i == 0)
                return "COMPUS";
        }

        return "PRIM";
    }
}