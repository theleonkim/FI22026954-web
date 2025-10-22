using System;

public static class Numbers
{
    // No cambiar double ni el redondeo a 10 decimales.

    // PUBLIC: usados por Program.cs (no modificar firmas)
    public static double Formula(int z) => formula(z);
    public static double RecursiveRatio(int z, int n) => recursive_ratio(z, n);
    public static double IterativeRatio(int z, int n) => iterative_ratio(z, n);
    public static double Round10(double v) => Math.Round(v, 10, MidpointRounding.AwayFromZero);

    // ----------------- PRIVATE (aquí van tus cambios) -----------------

    // Fórmula directa: (z + sqrt(4 + z^2)) / 2
    private static double formula(int z)
    {
        // Improvement: implementación exacta
        double zz = z;
        return (zz + Math.Sqrt(4.0 + zz * zz)) / 2.0;
    }

    // f(z,0) = 1; f(z,1) = 1; f(z,n) = z * f(z,n-1) + f(z,n-2), para n >= 2
    private static double f_recursive(int z, int n)
    {
        if (n == 0 || n == 1) return 1.0;
        return z * f_recursive(z, n - 1) + f_recursive(z, n - 2);
    }

    // Razón f(z,n)/f(z,n-1) con n dado (el enunciado usa n=25 global)
    private static double recursive_ratio(int z, int n)
    {
        // Improvement: calcula exactamente con recursión
        if (n < 1) return 1.0; // seguridad
        double a = f_recursive(z, n);
        double b = f_recursive(z, n - 1);
        return a / b;
    }

    // Iterativo equivalente (sin recursión)
    private static double iterative_ratio(int z, int n)
    {
        // Update: implementación iterativa con dos acumuladores
        if (n < 1) return 1.0;
        double prev2 = 1.0; // f(z,0)
        double prev1 = 1.0; // f(z,1)

        if (n == 1) return prev1 / prev2; // 1/1 = 1

        for (int i = 2; i <= n; i++)
        {
            double cur = z * prev1 + prev2; // f(z,i) = z*f(z,i-1) + f(z,i-2)
            prev2 = prev1;
            prev1 = cur;
        }
        return prev1 / prev2; // f(z,n)/f(z,n-1)
    }
}
