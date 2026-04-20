class CelMaiLungPalindrom
{
    static void Main(string[] args)
    {
        new CelMaiLungPalindrom();
    }
    public CelMaiLungPalindrom()
    {
        GenerateTests();
    }
    /// <summary>
    /// Genereaza teste pentru problema palindromului
    /// </summary>
    private void GenerateTests()
    {
        int[] n = new int[] {
5, 10, 100, 300, 500, 700, 800, 900, 950, 999 };
        for (int indexTest = 0; indexTest < n.Length; indexTest++)
        {
            int[] a = GenerateArray(n[indexTest]);
            WriteToFile(a, "input-" + (indexTest + 1).ToString() + ".txt");
            int start = Environment.TickCount;
            using (StreamWriter writer = new StreamWriter(
            "output-" + (indexTest + 1).ToString() + ".txt"))
                writer.WriteLine(Solve(a));
            int end = Environment.TickCount;
            Console.WriteLine("time = {0} ms", end - start);
        }
    }
    /// <summary>
    /// Genereaza un sir de numere alese la intamplare
    /// </summary>
    Random random = new Random();
    private int[] GenerateArray(int n)
    {
        int[] a = new int[n];
        for (int i = 0; i < n; i++)
            a[i] = random.Next(100);
        return a;
    }
    /// <summary>
    /// Rezolva problema palindromulu
    /// /// </summary>
    private int Solve(int[] a)
    {
        int[] aReversed = new int[a.Length];
        for (int i = 0; i < a.Length; i++)
            aReversed[i] = a[a.Length - 1 - i];
        return LengthOfLongestPalindrome(a, aReversed);
    }
    /// <summary>
    /// Problema LCS
    /// </summary>
    private int LengthOfLongestPalindrome(int[] a, int[] b)
    {
        int n = a.Length;
        int[,] length = new int[n, n];
        length[0, 0] = (a[0] == b[0]) ? 1 : 0;
        for (int j = 1; j < n; j++)
            length[0, j] = (a[0] == b[j]) ? 1 : length[0, j - 1];
        for (int i = 1; i < n; i++)
            length[i, 0] = (a[i] == b[0]) ? 1 : length[i - 1, 0];
        for (int i = 1; i < n; i++)
            for (int j = 1; j < n; j++)
                length[i, j] = max(
                length[i - 1, j],
                length[i, j - 1],
                length[i - 1, j - 1] + (int)((a[i] == b[j]) ? 1 : 0));
        return length[n - 1, n - 1];
    }
    private int max(int a, int b, int c)
    {
        return Math.Max(a, Math.Max(b, c));
    }
    /// <summary>
    /// Afiseaza solutia la consola
    /// </summary>
    private void WriteToConsole(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
            Console.Write("{0}, ", a[i]);
        Console.WriteLine();
    }
    /// <summary>
    /// Afiseaza solutia intr-un fisier
    /// </summary>
    private void WriteToFile(int[] a, string fileName)
    {

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine(a.Length);
            for (int i = 0; i < a.Length; i++)
                writer.Write("{0}{1}", a[i], i == a.Length - 1 ? "" : " ");
        }
    }
    /// <summary>
    /// Citeste datele problemei dintr-un fisier
    /// </summary>
    private int[] ReadFromFile(string fileName)
    {
        int[] a = null;
        using (StreamReader reader = new StreamReader("int.txt"))
        {
            int n = int.Parse(reader.ReadLine());
            string[] values = reader.ReadLine().Trim().Split(' ');
            a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = int.Parse(values[i]);
        }
        return a;
    }
}