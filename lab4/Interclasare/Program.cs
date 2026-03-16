using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Program
{
    static Random rand = new Random();

    static void Main()
    {
        Console.WriteLine("ALGORITMI DE SORTARE - DIVIDE ET IMPERA");
        Console.WriteLine("=========================================\n");

        int[] a = CitesteDate();

        Console.WriteLine("Vectorul initial: " + string.Join(" ", a));
        Console.WriteLine();

        
        Console.WriteLine("1. SORTARE PRIN INTERCLASARE (MERGE SORT)");
        Console.WriteLine("-------------------------------------------");
        int[] mergeSortVector = CitesteDate();
        RuleazaSiMasoara("Merge Sort", () => SorteazaInterclasare(mergeSortVector, 0, mergeSortVector.Length - 1));

        
        Console.WriteLine("\n2. QUICKSORT - 3 VARIANTE");
        Console.WriteLine("---------------------------");

       
        int[] qsMijlocVector = CitesteDate();
        RuleazaSiMasoara("QuickSort (pivot mijloc)", () => QuickSortMijloc(qsMijlocVector, 0, qsMijlocVector.Length - 1));

        
        int[] qsPrimaVector = CitesteDate();
        RuleazaSiMasoara("QuickSort (pivot prima pozitie)", () => QuickSortPrimaPozitie(qsPrimaVector, 0, qsPrimaVector.Length - 1));

       
        int[] qsRandomVector = CitesteDate();
        RuleazaSiMasoara("QuickSort (pivot random)", () => QuickSortRandom(qsRandomVector, 0, qsRandomVector.Length - 1));

       
        Console.WriteLine("\n3. REZULTATE FINALE");
        Console.WriteLine("---------------------");
        Console.WriteLine($"Merge Sort:       {string.Join(" ", mergeSortVector)}");
        Console.WriteLine($"QuickSort mijloc: {string.Join(" ", qsMijlocVector)}");
        Console.WriteLine($"QuickSort prima:  {string.Join(" ", qsPrimaVector)}");
        Console.WriteLine($"QuickSort random: {string.Join(" ", qsRandomVector)}");

        ScrieRezultat(mergeSortVector);
        Console.WriteLine("\nRezultatul a fost salvat în output.txt");
    }

    static void RuleazaSiMasoara(string numeAlgoritm, Action sorteaza)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        sorteaza();
        sw.Stop();
        Console.WriteLine($"{numeAlgoritm}: {sw.Elapsed.TotalMilliseconds} ms");
    }

    static int[] CitesteDate()
    {
        string[] linii = File.ReadAllLines("input.txt");
        return linii[1].Trim().Split(' ').Select(int.Parse).ToArray();
    }

    static void ScrieRezultat(int[] a)
    {
        File.WriteAllText("output.txt", string.Join(" ", a));
    }

    // ==================== MERGE SORT (INTERCLASARE) ====================

    static void SorteazaInterclasare(int[] a, int p, int q)
    {
        if (p < q)
        {
           
            int m = (p + q) / 2;

           
            SorteazaInterclasare(a, p, m);
            SorteazaInterclasare(a, m + 1, q);

            int n1 = m - p + 1;
            int n2 = q - m;

            int[] b = new int[n1];
            int[] c = new int[n2];

            for (int idx = 0; idx < n1; idx++)
                b[idx] = a[p + idx];

            for (int idx = 0; idx < n2; idx++)
                c[idx] = a[m + 1 + idx];

            
            int[] rezultat = Interclaseaza(b, n1, c, n2);

         
            for (int idx = 0; idx < rezultat.Length; idx++)
                a[p + idx] = rezultat[idx];
        }
    }

    static int[] Interclaseaza(int[] a, int n, int[] b, int m)
    {
        int k = 0;           
        int i = 0;           
        int j = 0;          
        int[] c = new int[n + m];

      
        while (i < n && j < m)
        {
            if (a[i] == b[j])
            {
                c[k++] = a[i++];
                c[k++] = b[j++];
            }
            else if (a[i] < b[j])
            {
                c[k++] = a[i++];
            }
            else
            {
                c[k++] = b[j++];
            }
        }

        
        while (i < n)
        {
            c[k++] = a[i++];
        }

        
        while (j < m)
        {
            c[k++] = b[j++];
        }

        return c;
    }

    // ==================== QUICKSORT - VARIANTE ====================

    // VARIANTEA 1: PIVOT PE MIJLOC
    static void QuickSortMijloc(int[] a, int p, int q)
    {
        if (p < q)
        {
            int i = p;
            int j = q;
            int x = a[(p + q) / 2]; 
            while (i <= j)
            {
                while (i <= q && a[i] < x) i++;
                while (j >= p && a[j] > x) j--;

                if (i <= j)
                {
                    
                    int temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            }

            
            if (p < j) QuickSortMijloc(a, p, j);
            if (i < q) QuickSortMijloc(a, i, q);
        }
    }

    // VARIANTEA 2: PIVOT PE PRIMA POZIȚIE
    static void QuickSortPrimaPozitie(int[] a, int p, int q)
    {
        if (p < q)
        {
           
            int pivot = a[p];
            int i = p + 1;
            int j = q;

            while (i <= j)
            {
               
                while (i <= q && a[i] <= pivot) i++;
                while (j > p && a[j] > pivot) j--;

                if (i < j)
                {
                    
                    int temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }

            
            int temp2 = a[p];
            a[p] = a[j];
            a[j] = temp2;

            
            if (p < j - 1) QuickSortPrimaPozitie(a, p, j - 1);
            if (j + 1 < q) QuickSortPrimaPozitie(a, j + 1, q);
        }
    }

    // VARIANTEA 3: PIVOT RANDOM
    static void QuickSortRandom(int[] a, int p, int q)
    {
        if (p < q)
        {
            
            int randomIndex = rand.Next(p, q + 1);
            int temp = a[p];
            a[p] = a[randomIndex];
            a[randomIndex] = temp;

           
            int pivot = a[p];
            int i = p + 1;
            int j = q;

            while (i <= j)
            {
                while (i <= q && a[i] <= pivot) i++;
                while (j > p && a[j] > pivot) j--;

                if (i < j)
                {
                    int temp2 = a[i];
                    a[i] = a[j];
                    a[j] = temp2;
                }
            }

           
            int temp3 = a[p];
            a[p] = a[j];
            a[j] = temp3;

           
            if (p < j - 1) QuickSortRandom(a, p, j - 1);
            if (j + 1 < q) QuickSortRandom(a, j + 1, q);
        }
    }
}