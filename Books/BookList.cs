using ProjektUppgift;
using Spectre.Console;

namespace ProjektUppgift.Book;

public class BookList
{

    public List<Book> Books { get; set; } = new List<Book>();

    //===== METODER SOM SÖKER/SORTERAR BÖCKER =====

    //sortera efter status
    public void SortByReadStatus(string argReadStatus)
    {
        List<Book> sortedBooks;

        if (argReadStatus == "read")
        {
            sortedBooks = Books.OrderByDescending(book => book.IsRead).ToList();
        }
        else if (argReadStatus == "unread")
        {
            sortedBooks = Books.OrderBy(book => book.IsRead).ToList();
        }
        else
        {
            Console.WriteLine("1. Olästa böcker först.");
            Console.WriteLine("2. Lästa böcker först.");
            Console.Write("Välj sortering: ");

            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                sortedBooks = Books.OrderBy(book => book.IsRead).ToList();
            }
            else if (choice == "2")
            {
                sortedBooks = Books.OrderByDescending(book => book.IsRead).ToList();
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
                return;
            }
        }
        BookDesign.ShowSpinner();
        BookDesign.DisplayBooksTable(sortedBooks);
    }
    //filtrera böcker efter sökord
    public void SearchBooks(string userSearch)
    {
        //om sökningen är tom eller innehåller whitespace
        if (string.IsNullOrWhiteSpace(userSearch))
        {
            Console.WriteLine("Du kan inte söka efter en bok utan namn");
            return;
        }

        bool found = false;

        foreach (Book book in Books)
        {
            //om sökningen innehåller antingen del av titel eller författare, visa.
            if (book.Title.Contains(userSearch, StringComparison.OrdinalIgnoreCase) || book.Author.Contains(userSearch, StringComparison.OrdinalIgnoreCase))
            {
                AnsiConsole.MarkupLine($"[bold darkcyan]{book.Title}[/] av {book.Author}");

                BookDesign.ChangeStatus(book);
                found = true;
            }
        }
        if (found == false)
        {
            AnsiConsole.MarkupLine("[red]Hm, sökningen hittade tyvärr inget.[/]");
        }
    }


    //===== METODER SOM ÄNDRAR BÖCKER =====

    //lägg till bok
    public bool AddBook(string title, string author, int pages, string genre)
    {
        if (string.IsNullOrWhiteSpace(title) || 
            string.IsNullOrWhiteSpace(author) || 
            string.IsNullOrWhiteSpace(genre) ||
            pages <= 0)
        {
            return false;
        }

        if(Books.Any(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)))
        {
            return false;
        }

        Book newBook = new Book(title, author, pages, genre);
        Books.Add(newBook);
        JsonBookStorage.Save(this);
        return true;
    }

    //betygsätt bok
    public bool AddRating(int bookNumber)
    {
        if (bookNumber < 1 || bookNumber > Books.Count)
        {
            Console.WriteLine("Det finns ingen bok med det numret.");
            return false;
        }

        //i booklist är böckerna numrerade med start på 0. - 1 gör att användarens val matchar
        int booklistIndex = bookNumber - 1;

        Console.WriteLine("Vilket betyg vill du sätta (1-5)?");
        if (!int.TryParse(Console.ReadLine(), out int rating) || rating < 1 || rating > 5)
        {
            Console.WriteLine("Vänligen ange ett giltigt betyg mellan 1 och 5.");
            return false;
        }

        //hämta rätt bok gebnnom att använda omräknade index (booknumber - 1)
        var book = Books[booklistIndex];

        book.Rating = rating;
        JsonBookStorage.Save(this);
        Console.WriteLine("Betyget har sparats!");
        return true;
    }
    //markera som läst/oläst
    public void AddAsRead()
    {
        BookDisplay bookDisplay = new BookDisplay();

        if(Books.Count == 0)
        {
            Console.WriteLine("Det finns inga böcker att ändra status på.");
            return;
        }

        bookDisplay.ShowBooks(Books);

        Console.WriteLine("Ange bokens nummer som du vill markera som läst/oläst:");

        if(!int.TryParse(Console.ReadLine(), out int bookNumber))
        {
            Console.WriteLine("Vänligen ange ett giltigt nummer.");
            return;
        }
        if(bookNumber < 1 || bookNumber > Books.Count)
        {
            Console.WriteLine("Icke giltigt nummer. Försök igen.");
            return;
        }
        int booklistIndex = bookNumber - 1;

        //växla bool
        Books[booklistIndex].IsRead = !Books[booklistIndex].IsRead;

        JsonBookStorage.Save(this);
        //om boken blir markerad som läst, meddelande. Tvärtom om oläst
        if (Books[booklistIndex].IsRead)
        {

            Console.WriteLine("Boken är markerad som läst!");
        }
        else
        {
            Console.WriteLine("Boken är markerad som oläst!");
        }
        Thread.Sleep(1500);
    }

    //radera bok från json
    public void RemoveBook(string userInput)
    {
        if (!int.TryParse(userInput, out int userIndex))
        {
            Console.WriteLine("Vänligen ange ett giltigt index för boken du vill ta bort.");
            Thread.Sleep(1500);
            return;
        }
        //ändra så användarens nummer matchar index
        int booklistIndex = userIndex - 1;

        //om index är större eller lika med noll och om index är mindre än hela listan, radera. Annars felmeddelande
        if(booklistIndex >= 0 && booklistIndex < Books.Count)
        {
            Books.RemoveAt(booklistIndex);
            Console.WriteLine($"Bok nummer {userIndex} har tagits bort");

            JsonBookStorage.Save(this);
            Thread.Sleep(1500);
        }
        else
        {
            Console.WriteLine("Det finns ingen bok med det numret.");
            Thread.Sleep(1500);
        }
    }
}