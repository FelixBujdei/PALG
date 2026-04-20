using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class VerificareVocabular
{
    static void Main()
    {
        CitesteSiRezolva("input_vocabular.txt", "output_vocabular.txt");
    }

    
    static int DistantaLevenshtein(string cuvant1, string cuvant2)
    {
        int n = cuvant1.Length;
        int m = cuvant2.Length;

       
        int[,] dp = new int[n + 1, m + 1];

        
        for (int i = 0; i <= n; i++)
            dp[i, 0] = i;  

        for (int j = 0; j <= m; j++)
            dp[0, j] = j;  

        
        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                if (cuvant1[i - 1] == cuvant2[j - 1])
                {
                    
                    dp[i, j] = dp[i - 1, j - 1];
                }
                else
                {
                   
                    dp[i, j] = Math.Min(
                        dp[i - 1, j] + 1,     
                        Math.Min(
                            dp[i, j - 1] + 1,  
                            dp[i - 1, j - 1] + 1  
                        )
                    );
                }
            }
        }

        return dp[n, m];
    }

   
    static string GasesteCuvantCorect(string cuvantGresit, List<string> dictionar)
    {
        string celMaiApropiat = cuvantGresit;
        int distantaMinima = int.MaxValue;

        foreach (string cuvantCorect in dictionar)
        {
            int distanta = DistantaLevenshtein(cuvantGresit, cuvantCorect);

            if (distanta < distantaMinima)
            {
                distantaMinima = distanta;
                celMaiApropiat = cuvantCorect;
            }
        }

        return celMaiApropiat;
    }

    static void CitesteSiRezolva(string inputFile, string outputFile)
    {
        string[] linii = File.ReadAllLines(inputFile);
        int index = 0;

        
        int n = int.Parse(linii[index++]);

        
        List<string> dictionar = new List<string>();
        for (int i = 0; i < n; i++)
        {
            dictionar.Add(linii[index++].Trim());
        }

       
        int m = int.Parse(linii[index++]);

       
        List<string> cuvinteUtilizator = new List<string>();
        for (int i = 0; i < m; i++)
        {
            cuvinteUtilizator.Add(linii[index++].Trim());
        }

       
        List<string> cuvinteCorectate = new List<string>();
        foreach (string cuvant in cuvinteUtilizator)
        {
           
            if (dictionar.Contains(cuvant))
            {
                cuvinteCorectate.Add(cuvant);
            }
            else
            {
              
                string corectat = GasesteCuvantCorect(cuvant, dictionar);
                cuvinteCorectate.Add(corectat);
            }
        }


        using (StreamWriter writer = new StreamWriter(outputFile))
        {
            foreach (string cuvant in cuvinteCorectate)
            {
                writer.WriteLine(cuvant);
            }
        }
        Console.WriteLine("Cuvinte corectate:");
        foreach (string cuvant in cuvinteCorectate)
        {
            Console.WriteLine(cuvant);
        }
    }
}