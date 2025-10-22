using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        // Asegurar punto decimal consistente
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        var metals = new[]
        {
            "Platinum","Golden","Silver","Bronze","Copper",
            "Nickel","Aluminum","Iron","Tin","Lead"
        };

        const int n = 25; // dado por el enunciado

        for (int z = 0; z <= 9; z++)
        {
            Console.WriteLine($"[{z}] {metals[z]}");

            double f = Numbers.Formula(z);
            Console.WriteLine($" ↳ formula({z})   ≈ {Numbers.Round10(f)}");

            double r = Numbers.RecursiveRatio(z, n);
            Console.WriteLine($" ↳ recursive({z}) ≈ {Numbers.Round10(r)}");

            double it = Numbers.IterativeRatio(z, n);
            Console.WriteLine($" ↳ iterative({z}) ≈ {Numbers.Round10(it)}");
            Console.WriteLine();
        }
    }
}
