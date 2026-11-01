using Spectre.Console;

namespace ProjektUppgift.Book
{
    public class BookDisplay
    {

        //==== METODER SOM VISAR INFORMATION OM BÖCKER =====

        public void ShowBooks(List<Book> books)
        {
            BookDesign.ShowSpinner();
            BookDesign.DisplayBooksTable(books);
        }

        //föreslå en bok att läsa bland olästa
        public void SuggestRandomBook(List<Book> books)
        {
            //visa en spinner när random bok letas
            BookDesign.ShowSpinner();
            //samla alla böcker som inte är lästa
            var unreadBooks = books.Where(book => !book.IsRead).ToList();

            //om inga olästa, felmeddelande
            if (unreadBooks.Count == 0)
            {
                Console.WriteLine("Tyvärr har du inga olästa böcker.");
                return;
            }

            //skapa random objekt för att kunna använda slumpmässiga tal
            Random random = new Random();

            //slumpa fram ett tal inom listan av olästa böcker
            int randomIndex = random.Next(unreadBooks.Count);

            //hämta bok på det slumpade indexet
            var book = unreadBooks[randomIndex];


            AnsiConsole.MarkupLine($"Ditt bokförslag: [bold darkcyan]{book.Title}[/] av {book.Author}");

            Console.WriteLine();
        }
        public void BookReadStats(List<Book> books)
        {
            int readBooks = books.Count(book => book.IsRead);
            int unreadBooks = books.Count(book => !book.IsRead);

            Console.WriteLine($"Alla dina böcker: {books.Count}");
            Console.WriteLine($"Du har läst: {readBooks} böcker");
            Console.WriteLine($"Du har olästa böcker: {unreadBooks}");
        }
    }
}