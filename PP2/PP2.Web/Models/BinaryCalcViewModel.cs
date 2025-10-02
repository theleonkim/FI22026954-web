using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PP2.Web.Models
{
    // Valida: solo 0/1 y longitudes 2,4,6,8 (no vacío)
    public sealed class BinaryStringAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var s = (value as string)?.Trim() ?? string.Empty;

            if (s.Length == 0) { ErrorMessage = "Value is required."; return false; }

            int[] allowed = { 2, 4, 6, 8 };
            if (!allowed.Contains(s.Length))
            {
                ErrorMessage = "Length must be 2, 4, 6, or 8 characters.";
                return false;
            }

            foreach (var ch in s)
            {
                if (ch != '0' && ch != '1')
                {
                    ErrorMessage = "Only 0 and 1 are allowed.";
                    return false;
                }
            }
            return true;
        }
    }

    public class BinaryCalcViewModel
    {
        [Display(Name = "a")]
        [BinaryString]
        public string A { get; set; } = string.Empty;

        [Display(Name = "b")]
        [BinaryString]
        public string B { get; set; } = string.Empty;

        public List<ResultRow> Results { get; set; } = new();

        // a/b con 8 bits para mostrar
        public string A8 => PadLeftTo8(A);
        public string B8 => PadLeftTo8(B);
        static string PadLeftTo8(string s) => (s ?? "").PadLeft(8, '0');

        // ---- Operaciones bit a bit sobre strings ----
        public static string BitwiseAnd(string a, string b)
        {
            Align(a, b, out var aa, out var bb);
            var chars = new char[aa.Length];
            for (int i = 0; i < aa.Length; i++)
                chars[i] = (aa[i] == '1' && bb[i] == '1') ? '1' : '0';
            return TrimBin(new string(chars));
        }

        public static string BitwiseOr(string a, string b)
        {
            Align(a, b, out var aa, out var bb);
            var chars = new char[aa.Length];
            for (int i = 0; i < aa.Length; i++)
                chars[i] = (aa[i] == '1' || bb[i] == '1') ? '1' : '0';
            return TrimBin(new string(chars));
        }

        public static string BitwiseXor(string a, string b)
        {
            Align(a, b, out var aa, out var bb);
            var chars = new char[aa.Length];
            for (int i = 0; i < aa.Length; i++)
                chars[i] = (aa[i] != bb[i]) ? '1' : '0';
            return TrimBin(new string(chars));
        }

        static void Align(string a, string b, out string aa, out string bb)
        {
            aa = a ?? "";
            bb = b ?? "";
            int n = Math.Max(aa.Length, bb.Length);
            aa = aa.PadLeft(n, '0');
            bb = bb.PadLeft(n, '0');
        }

        static string TrimBin(string s)
        {
            var t = s.TrimStart('0');
            return string.IsNullOrEmpty(t) ? "0" : t;
        }

        // Conversiones
        public static int    BinToInt(string s) => Convert.ToInt32(s, 2);
        public static string IntToBin(int n)    => n == 0 ? "0" : Convert.ToString(n, 2);
        public static string IntToOct(int n)
        {
            if (n == 0) return "0";
            var stack = new Stack<char>();
            for (int v = n; v > 0; v /= 8) stack.Push((char)('0' + (v % 8)));
            return new string(stack.ToArray());
        }
        public static string IntToDec(int n) => n.ToString();
        public static string IntToHex(int n) => n.ToString("X");
    }

    public class ResultRow
    {
        public string Label { get; set; } = "";
        public string Bin   { get; set; } = "";
        public string Oct   { get; set; } = "";
        public string Dec   { get; set; } = "";
        public string Hex   { get; set; } = "";
    }
}
