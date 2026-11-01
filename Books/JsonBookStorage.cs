
using System.Text.Json;

namespace ProjektUppgift.Book;

public class JsonBookStorage
{
    //för att lagra användarens boklista på bestämd plats, oberoende av programmets aktuella katalog
    //beroende på om terminalen eller debugger användes gjorde tidigare alternativ att en lista sparades i bin, den andra direkt i projektuppgiftens mapp
    //
    //fler alternativ: https://putridparrot.com/blog/where-to-store-your-application-data/
    private static string GetFilePath()
    {
        //bestäm vad mapp för Booktracker ska vara
        string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BookTracker");

        Directory.CreateDirectory(folder);
        return Path.Combine(folder, "booklist.json");
    }
    //spara lista med böcker i Json
    public static void Save(BookList books)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };

        string json = JsonSerializer.Serialize(books, options);
        string filePath = GetFilePath();
        File.WriteAllText(filePath, json);
    }

    //ladda existerande eller skapa ny BookLog
    public static BookList Load()
    {
        string filePath = GetFilePath();

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<BookList>(json) ?? new BookList();
        }
        return new BookList();
    }
}
