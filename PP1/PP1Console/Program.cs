// PP1 – Práctica Programada 1 (SC-701)
// .NET 8 – Consola – Solo int (System.Int32)

using System;

internal static class Program
{
    // ---------- Métodos requeridos ----------
    // SumFor: fórmula n*(n+1)/2 en contexto unchecked (permitir overflow de int)
    static int SumFor(int n)
    {
        unchecked
        {
            // No se valida n, se asume n>0
            return (n * (n + 1)) / 2;
        }
    }

    // SumIte: versión iterativa equivalente a SumRec
    static int SumIte(int n)
    {
        unchecked
        {
            int s = 0;
            for (int i = 1; i <= n; i++)
                s += i;          // overflow int permitido
            return s;
        }
    }

    // ---------- Búsquedas ----------
    // Ascendente: 1 -> MaxValue, retorna (n, sum) del último válido (sum>0)
    static (int n, int sum) FindAsc(Func<int, int> sumFn)
    {
        unchecked
        {
            int lastN = 0;
            int lastSum = 0;

            // IMPORTANTE: se “chequea cada valor de n” como indica la consigna.
            for (int n = 1; n > 0; n++)              // n>0 evita desbordar a negativo
            {
                int s = sumFn(n);
                if (s > 0)
                {
                    lastN = n;
                    lastSum = s;
                }
                else
                {
                    // el anterior fue el último válido
                    break;
                }
            }
            return (lastN, lastSum);
        }
    }

    // Descendente: MaxValue -> 1, retorna (n, sum) del primer válido (sum>0)
    static (int n, int sum) FindDesc(Func<int, int> sumFn)
    {
        unchecked
        {
            // “Desde Max hasta 1”, chequeando cada n hasta hallar el primero válido
            for (int n = int.MaxValue; n >= 1; n--)
            {
                int s = sumFn(n);
                if (s > 0)
                    return (n, s);
            }
            return (0, 0); // No debería ocurrir
        }
    }

    // ---------- Formato de salida ----------
    static void PrintResult(string title, (int n, int sum) asc, (int n, int sum) desc)
    {
        Console.WriteLine($"• {title}:");
        Console.WriteLine($"        ◦ From 1 to Max → n: {asc.n} → sum: {asc.sum}");
        Console.WriteLine($"        ◦ From Max to 1 → n: {desc.n} → sum: {desc.sum}");
        Console.WriteLine();
    }

    // ---------- Main ----------
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var ascFor  = FindAsc(SumFor);
        var descFor = FindDesc(SumFor);

        var ascIte  = FindAsc(SumIte);
        var descIte = FindDesc(SumIte);

        PrintResult("SumFor", ascFor, descFor);
        PrintResult("SumIte", ascIte, descIte);
    }
}
