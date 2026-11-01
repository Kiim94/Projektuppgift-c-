using System;
using System.Collections.Generic;
using System.Text;
using Spectre.Console;

namespace ProjektUppgift.Book;

public class BookDesign
{
    public static void ChangeStatus(Book book)
    {
        //beroende på lässtatus, ändra hur status ser ut
        if (book.IsRead)
        {
            AnsiConsole.MarkupLine("Status: [green]Läst[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("Status: [red]Oläst[/]");
        }
    }

    public static void DisplayBooksTable(List<Book> books)
    {
        //skapa tabell objekt
        var table = new Table();

        //styla tabell
        table.Border(TableBorder.Rounded);
        table.BorderColor(Color.Cyan);

        //namn på tabellen
        table.Title("[bold yellow]Mina böcker[/]");

        //kolumner i tabellen
        table.AddColumn("Nr");
        table.AddColumn("Titel");
        table.AddColumn("Författare");
        table.AddColumn("Genre");
        table.AddColumn("Sidor");
        table.AddColumn("Status");
        table.AddColumn("Betyg");
        table.AddColumn("Tillagd");

        for (int i = 0; i < books.Count; i++)
        {
            Book book = books[i];

            string status = book.IsRead ? "[green]Läst[/]" : "[red]Oläst[/]";

            //tabellrader. Första ska visa index nummer. De rader med ToString() används för att omvandla siffror till text format
            table.AddRow((i + 1).ToString(),
                book.Title,
                book.Author,
                book.Genre,
                book.Pages.ToString(),
                status,
                book.Rating.ToString(),
                book.DateAdded.ToString("yyyy-MM-dd")
                );
        }
        //skriv ut tabell till konsolen
        AnsiConsole.Write(table);
    }
    public static void ShowSpinner()
    {
        AnsiConsole.Status().Spinner(Spinner.Known.Dots).Start("Vänta ett ögonblick...", ctx =>
        {
            Thread.Sleep(1500);
        });
    }
}
