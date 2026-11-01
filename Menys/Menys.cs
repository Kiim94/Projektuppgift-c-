using Spectre.Console;
using ProjektUppgift.Book;

namespace ProjektUppgift;

//klass som håller meny-delar från Program.cs pga att den blev väldigt lång.
public class Menys
{
    private static BookDisplay display = new BookDisplay();
    //meny under alla böcker som man har lagt till
    public static void ShowBookMenu(BookList books, string argReadStatus)
    {
        int choice = 0;

        //om valet inte är 5, fortgå till andra menyval
        while (choice != 5)
        {
            Console.Clear();
            display.ShowBooks(books.Books);
            Console.WriteLine();

            AnsiConsole.Write(new Rule("VAD VILL DU GÖRA?"));

            display.BookReadStats(books.Books);
            Console.WriteLine();
            Console.WriteLine("1. Sortera böcker efter status.*");
            Console.WriteLine("2. Sök efter bok");
            Console.WriteLine("3. Ändra en boks status.");
            Console.WriteLine("4. Betygsätt bok.");
            Console.WriteLine("5. Återgå till menyn.");
            Console.WriteLine();

            //gul färg för att göra användaren observant
            var panel = new Panel("* Sorteringen visar en ny sorterad lista.\nDen ursprungliga listan påverkas inte.").Header("[yellow]Notera[/]").BorderColor(Color.Yellow).RoundedBorder();
            //AnsiConsole.MarkupLine("[yellow]*  Observera! Sorteringen visar en ny sorterad lista.\n   Den ursprungliga listan påverkas inte.[/]");
            AnsiConsole.Write(panel);
            AnsiConsole.Write(new Rule());

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Vänligen ange ett nummer från listan.");
                continue;
            }
            if (choice < 1 || choice > 5)
            {
                Console.WriteLine("Vänligen ange ett nummer mellan 1 och 5.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    AnsiConsole.Write(new Rule(" SORTERA EFTER STATUS "));
                    if (argReadStatus == "read" || argReadStatus == "unread")
                    {
                        Console.WriteLine("Observera! Sorteringsordningen har valts via argument. \nStarta med 'dotnet run' utan argument för att välja sortering manuellt.");
                    }
                    books.SortByReadStatus(argReadStatus);
                    ReturnMeny();
                    break;
                case 2:
                    Console.Clear();
                    AnsiConsole.Write(new Rule(" SÖK EFTER BOK "));
                    Console.WriteLine("Vad letar du efter?");
                    string userSearch = Console.ReadLine() ?? "";
                    books.SearchBooks(userSearch);
                    ReturnMeny();
                    break;

                case 3:
                    Console.Clear();
                    AnsiConsole.Write(new Rule(" ÄNDRA STATUS "));
                    books.AddAsRead();
                    ReturnMeny();
                    break;

                case 4:
                    Console.Clear();
                    AnsiConsole.Write(new Rule(" BETYGSÄTT BOK "));
                    display.ShowBooks(books.Books);
                    Console.WriteLine("Vilken bok vill du betygsätta?");
                    if (!int.TryParse(Console.ReadLine(), out int bookNumber))
                    {
                        Console.WriteLine("Ange ett giltigt boknummer!");
                        ReturnMeny();
                        break;
                    }
                    
                    books.AddRating(bookNumber);
                    ReturnMeny();
                    break;

                case 5:
                    break;
                default:
                    break;
            }
        }
    }

    //meny när man lägger till böcker
    public static void AddBookMenu(BookList books)
    {
        int pages;

        Console.WriteLine("Lägg till en ny bok! Du kan markera den som läst och ge betyg i menyval 1.");
        Console.WriteLine();
        Console.Write("Boktitel: ");
        string title = Console.ReadLine() ?? "";
        Console.Write("Författaren: ");
        string author = Console.ReadLine() ?? "";

        while (true)
        {
            Console.Write("Sidoantal: ");

            if (int.TryParse(Console.ReadLine(), out pages) && pages > 0)
            {

                break;
            }
            else
            {
                Console.Write("Felaktigt sidantal! Försök igen.");
            }
        }

        Console.Write("Genre: ");
        string genre = Console.ReadLine() ?? "";

        if (books.AddBook(title, author, pages, genre))
        {
            Console.WriteLine("Boken har lagts till!");
        }
        else
        {
            Console.WriteLine("Boken kunde inte läggas till. Kontrollera så att alla fält är ifyllda och att boken inte redan finns i bibliotekt.");
        }
    }
    

    //meny när man vill få bokförslag
    public static void SuggestBookMeny(BookList books)
    {
        bool inRandomBookMenu = true;
        while (inRandomBookMenu)
        {
            Console.Clear();
            AnsiConsole.Write(new Rule("[bold yellow]Slumpa bok[/]").RuleStyle("grey"));

            Console.WriteLine();
            Console.WriteLine("1. Ge ett bokförslag");
            Console.WriteLine("2. Återgå till huvudmenyn");

            if (!int.TryParse(Console.ReadLine(), out int randomBookChoice) || randomBookChoice < 1 || randomBookChoice > 2)
            {
                Console.WriteLine("Vänligen ange ett giltigt menyval");
                continue;
            }
            switch (randomBookChoice)
            {
                //förslag på bok
                case 1:
                    Console.Clear();
                    display.SuggestRandomBook(books.Books);
                    ReturnMeny();
                    break;
                //återgå
                case 2:
                    inRandomBookMenu = false;
                    break;
            }
        }
    }

    public static void DeleteBookMeny(BookList books)
    {
        //kalla på metod i BookDisplay: skicka med books.Books (namn på lista här, namn på egenskap i BookList) för att kunna visa böcker
        display.ShowBooks(books.Books);
        bool running = true;
        while (running)
        {
            Console.WriteLine("1. Radera bok");
            Console.WriteLine("2. Återgå till huvudmenyn");
            if (!int.TryParse(Console.ReadLine(), out int deleteChoice) || deleteChoice < 1 || deleteChoice > 2)
            {
                Console.WriteLine("Vänligen ange ett av menyvalen");
                continue;
            }
            switch (deleteChoice)
            {
                case 1:
                    Console.Write("Ange boknumret:");
                    string userInput = Console.ReadLine() ?? "";

                    books.RemoveBook(userInput);
                    Console.Clear();
                    display.ShowBooks(books.Books);

                    break;
                case 2:
                    running = false;
                    break;
            }
        }
    }

    //återgå till föregående meny
    public static void ReturnMeny()
    {
        Console.WriteLine("Tryck på Enter för att återgå till menyn.");
        Console.ReadLine();
        Console.Clear();
    }
}
