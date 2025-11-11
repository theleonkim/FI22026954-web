using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        // Asegura la carpeta de datos
        Directory.CreateDirectory("data");

        using var db = new BooksContext();

        // ¿BD vacía? -> Primera corrida: llenar desde CSV
        if (!db.Authors.Any())
        {
            Console.WriteLine("La base de datos está vacía, se cargará desde el CSV...");

            var csvPath = Path.Combine("data", "books.csv");
            if (!File.Exists(csvPath))
            {
                Console.WriteLine("No se encontró data/books.csv. Coloca el archivo y vuelve a ejecutar.");
                return;
            }

            var lines = File.ReadLines(csvPath, Encoding.UTF8).Skip(1); // salta encabezado

            foreach (var raw in lines)
            {
                if (string.IsNullOrWhiteSpace(raw)) continue;

                var parts = SplitCsvLine(raw);
                if (parts.Length < 3) continue;

                var authorName = parts[0].Trim();
                var titleName  = parts[1].Trim();
                var tagsRaw    = parts[2].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                // Autor (buscar o crear)
                var author = db.Authors.FirstOrDefault(a => a.AuthorName == authorName);
                if (author is null)
                {
                    author = new Author { AuthorName = authorName };
                    db.Authors.Add(author);
                    db.SaveChanges(); // asegura AuthorId para relaciones
                }

                // Título
                var title = new Title { TitleName = titleName, AuthorId = author.AuthorId };
                db.Titles.Add(title);
                db.SaveChanges(); // asegura TitleId

                // Tags y relación TitleTag (IMPORTANTE: usar navegaciones)
                foreach (var tagName in tagsRaw)
                {
                    var tag = db.Tags.FirstOrDefault(t => t.TagName == tagName);
                    if (tag is null)
                    {
                        tag = new Tag { TagName = tagName };
                        db.Tags.Add(tag); // trackea para asignar Id al guardar
                    }

                    db.TitlesTags.Add(new TitleTag { Title = title, Tag = tag });
                }

                db.SaveChanges(); // guarda el batch de este registro
            }

            Console.WriteLine("Procesando... Listo.");
            return;
        }

        // BD con datos -> Segunda corrida: generar TSV
        Console.WriteLine("La base de datos se está leyendo para crear los archivos TSV.");

        var rows = db.TitlesTags
            .Include(tt => tt.Title).ThenInclude(t => t.Author)
            .Include(tt => tt.Tag)
            .Select(tt => new
            {
                AuthorName = tt.Title.Author.AuthorName,
                TitleName  = tt.Title.TitleName,
                TagName    = tt.Tag.TagName
            })
            .AsEnumerable();

        // Agrupa por primera letra del AuthorName
        var groups = rows.GroupBy(r => char.ToUpperInvariant(r.AuthorName.FirstOrDefault('A')));

        foreach (var g in groups)
        {
            var filePath = Path.Combine("data", $"{g.Key}.tsv");
            using var w = new StreamWriter(filePath, false, new UTF8Encoding(false));
            w.WriteLine("AuthorName\tTitleName\tTagName");

            foreach (var r in g
                .OrderByDescending(r => r.AuthorName, StringComparer.CurrentCultureIgnoreCase)
                .ThenByDescending(r => r.TitleName,  StringComparer.CurrentCultureIgnoreCase)
                .ThenByDescending(r => r.TagName,    StringComparer.CurrentCultureIgnoreCase))
            {
                w.WriteLine($"{r.AuthorName}\t{r.TitleName}\t{r.TagName}");
            }
        }

        Console.WriteLine("Procesando... Listo.");
    }

    // Parser CSV que respeta comillas dobles en campos con coma
    static string[] SplitCsvLine(string line)
    {
        var res = new List<string>();
        bool inQuotes = false;
        var sb = new StringBuilder();

        foreach (var ch in line)
        {
            if (ch == '"') { inQuotes = !inQuotes; continue; }
            if (ch == ',' && !inQuotes)
            {
                res.Add(sb.ToString());
                sb.Clear();
            }
            else sb.Append(ch);
        }
        res.Add(sb.ToString());
        return res.ToArray();
    }
}
